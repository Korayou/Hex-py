using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class IAManager
{
    private string GameLogPath = "gamelogs";
    
    private string InputPath = "input.csv";
    private string OutputPath = "output.csv";

    private string LearnInputPath = "linput.csv";
    private string LearnOutputPath = "loutput.csv";

    private IA iaBack = new IA();

    public IAManager() { }

    public IAManager(string gamelogpath, string inputpath, string outputpath, string learninputpath, string learnoutputpath)
    {
        GameLogPath = gamelogpath;
        InputPath = inputpath;
        OutputPath = outputpath;
        LearnInputPath = learninputpath;
        LearnOutputPath = learnoutputpath;
    }

    public int Run(HexBlock[,] list, int player)
    {
        List<int> values = ConvertTableau(list, player);

        CSV<int> inputFile = new CSV<int>(InputPath);
        inputFile.WriteLine(values);

        iaBack.Execute(IA.Behaviour.RUN, InputPath, OutputPath, false);

        CSV<float> outputFile = new CSV<float>(OutputPath);
        List<float> output = outputFile.ReadLine(0);

        return output.IndexOf(output.Max());
    }

    public void Learn(string gamename)
    {
        string gamePath = GameLogPath + "/" + gamename;



    }

    private List<int> ConvertTableau(HexBlock[,] list, int player)
    {
        List<int> values = new List<int>();

        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                values.Add((int)list[i, j].Type);
            }
        }

        values.Add(player);
        values.Add(1); // parce-que

        return values;
    }

}
