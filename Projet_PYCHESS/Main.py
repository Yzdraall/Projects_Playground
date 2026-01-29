import pygame
import chess
import random
import torch
import torch.nn as nn
import numpy as np
from torch.utils.data import TensorDataset, DataLoader
from Engine import ChessEngine
from NN_model import ChessEvalNet, board_to_tensor, parse_pgn_classification
from IA_player import AIPlayer



# --- CONSTANTES PYGAME ---
WIDTH = HEIGHT = 512       # Taille de la fenêtre
DIMENSION = 8              # Échiquier 8x8
SQ_SIZE = WIDTH // DIMENSION # Taille d'une case
MAX_FPS = 15               # Pour l'animation
IMAGES = {}

# Beige clair
C_LIGHT = (240, 217, 181) 
# Marron foncé
C_DARK = (181, 136, 99)




def load_images():
    """Charge les images wP, wR, bP, etc."""
    pieces = ['wP', 'wR', 'wN', 'wB', 'wQ', 'wK', 'bP', 'bR', 'bN', 'bB', 'bQ', 'bK']
    for piece in pieces:
        try:
            # Utilisez un chemin relatif si possible, sinon vérifiez bien votre chemin absolu
            # Notez le f"..." pour inclure le nom de la pièce
            chemin = f"C:/Users/seval/Documents/Cours/Projet_chess/pieces-png/{piece}.png"
            IMAGES[piece] = pygame.transform.scale(
                pygame.image.load(chemin), 
                (SQ_SIZE, SQ_SIZE)
            )
        except FileNotFoundError:
            print(f"❌ ERREUR : Impossible de trouver l'image : {chemin}")

def draw_pieces(screen, board, inverse_vue=False):
    """
    Dessine les pièces.
    Si inverse_vue est True (Joueur Noir), on dessine le plateau à l'envers.
    """
    for square_index, piece in board.piece_map().items():
        # Coordonnées standard (Blancs en bas)
        rank = chess.square_rank(square_index) # Ligne (0-7)
        file = chess.square_file(square_index) # Colonne (0-7)

        if inverse_vue:
            # Si on joue les noirs, on inverse tout
            # La ligne 0 (Blancs) devient la ligne visuelle 0 (Haut de l'écran)
            r = rank 
            # La colonne 0 (A) devient la colonne visuelle 7 (Droite de l'écran)
            c = 7 - file
        else:
            # Vue standard (Blancs en bas)
            r = 7 - rank
            c = file
        
        color_code = 'w' if piece.color == chess.WHITE else 'b'
        type_code = piece.symbol().upper()
        image_name = color_code + type_code
        
        if image_name in IMAGES:
            screen.blit(IMAGES[image_name], pygame.Rect(c*SQ_SIZE, r*SQ_SIZE, SQ_SIZE, SQ_SIZE))


def draw_board(screen):
    """Dessine les cases en marron et beige."""
    colors = [pygame.Color(C_LIGHT), pygame.Color(C_DARK)]
    for r in range(DIMENSION):
        for c in range(DIMENSION):
            # L'astuce ((r + c) % 2) permet d'alterner les couleurs
            color = colors[((r + c) % 2)]
            pygame.draw.rect(screen, color, pygame.Rect(c*SQ_SIZE, r*SQ_SIZE, SQ_SIZE, SQ_SIZE))

"""def bot_play(board):
    "l'adversaire joue un coup random"
    coups_legaux = list(board.legal_moves)
    if len(coups_legaux) > 0:
        return random.choice(coups_legaux)
    return None"""

