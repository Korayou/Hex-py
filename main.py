import logging

from rich import print
from rich.logging import RichHandler

FORMAT = "%(message)s"
logging.basicConfig(level="NOTSET", format=FORMAT, datefmt="[%X]", handlers=[RichHandler()])

from classes.tournament import Tournament


def main(args):
    arena = Tournament(args)

    if MODE is "cpu_vs_cpu":
        arena.championship()
    if MODE is "man_vs_cpu":
        arena.single_game(blue_starts=True)


if __name__ == "__main__":
    BOARD_SIZE = 7
    ITERMAX = 1000
    MODE = "man_vs_cpu"
    GAME_COUNT, N_GAMES = 0, 200

    if MODE == "man_vs_cpu":
        log = logging.getLogger("rich")

        print()
        log.info("You will be playing as the BLUE player!")
        log.warning("No Pie Rule not implemented yet!")
        print()

    args = BOARD_SIZE, ITERMAX, MODE, GAME_COUNT, N_GAMES
    main(args)
