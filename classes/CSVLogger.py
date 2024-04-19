import numpy as np
import datetime
import os

class CSVLogger:
    dataset: np.ndarray

    def __init__(self, board_size: int):
        self.dataset = np.zeros(shape=(0, (board_size ** 2) + 2), dtype=np.uint8)

    def add_row(self, board: np.ndarray, player: int):
        self.dataset = np.vstack([self.dataset, np.append(board.flatten(), np.array([player, 0]))])
    
    def set_winner(self, player: int):
        for row in self.dataset:
            if row[-2] == player:
                row[-1] = 1

        self.write_file()

    def write_file(self):
        folder_path = "games_log"

        if not os.path.exists(folder_path):
            os.makedirs(folder_path)

        current_datetime = datetime.datetime.now().strftime("%Y%m%d_%H%M%S")
        file_path = os.path.join(folder_path, f"dataset_{current_datetime}.csv")
        np.savetxt(file_path, self.dataset, delimiter=",", fmt="%i")
