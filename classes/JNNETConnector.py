import numpy as np
import subprocess as sub

AI_ENGINE_PATH: str = "MLPerceptron.jar"
AI_MLP_PATH: str = "model.mlp"

class JNNETConnector:
    def __init__(self, iFile: str = "input.csv", oFile: str = "output.csv"):
        self.iFile = iFile
        self.oFile = oFile

    def sendData(self, data: np.ndarray):
        np.savetxt(self.iFile, [data], delimiter=";", fmt="%i")

        print("sending...")
        sub.run(["java", "-jar", AI_ENGINE_PATH, "-r", "-m", AI_MLP_PATH, "-i", self.iFile, "-o", self.oFile])
        print("sent.")

    def receiveData(self, next: int = 0, possibleMoves: list[int] = []) -> int:
        """Public function that returns the best (or `next`th) move from the neural network."""
        data: list[int] = self._receiveRawData()
        #processed: list[int] = self._processRawData(data)
        print(possibleMoves)
        #print(processed)
        filtered: list[float] = []
        if len(possibleMoves) != 0:
            filtered = [data[i] for i in possibleMoves]
        
        #filtered: list[int] = [i for i in processed if i in possibleMoves or len(possibleMoves) == 0]

        print(data.index(max(filtered)))

        try:
            return data.index(max(filtered))
        except IndexError:
            return -1

    def _receiveRawData(self) -> list[int]:
        """Private function that returns the raw data from the neural network."""
        data: list[str] = []
        with open(self.oFile, 'r') as file:
            data = file.readline().split(';')

        #print(data)
        return [float(i) for i in data]
    
    def _processRawData(self, data: list[int]) -> list[int]:
        """Private function that sorts the list and returns the best moves."""
        return sorted(data, reverse=True)