def highlight_legal_moves(screen, board, sq_selected, inverse_vue):
    if sq_selected == ():
        return

    s_select = pygame.Surface((SQ_SIZE, SQ_SIZE))
    s_select.set_alpha(120)
    s_select.fill((0, 0, 255))  # Bleu

    s_move = pygame.Surface((SQ_SIZE, SQ_SIZE))
    s_move.set_alpha(120)
    s_move.fill((0, 255, 0))  # Vert

    # Conversion coordonnées
    rank = chess.square_rank(sq_selected)
    file = chess.square_file(sq_selected)

    if inverse_vue:
        r = rank
        c = 7 - file
    else:
        r = 7 - rank
        c = file

    screen.blit(s_select, (c * SQ_SIZE, r * SQ_SIZE))

    # Coups légaux
    for move in board.legal_moves:
        if move.from_square == sq_selected:
            rank = chess.square_rank(move.to_square)
            file = chess.square_file(move.to_square)

            if inverse_vue:
                r = rank
                c = 7 - file
            else:
                r = 7 - rank
                c = file

            screen.blit(s_move, (c * SQ_SIZE, r * SQ_SIZE))

def highlight_last_move(screen, board, inverse_vue):
    if len(board.move_stack) == 0:
        return

    last_move = board.move_stack[-1]

    s = pygame.Surface((SQ_SIZE, SQ_SIZE))
    s.set_alpha(100)
    s.fill((255, 225, 10))  # Jaune doux

    for square in [last_move.from_square, last_move.to_square]:
        rank = chess.square_rank(square)
        file = chess.square_file(square)

        if inverse_vue:
            r = rank
            c = 7 - file
        else:
            r = 7 - rank
            c = file

        screen.blit(s, (c * SQ_SIZE, r * SQ_SIZE))


def draw_end_game_text(screen, text):
    """Affiche le texte de fin de partie au centre."""
    # Création de la police (Taille 32, Gras)
    font = pygame.font.SysFont("Arial", 32, True, False)
    
    # Ombre du texte (Noir)
    text_object_shadow = font.render(text, 0, pygame.Color('gray'))
    text_location_shadow = pygame.Rect(0, 0, WIDTH, HEIGHT).move(2, 2)
    screen.blit(text_object_shadow, text_object_shadow.get_rect(center=text_location_shadow.center))

    # Texte principal (Rouge)
    text_object = font.render(text, 0, pygame.Color('red'))
    text_location = pygame.Rect(0, 0, WIDTH, HEIGHT)
    screen.blit(text_object, text_object.get_rect(center=text_location.center))

