import chess
import random
import torch
import pickle
import os
import torch.nn as nn
import torch.nn.functional as F
import numpy as np
from torch.utils.data import TensorDataset, DataLoader, Dataset
import torch.optim as optim
import chess.pgn
from tqdm import tqdm



# Gestion DirectML
try:
    import torch_directml 
    HAS_DIRECTML = True
except ImportError:
    HAS_DIRECTML = False

# ---ARCHITECTURE CLASSIFICATION  ---

class ResidualBlock(nn.Module):
    def __init__(self, channels):
        super().__init__()
        
        self.conv1 = nn.Conv2d(channels, channels, 3, padding=1)
        self.bn1 = nn.GroupNorm(8, channels)
        
        
        self.conv2 = nn.Conv2d(channels, channels, 3, padding=1)
        self.bn2 = nn.GroupNorm(8,channels)
        
    def forward(self, x):
        residual = x  #on garde une copie de l'entrée
        
        out = F.relu(self.bn1(self.conv1(x)))
        out = self.bn2(self.conv2(out))
        
        out += residual 
        out = F.relu(out) #on active après l'addition
        return out

class ChessEvalNet(nn.Module):
    def __init__(self):
        super().__init__()
        
        self.conv_input = nn.Conv2d(13, 128, 3, padding=1)
        self.bn_input = nn.BatchNorm2d(128)
        
        self.res_layers = nn.ModuleList([
            ResidualBlock(128) for _ in range(5) 
        ])
        self.conv_out = nn.Conv2d(128, 32, 1) # Conv 1x1 pour compresser l'info
        self.bn_out = nn.GroupNorm(4, 32)
        
        # Couches denses finales
        self.fc1 = nn.Linear(32 * 8 * 8, 128)
        self.fc2 = nn.Linear(128, 3) # 3 choix : Noir, Nul, Blanc

    def forward(self, x):
        #entree
        x = F.relu(self.bn_input(self.conv_input(x)))
        
        #passage dans les blocs résiduels
        for layer in self.res_layers:
            x = layer(x)
            
        # sortie
        x = F.relu(self.bn_out(self.conv_out(x)))
        x = x.view(x.size(0), -1) #aplatir
        
        x = F.relu(self.fc1(x))
        x = self.fc2(x)
        
        return x
    

#data --------

PIECE_TO_PLANE = {'P':0, 'N':1, 'B':2, 'R':3, 'Q':4, 'K':5, 'p':6, 'n':7, 'b':8, 'r':9, 'q':10, 'k':11}

def board_to_tensor(board):
    tensor = np.zeros((13, 8, 8), dtype=np.float32)
    for square, piece in board.piece_map().items():
        rank, file = chess.square_rank(square), chess.square_file(square)
        tensor[PIECE_TO_PLANE[piece.symbol()], rank, file] = 1.0
    if board.turn == chess.WHITE:
        tensor[12, :, :] = 1.0
    return tensor

def fen_to_tensor(fen):
    return board_to_tensor(chess.Board(fen))

class ChessDataset(Dataset):
    def __init__(self, data): self.data = data
    def __len__(self): return len(self.data)
    def __getitem__(self, idx):
        fen, label = self.data[idx]
        return torch.from_numpy(fen_to_tensor(fen)), torch.tensor(label, dtype=torch.long)

def parse_pgn_classification(source_path, max_games=100000):
    data = []
    count = 0
    files = [os.path.join(source_path, f) for f in os.listdir(source_path) if f.endswith(".pgn")]
    print(f" {len(files)} fichiers PGN.")
    
    pbar = tqdm(total=max_games, desc="Parsing")
    
    for file_path in files:
        if count >= max_games: break
        try:
            with open(file_path, 'r', encoding='utf-8', errors='ignore') as f:
                while True:
                    game = chess.pgn.read_game(f)
                    if game is None: break
                    
                    # CLASSIFICATION : On transforme le résultat en 0, 1, 2
                    res = game.headers.get("Result", "*")
                    if res == "1-0": label = 2    #Blanc gagne
                    elif res == "0-1": label = 0  #Noir gagne
                    else: label = 1               #Nul

                    board = game.board()
                    ply = 0
                    for move in game.mainline_moves():
                        board.push(move)
                        ply += 1
                        #on prend des positions pas trop proches du début
                        if ply > 16 and ply % 10 == 0: 
                             data.append((board.fen(), label))
                    
                    count += 1
                    pbar.update(1)
                    if count >= max_games: break
        except: continue
    pbar.close()
    return data

def balance_dataset(data):
    #on sépare les 3 classes
    wins = [d for d in data if d[1] == 2]
    loss = [d for d in data if d[1] == 0]
    draw = [d for d in data if d[1] == 1]
    
    #on coupe à la taille du plus petit groupe
    n = min(len(wins), len(loss), len(draw))
    if n == 0: return data
    
    print(f"Dataset équilibré : {n} Victoires / {n} Défaites / {n} Nuls")
    return wins[:n] + loss[:n] + draw[:n]

