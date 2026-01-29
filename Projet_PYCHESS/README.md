
# PyChess AI - Deep Learning Chess Engine

A hybrid Chess Engine built with **Python**, combining **Deep Learning** (PyTorch) for positional intuition and **Classical Search Algorithms** (Minimax X alpha-beta) for tactical calculation.

The AI "learns" chess patterns by analyzing Master-level games (PGN) and plays through a custom GUI built with **Pygame**.

## Features

* **Deep Neural Network (ResNet)**:
* Evaluates board positions using a Residual Convolutional Neural Network.
* **Input**: 13x8x8 Bitboard representation (Pieces + Turn).
* **Output**: Win/Draw/Loss probability (Classification).
* **Architecture**: 5 Residual Blocks, GroupNorm for stability, LeakyReLU activation.


* **Advanced Search Algorithm**:
* **Minimax** with **Alpha-Beta Pruning**.
* **Quiescence Search**: Mitigates the "Horizon Effect" by continuing analysis during capture sequences.
* **Transposition Table**: Caches previously analyzed positions to optimize search speed.
* **Hybrid Evaluation**: Combines Neural Network positional judgment with Piece-Square Tables for tactical speed.


* **Graphical Interface**:
* Playable GUI using `pygame`.
* Legal move highlighting.
* Drag-and-drop or Click-to-move interface.
* Play as White or Black against the AI.



## Installation

### 1. Prerequisites

Ensure you have **Python 3.10** or higher installed.

### 2. Install Dependencies

Run the following command to install the required libraries:


*(Optional)* If you possess an AMD GPU on Windows, you can install DirectML, although the engine defaults to CPU inference for stability:


### 3. Project Structure

Your project directory should be structured as follows:

```text
/Projet_chess
│
├── Main.py              # The GUI and Game Loop
├── NN_model.py          # Neural Network Architecture & Training Script
├── IA_player.py         # Minimax Search, Alpha-Beta, & Bot Logic
├── Engine.py            # Game State management wrapper
├── chess_eval_model.pt  # The trained model weights (generated after training)
│
├── /games_pgn           # Folder containing .pgn files for training
│   └── data.pgn
│
└── /pieces-png          # Folder containing piece images (wP.png, bK.png, etc.)

```

## Usage

### Training the AI

Before playing, the AI must be trained. You need `.pgn` files (recorded chess games) in the `games_pgn` folder.

1. **Download PGNs**: Acquire a database (e.g., KingBase or CCRL) and place the `.pgn` files in `games_pgn/`.
2. **Run Training**:
Execute the model script. It will parse the games, create a dataset, and train the Neural Network.



* **Step 1**: It parses the PGNs and saves a cache file (`dataset_cache.pkl`).
* **Step 2**: It trains the model for the specified number of epochs.
* **Result**: Saves the weights to `chess_eval_model.pt`.



### Playing the Game

Once `chess_eval_model.pt` exists, you can launch the game.


1. Select your color via the menu (White, Black, or Random).
2. Make your move on the board using the mouse.
3. The AI will analyze the position (progress displayed in the console) and respond.


## Technical Details

### The Neural Network

The architecture is a simplified **ResNet** (Residual Network):

* **Input**: 13 channels (6 piece types x 2 colors + 1 side-to-move plane).
* **Layers**: 3 Convolutional Layers and 5 Residual Blocks using `GroupNorm` and `LeakyReLU`.
* **Output**: A probability distribution `[Black Win, Draw, White Win]`.

### The Search Engine

The AI combines NN evaluation with classical logic:

* **Depth**: Customizable (default is 3 ply).
* **Move Ordering**: Prioritizes captures and promotions to optimize Alpha-Beta pruning efficiency.
* **Safety**: Forces CPU execution for inference to avoid common PyTorch/DirectML synchronization issues on specific hardware (e.g., Intel Iris Xe).

## Troubleshooting

* **Error: `chess_eval_model.pt` not found**:
You skipped the training step. Run `python NN_model.py` first to generate the model file.
* **Training is slow**:
The script forces CPU usage by default for stability. If you have a compatible NVIDIA GPU, verify `torch.cuda.is_available()` returns `True` and adjust the device setting in the script.
* **The AI plays illegal moves**:
Ensure you are using the latest compatible version of the `python-chess` library.

## Credits

* Chess Logic: [Python-Chess](https://python-chess.readthedocs.io/)
* GUI: [Pygame](https://www.pygame.org/)
* Deep Learning: [PyTorch](https://pytorch.org/)