using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class GameLogger
{
    private string GameLogPath = "gamelogs";
    private CSV<int> log;

    public GameLogger(string name)
    {
        log = new CSV<int>(GameLogPath + "/" + name);
    }

    public void LogTurn(HexBlock[,] list, int player, int move)
    {

    }

    public void GameEnd(int winner)
    {

    }
}
