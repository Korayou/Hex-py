import numpy as np
import datetime
import os

class CSVLogger:
    nb_cases: int
    dataset: np.ndarray
    header: list[str]

    def __init__(self, board_size: int):
        self.nb_cases = board_size ** 2
        self.dataset = np.zeros(shape=(0, (self.nb_cases) * 2 + 2), dtype=np.uint8)
        self.header = []

        for i in range(self.nb_cases):
            self.header.append(f"Case_{i}")

        self.header.append("Joueur")
        self.header.append("Victoire")

        for i in range(self.nb_cases):
            self.header.append(f"Case_{i}_score")

    def add_row(self, board: np.ndarray, player: int, case_jouee: int):
        scores: list[int] = [0] * (self.nb_cases)

        for i in range(board.size):
            if case_jouee == i:
                scores[i] = 1
        
        # We add a new row : current board, player, winner, scores
        self.dataset = np.vstack([self.dataset, np.append(board.flatten(), np.append([player, 0], np.array(scores)))])

    def set_winner(self, player: int):
        for row in self.dataset:
            if row[-(self.nb_cases + 2)] == player:
                row[-(self.nb_cases + 1)] = 1

        self.write_file()

    def write_file(self):
        folder_path = "games_log"

        if not os.path.exists(folder_path):
            os.makedirs(folder_path)

        current_datetime = datetime.datetime.now().strftime("%Y%m%d_%H%M%S")
        file_path = os.path.join(folder_path, f"game_{current_datetime}.csv")
        np.savetxt(file_path, self.dataset, delimiter=";", fmt="%i", header=";".join(self.header), comments="")
