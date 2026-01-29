import pygame
import chess
import random
import torch
import torch.nn as nn
import numpy as np
from torch.utils.data import TensorDataset, DataLoader




class ChessEngine:
    def __init__(self):
        """Initialise une nouvelle partie"""
        self.board = chess.Board()


    def reset(self):
        """Remet le plateau à l'état initial"""
        self.board.reset()


    def est_terminee(self):
        """Retourne True si la partie est finie (mat, pat, etc.)"""
        return self.board.is_game_over()


    def make_move(self, move):
        """
        Joue un coup. Accepte soit un objet chess.Move, soit une string 
        Retourne True si le coup est valide, False sinon.
        """
        if isinstance(move, str):
            # Tente de convertir la string en coup valide
            try:
                # On essaie d'abord la notation algébrique (ex: "Nf3")
                move_obj = self.board.parse_san(move)
            except ValueError:
                try:
                    # Sinon on essaie la notation UCI (ex: "g1f3")
                    move_obj = self.board.parse_uci(move)
                except ValueError:
                    return False
        else:
            move_obj = move

        # Vérification finale de légalité
        if move_obj in self.board.legal_moves:
            self.board.push(move_obj)
            return True
        return False
    

    def undo_move(self):
        """Annule le dernier coup joué."""
        if len(self.board.move_stack) > 0:
            self.board.pop()
        else:
            print("Aucun coup à annuler.")


    def obtenir_resultat(self):
        """Retourne le résultat de la partie ou None si en cours."""
        if self.est_terminee():
            outcome = self.board.outcome()
            result_map = {
                chess.WHITE: "Victoire des Blancs",
                chess.BLACK: "Victoire des Noirs",
                None: "Match Nul"
            }
            # outcome.winner est True (Blancs), False (Noirs) ou None (Nul)
            winner_text = result_map.get(outcome.winner)
            reason = outcome.termination.name  # Ex: CHECKMATE, STALEMATE
            return f"{winner_text} ({reason})"
        return "Partie en cours"


    def afficher(self):
        """Affiche le plateau dans la console."""
        print(f"\nTrait aux : {'Blancs' if self.board.turn == chess.WHITE else 'Noirs'}")
        print(self.board)
        print("-" * 20)


    def lister_coups_legaux(self):
        """Retourne une liste de strings des coups possibles (ex: ['e3', 'e4', ...])"""
        #on convertit les objets Move en strings lisibles
        return [self.board.san(move) for move in self.board.legal_moves]
    
"""
#tests ------
if __name__ == "__main__":
    jeu = ChessEngine()
    
    print("Bienvenue dans le ChessEngine v1.0")
    jeu.afficher()

    # Petite boucle de jeu interactive pour tester la classe
    while not jeu.est_terminee():
        print(f"Coups possibles : {jeu.lister_coups_legaux()[:5]} ...") # On en montre 5
        user_input = input("Entrez votre coup : ")
        
        if user_input.lower() == "quit":
            break
            
        succes = jeu.jouer_coup(user_input)
        
        if succes:
            jeu.afficher()
        else:
            print("Coup invalide ou illégal")

    print("Fin de partie :", jeu.obtenir_resultat())

"""
