import chess
import random
import torch
from NN_model import ChessEvalNet, board_to_tensor


"""
Docstring for Minimax
"""



class OptimizedAI:
    def __init__(self, model_path="chess_eval_model.pt", device=None):
        self.device = device or ('cuda' if torch.cuda.is_available() else 'cpu')
        self.model = ChessEvalNet()
        self.model.load_state_dict(torch.load(model_path, map_location=self.device))
        self.model.to(self.device)
        self.model.eval()
        
        self.transposition_table = {}

    def evaluate_board(self, board):
        """Évalue la position via le NN, avec table de transposition"""
        key = board.fen()
        if key in self.transposition_table:
            return self.transposition_table[key]

        tensor = torch.from_numpy(board_to_tensor(board)).float().unsqueeze(0).to(self.device)
        with torch.no_grad():
            score = self.model(tensor).item()

        self.transposition_table[key] = score
        return score

    def minimax(self, board, depth, alpha, beta, maximizing):
        if depth == 0 or board.is_game_over():
            if board.is_checkmate():
                return -9999 if maximizing else 9999
            return self.evaluate_board(board)

        #tri des coups : captures, promotions, checks...
        moves = list(board.legal_moves)
        moves.sort(key=lambda m: (
            board.is_capture(m) or
            board.is_check() or
            m.promotion is not None
        ), reverse=True)

        if maximizing:
            max_eval = -float('inf')
            for move in moves:
                board.push(move)
                eval = self.minimax(board, depth-1, alpha, beta, False)
                board.pop()
                max_eval = max(max_eval, eval)
                alpha = max(alpha, eval)
                if beta <= alpha:
                    break
            return max_eval
        else:
            min_eval = float('inf')
            for move in moves:
                board.push(move)
                eval = self.minimax(board, depth-1, alpha, beta, True)
                board.pop()
                min_eval = min(min_eval, eval)
                beta = min(beta, eval)
                if beta <= alpha:
                    break
            return min_eval

    def find_best_move(self, board, depth=4):
        maximizing = board.turn == chess.WHITE
        best_move = None
        best_value = -float('inf') if maximizing else float('inf')

        #tris coups racine
        moves = list(board.legal_moves)
        moves.sort(key=lambda m: (board.is_capture(m) or m.promotion is not None), reverse=True)

        for move in moves:
            board.push(move)
            value = self.minimax(board, depth-1, -float('inf'), float('inf'), not maximizing)
            board.pop()

            if maximizing and value > best_value:
                best_value = value
                best_move = move
            elif not maximizing and value < best_value:
                best_value = value
                best_move = move

        return best_move

