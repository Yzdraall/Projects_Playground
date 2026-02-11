# Project Playtime: Portfolio

This repository documents my personal work in Artificial Intelligence and Simulation. 
As an engineering student at Polytech Nantes (Data & IA), I use this project to experiment with Reinforcement Learning and Algorithmic Search concepts, moving beyond theory to build functional systems from scratch.


## 1. Active Ragdolls (Unity & Reinforcement Learning)

The goal of this project was to train a physics-based bipedal agent to walk without using any pre-recorded animations. The agent learns locomotion solely through trial and error using Deep Reinforcement Learning.

### Technical Implementation
- **Environment:** Built in Unity 3D with the ML-Agents toolkit.
- **Algorithm:** Proximal Policy Optimization (PPO) mlagents tools.
- **Observation Space:** The agent perceives its environment through vector observations (joint angles, velocities) and raycasts (ground detection).
- **Reward Shaping:** I designed a custom reward function to balance multiple objectives:
  - Maximizing forward velocity.
  - Maintaining head stability (minimizing vertical oscillation).
  - Energy efficiency (penalizing excessive torque usage).


## 2. Chess Engine (Python & Algorithms)

A chess engine developed entirely in Python. The objective was to master the fundamentals of game theory and search algorithm and optimization.

### Technical Implementation
- **Board Representation:** Custom implementation of board state management.
- **Search Algorithm:** Implementation of the Minimax algorithm.
- **Optimization:** Added Alpha-Beta pruning to significantly reduce the number of nodes evaluated, allowing for deeper search depth in real-time.
- **Evaluation Function:** Heuristic evaluation based on material count and piece-square tables (positional advantages).


## 3. Tectonic Grid Solver & Generator (Computer Vision & AI)

An end-to-end application capable of solving logic puzzles (Tectonic) from a simple photograph. I was responsible for the global architecture, the Graphical User Interface (GUI), and the Optical Character Recognition (OCR) pipeline.

### Technical Implementation
- **Computer Vision (OpenCV):** Implemented an image processing pipeline to detect grid contours, correct perspective (homography), and segment individual cells and irregular zones from a raw image.
- **OCR System (PyTorch):** Designed and trained a lightweight Convolutional Neural Network (CNN) to recognize digits within the grid. To overcome the lack of labeled data, I developed a generator for synthetic training data (`PrintedMNIST`) simulating various fonts, rotations, and noise.
- **Procedural Generation:** Developed an algorithm to create infinite playable levels. It uses a randomized flood-fill technique to build irregular zones and leverages the SAT Solver to validate the solvability of each generated grid in real-time.
- **Integration & UI (Pygame):** Built the main interface allowing users to import images, edit the grid state, and visualize the solving process. The system orchestrates calls to a SAT Solver (MiniSat) for instant resolution.
