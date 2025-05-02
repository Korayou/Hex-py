using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class GameLogger
{
    private string GameLogPath = "gamelogs";
    private CSV<int> log;

    public GameLogger(string name)
    {
        log = new CSV<int>(GameLogPath + "/" + name);
        log.DirectoryName = GameLogPath;
    }

    public void LogTurn(HexBlock[,] list, int player, (int,int) move)
    {
        List<int> tableau = ConvertTableau(list);
        tableau.Add(player);
        tableau.Add(-1/*Placeholder*/);
        int index = move.Item1 * 7 + move.Item2;
        for (int i = 0; i < 49; i++)
        {
            if (i == index) tableau.Add(1);
            else tableau.Add(0);
        }

        log.WriteLine(tableau);
    }

    public void GameEnd(int winner)
    {
        List<List<int>> fulllog = log.ReadAllLines();

        for(int i = 0; i < fulllog.Count; i++)
        {
            List<int> ligne = fulllog[i];
            if (ligne[49] == winner) ligne[50] = 1;
            else ligne[50] = 0;
        }

        log.DeleteFile();
        log.CreateFile();

        for (int i = 0; i < fulllog.Count; i++)
            log.WriteLine(fulllog[i]);
    }

    private List<int> ConvertTableau(HexBlock[,] list)
    {
        List<int> values = new List<int>();

        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                values.Add((int)list[i, j].Type);
            }
        }
        return values;
    }

}