def menu(screen):
    font = pygame.font.SysFont("Arial", 36, True)
    clock = pygame.time.Clock()

    boutons = {
        "Blancs": pygame.Rect(156, 150, 200, 50),
        "Noirs": pygame.Rect(156, 220, 200, 50),
        "Aléatoire": pygame.Rect(156, 290, 200, 50)
    }

    while True:
        screen.fill((30, 30, 30))

        titre = font.render("PYCHESS PROJECT", True, pygame.Color("white"))
        screen.blit(titre, titre.get_rect(center=(WIDTH//2, 80)))

        for texte, rect in boutons.items():
            pygame.draw.rect(screen, pygame.Color("gray"), rect)
            label = font.render(texte, True, pygame.Color("black"))
            screen.blit(label, label.get_rect(center=rect.center))

        pygame.display.flip()

        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                pygame.quit()
                quit()

            if event.type == pygame.MOUSEBUTTONDOWN:
                pos = pygame.mouse.get_pos()
                for texte, rect in boutons.items():
                    if rect.collidepoint(pos):
                        if texte == "Blancs":
                            return True
                        elif texte == "Noirs":
                            return False
                        else:
                            return random.choice([True, False])

        clock.tick(60)


def main():

    running = True
    game_over = False
    sq_selected = () 
    player_clicks = [] 
    

    pygame.init()
    screen = pygame.display.set_mode((WIDTH, HEIGHT))
    pygame.display.set_caption("PYCHESS PROJECT")
    clock = pygame.time.Clock()
    
    joueur_humain_est_blanc = menu(screen)
    joueur_humain_est_noir = not joueur_humain_est_blanc

    # Si l'humain est Noir, on inverse la vue (flip board)
    inverser_plateau = joueur_humain_est_noir

    game_state = ChessEngine()
    load_images()

    ai_bot = AIPlayer("chess_eval_model.pt") # Assurez-vous que le fichier .pt est là

    running = True


    while running:
        # Vérifier à qui le tour
        tour_humain = (game_state.board.turn == chess.WHITE and joueur_humain_est_blanc) or \
                      (game_state.board.turn == chess.BLACK and joueur_humain_est_noir)

        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                running = False
            
            # --- Gestion SOURIS (CORRIGÉE ET SÉCURISÉE) ---

            elif event.type == pygame.MOUSEBUTTONDOWN and not game_over and tour_humain:
                location = pygame.mouse.get_pos()
                col = location[0] // SQ_SIZE
                row = location[1] // SQ_SIZE
                
                # SÉCURITÉ : On vérifie qu'on a bien cliqué DANS l'échiquier

                if 0 <= col < 8 and 0 <= row < 8:
                    
                    # Conversion du clic en case d'échecs
                    if inverser_plateau:
                        # Logique inversée pour les Noirs
                        sq_clicked_rank = row
                        sq_clicked_file = 7 - col
                    else:
                        # Logique standard pour les Blancs
                        sq_clicked_rank = 7 - row
                        sq_clicked_file = col

                    # On recompose l'index de la case (0-63)
                    # Le try/except empêche le crash si jamais le calcul est faux
                    try:
                        sq_index = chess.square(sq_clicked_file, sq_clicked_rank)
                    except ValueError:
                        continue # On ignore ce clic s'il est buggé

                    # Gestion de la sélection
                    if sq_selected == sq_index:
                        sq_selected = ()
                        player_clicks = []
                    else:
                        sq_selected = sq_index
                        player_clicks.append(sq_selected)

                    
                    if len(player_clicks) == 2:
                        move = chess.Move(player_clicks[0], player_clicks[1])
                        
                        # Promotion automatique (Dame)
                        p = game_state.board.piece_at(player_clicks[0])
                        if p and p.piece_type == chess.PAWN:
                             # Si Pion arrive au bout
                             target_rank = chess.square_rank(player_clicks[1])
                             if target_rank == 0 or target_rank == 7:
                                 move.promotion = chess.QUEEN

                        if game_state.make_move(move):
                            print(f"Humain joue : {move}")
                            sq_selected = ()
                            player_clicks = []
                        else:
                            print("Coup invalide.")
                            player_clicks = [sq_selected]
                        


        # --- Logique de l'IA ---

        if not game_over and not tour_humain:
            pygame.time.wait(100) # Petit temps de réflexion simulé

            # --- NOUVEAU : C'est ici qu'on appelle notre Réseau de Neurones ---
            print("L'IA réfléchit...")
            coup_ia = ai_bot.predict_move(game_state.board)

            if coup_ia:
                game_state.make_move(coup_ia)
                print(f"IA joue : {coup_ia}")


            # Reset sélection joueur
            sq_selected = ()
            player_clicks = []


        # --- DESSIN ---
        draw_board(screen)
        highlight_last_move(screen, game_state.board, inverser_plateau)
        highlight_legal_moves(screen, game_state.board, sq_selected, inverser_plateau)
        draw_pieces(screen, game_state.board, inverse_vue=inverser_plateau)




        # --- GESTION FIN DE PARTIE ---

        if not game_over and game_state.est_terminee():
            game_over = True
            outcome = game_state.board.outcome()
            
            # On initialise la variable par défaut
            texte_fin = "GAME OVER" 

            # On précise la raison si possible
            if outcome:
                if outcome.termination == chess.Termination.CHECKMATE:
                    texte_fin = "CHECKMATE"
                elif outcome.termination == chess.Termination.STALEMATE:
                    texte_fin = "STALEMATE"
                elif outcome.termination == chess.Termination.INSUFFICIENT_MATERIAL:
                    texte_fin = "NOT ENOUGH MATERIAL"
                # Si c'est une autre raison (répétition, 50 coups), ça restera "GAME OVER"
                
                # On ajoute le gagnant
                if outcome.winner is not None:
                    gagnant = "White" if outcome.winner == chess.WHITE else "Black"
                    texte_fin = f"{gagnant} wins by {texte_fin}"
                else:
                    texte_fin = f"Draw by {texte_fin}"

            draw_end_game_text(screen, texte_fin)

        pygame.display.flip()
        clock.tick(MAX_FPS)

    pygame.quit()

if __name__ == "__main__":
    main()