#training--------
def train(model, dataset, epochs, batch_size, lr, device):
    loader = DataLoader(ChessDataset(dataset), batch_size=batch_size, shuffle=True, num_workers=0)
    optimizer = optim.Adam(model.parameters(), lr=lr)
    
    #crossentropy c'est bien apparament
    criterion = nn.CrossEntropyLoss() 
    
    model.to(device)
    model.train()

    print(f"calculs sur : {device}")
    
    for epoch in range(epochs):
        loop = tqdm(loader, desc=f"Epoch {epoch+1}/{epochs}")
        total_loss = 0
        correct = 0
        total = 0
        
        for X, y in loop:
            X, y = X.to(device), y.to(device)
            optimizer.zero_grad()
            
            outputs = model(X)
            loss = criterion(outputs, y)
            
            loss.backward()
            optimizer.step()
            
            total_loss += loss.item() * X.size(0)
            
            #accuracy
            _, predicted = torch.max(outputs, 1)
            correct += (predicted == y).sum().item()
            total += y.size(0)
            
            loop.set_postfix(loss=f"{loss.item():.4f}", acc=f"{100*correct/total:.1f}%")

        print(f"   -> Fin Epoch {epoch+1} | Loss: {total_loss/total:.4f} | Précision: {100*correct/total:.2f}%")

    torch.save(model.state_dict(), "chess_eval_model.pt")

#main --------
if __name__ == "__main__":
    device = torch.device('cpu')
    if HAS_DIRECTML and torch_directml.is_available(): #on essaie de trouver le gpu
        
        count = torch_directml.device_count()
        print(f"DirectML a trouvé {count} cartes graphiques.")
        
        found_amd = False
        for i in range(count):
            name = torch_directml.device_name(i)
            print(f"GPU {i}: {name}")
            if "Radeon" in name or "AMD" in name or "7800" in name:
                device = torch_directml.device(i)
                print(f"Utilisation de : {name}")
                found_amd = True
                break
        
        if not found_amd:
            print("Pas de carte AMD")
            device = torch_directml.device()
    
    base_dir = os.path.dirname(os.path.abspath(__file__))
    pgn_path = os.path.join(base_dir, "games_pgn")
    dataset_cache = "dataset_cache.pkl"
    
    dataset = None
    if os.path.exists(dataset_cache):   #on cherche un dataset déja trié
        try:
            with open(dataset_cache, "rb") as f: dataset = pickle.load(f)
            print("Cache chargé")
        except: pass

    if not dataset:
        raw_data = parse_pgn_classification(pgn_path, max_games=100000)
        dataset = balance_dataset(raw_data)
        random.shuffle(dataset)
        with open(dataset_cache, "wb") as f: 
            pickle.dump(dataset, f)
        print(f"Dataset préparé et sauvegardé ({len(dataset)} positions)")
    
    model = ChessEvalNet()
    optimizer = optim.Adam(model.parameters(), lr=0.001)
    criterion = nn.CrossEntropyLoss()


    
    #debug lgo
    if len(dataset) == 0:
        print("Dataset vide, impossible de calculer les statistiques.")
    else:
        ys = np.array([y for _, y in dataset])
        print("mean:", ys.mean())
        print("std :", ys.std())
        print("min/max:", ys.min(), ys.max())


    mini_dataset = dataset[:64]

    print([y for _, y in random.sample(dataset, 10)])
    
   
    test_loader = DataLoader(ChessDataset(mini_dataset), batch_size=16, shuffle=True)

    """#chargement du dataset
    if not os.path.exists("dataset_cache.pkl"):
        print("Erreur : dataset_cache.pkl introuvable.")
        exit()
        
    with open("dataset_cache.pkl", "rb") as f:
        full_dataset = pickle.load(f)


    mini_dataset = full_dataset[:128]
    test_loader = DataLoader(ChessDataset(mini_dataset), batch_size=16, shuffle=True)
    """

    #sans directml
    model = ChessEvalNet().to(device)
    optimizer = optim.Adam(model.parameters(), lr=0.001)
    criterion = nn.CrossEntropyLoss()

    """
    print("\nTest d'apprentissage") #debug log

    for epoch in range(1, 41):
        total_loss = 0
        correct = 0
        total = 0
        
        for X, y in test_loader:
            X, y = X.to(device), y.to(device)
            
            optimizer.zero_grad()
            out = model(X)
            loss = criterion(out, y)
            loss.backward()
            
            if epoch == 1 and total == 0:
                has_grad = any(p.grad is not None and torch.sum(torch.abs(p.grad)) > 0 for p in model.parameters())
                print(f" Gradients détectés : {has_grad}") #debuglog

            optimizer.step()
            
            _, pred = torch.max(out, 1)
            correct += (pred == y).sum().item()
            total += y.size(0)
            total_loss += loss.item()

        acc = 100 * correct / total
        if epoch % 5 == 0 or epoch == 1:
            print(f"Epoch {epoch:2d}/40 | Loss: {total_loss/len(test_loader):.4f} | Précision: {acc:5.1f}%")
        
        if acc >= 99:
            print(f"ca marcheee")
            break
        """

    train(model, dataset, epochs=25, batch_size=128, lr=0.0005, device=device)