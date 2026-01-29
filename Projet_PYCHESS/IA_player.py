import torch
import chess
import numpy as np
import random
import os
from NN_model import ChessEvalNet, board_to_tensor

class AIPlayer:
    def __init__(self, model_path="chess_eval_model.pt"):
        self.device = torch.device('cpu')
        print(f"Chargement du cerveau IA sur {self.device}...")
        
        if not os.path.exists(model_path):
            print(f"❌ ERREUR : Le fichier '{model_path}' est introuvable.")
            self.model = None
            return

        try:
            self.model = ChessEvalNet()
            self.model.load_state_dict(torch.load(model_path, map_location=self.device))
            self.model.to(self.device)
            self.model.eval()
            print("IA chargée")
        except Exception as e:
            print(f"Erreur critique lors du chargement : {e}")
            self.model = None

    def evaluate_board(self, board):
        """
        Utilise le Réseau de Neurones pour donner une note à la position.
        """
        # Si le modèle n'est pas chargé, on renvoie 0
        if self.model is None:
            return 0

        # Conversion en matrice (Tensor)
        # unsqueeze(0) ajoute la dimension du batch : [1, 13, 8, 8]
        tensor = torch.from_numpy(board_to_tensor(board)).float().unsqueeze(0).to(self.device)

        # Le modèle donne son avis
        with torch.no_grad():
        
            outputs = self.model(tensor)
            
            # Transformation en pourcentages (Softmax)
            probs = torch.softmax(outputs, dim=1)[0]
            
            # probs[0] = proba noir gagne
            # probs[1] = proba nul
            # probs[2] = proba blanc gagne
            
            #score final blanc = 1 noir = -1
            score = probs[2].item() - probs[0].item()
            
        return score

    def minimax(self, board, depth, alpha, beta, maximizing_player):
        """
        Algorithme Minimax avec élagage Alpha-Beta.
        """
        #safety
        if depth == 0 or board.is_game_over():
            if board.is_checkmate():
                #enorme note pour un mat
                return -9999 if board.turn == chess.WHITE else 9999
            return self.evaluate_board(board)
        
        legal_moves = list(board.legal_moves)
        
        #on regarde les captures d'abord pour couper l'arbre plus vite (opti)
        legal_moves.sort(key=lambda move: board.is_capture(move), reverse=True)
        
        #maximiser son propre score
        if maximizing_player:
            max_eval = -float('inf')
            for move in legal_moves:
                board.push(move)
                eval = self.minimax(board, depth - 1, alpha, beta, False)
                board.pop()
                
                max_eval = max(max_eval, eval)
                alpha = max(alpha, eval)
                if beta <= alpha:
                    break # Élagage
            return max_eval

        #minimiser adversaire
        else:
            min_eval = float('inf')
            for move in legal_moves:
                board.push(move)
                eval = self.minimax(board, depth - 1, alpha, beta, True)
                board.pop()
                
                min_eval = min(min_eval, eval)
                beta = min(beta, eval)
                if beta <= alpha:
                    break #elagage
            return min_eval

    def predict_move(self, board):
        """
        Fonction principale appelée par le jeu.
        """
        legal_moves = list(board.legal_moves)
        if not legal_moves:
            return None
        

        depth = 3   #coups d'avance
        
        best_move = None
        maximizing_player = (board.turn == chess.WHITE)
        
        #init
        best_value = -float('inf') if maximizing_player else float('inf')
        alpha = -float('inf')
        beta = float('inf')


        # On itère sur les coups racines
        legal_moves.sort(key=lambda move: board.is_capture(move), reverse=True)

        for move in legal_moves:
            board.push(move)
            #récursive
            board_value = self.minimax(board, depth - 1, alpha, beta, not maximizing_player)
            board.pop()

            # print(f"Coup {move} : {board_value:.3f}") # Debug

            if maximizing_player:
                if board_value > best_value:
                    best_value = board_value
                    best_move = move
                alpha = max(alpha, board_value)
            else:
                if board_value < best_value:
                    best_value = board_value
                    best_move = move
                beta = min(beta, board_value)
        
        #debuglog
        if best_move is None:
            return random.choice(legal_moves)
            
        print(f"IA joue : {best_move} (Score: {best_value:.3f})")
        return best_move