using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class IAManager : IPLayer
{
    private string GameLogPath = "gamelogs";
    
    private string InputPath = "input.csv";
    private string OutputPath = "output.csv";

    private string LearnInputPath = "linput.csv";
    private string LearnOutputPath = "loutput.csv";

    public string IAModel = "model.mlp";

    private IA iaBack = new IA();

    public Team Team { get; set; }

    public IAManager() { }

    public IAManager(string gamelogpath, string inputpath, string outputpath, string learninputpath, string learnoutputpath)
    {
        GameLogPath = gamelogpath;
        InputPath = inputpath;
        OutputPath = outputpath;
        LearnInputPath = learninputpath;
        LearnOutputPath = learnoutputpath;
    }

    public void SetModel(string model)
    {
        IAModel = model;
        iaBack.Model = IAModel;
    }

    public (int, int) GetInput(HexBlock[,] list)
    {
        int cell = Run(list, (int)Team);
        return (cell / 7, cell % 7);
    }

    public int Run(HexBlock[,] list, int player)
    {
        List<int> values = ConvertTableau(list, player);

        CSV<int> inputFile = new CSV<int>(InputPath, ";");
        inputFile.DeleteFile();
        inputFile.CreateFile();
        inputFile.WriteLine(values);

        iaBack.Execute(IA.Behaviour.RUN, InputPath, OutputPath, false);

        CSV<float> outputFile = new CSV<float>(OutputPath, ";");
        List<float> output = outputFile.ReadLine(0);

        int Max_idx = -1;
        float Max_val = -1;
        for(int i = 0; i < values.Count; i++)
        {
            if (values[i] == 0)
            {
                if(output[i] > Max_val)
                {
                    Max_val = output[i];
                    Max_idx = i;
                }
            }
        }
        return Max_idx;
    }

    public void Learn(string gamename)
    {
        string gamePath = GameLogPath + "/" + gamename;

        CSV<int> game = new CSV<int>(gamePath);
        List<List<int>> gameLogs = game.ReadAllLines();


        CSV<int> learnInput = new CSV<int>(LearnInputPath, ";");
        learnInput.DeleteFile();
        learnInput.CreateFile();
        CSV<int> learnOutput = new CSV<int>(LearnOutputPath, ";");
        learnOutput.DeleteFile();
        learnOutput.CreateFile();

        for (int line = 0; line < gameLogs.Count; line++)
        {
            List<int> row = gameLogs[line];

            List<int> output = new List<int>();
            List<int> input = new List<int>();

            int player = row[49];
            int victor = row[50];
            bool won = victor == 1;

            for(int i = 0; i < 49; i++)
            {
                input.Add(row[i]);
            }

            for (int i = 51; i < row.Count; i++)
            {
                if (row[i] == 0)
                {
                    if(won) output.Add(0);
                    else output.Add(1);
                }
                else
                {
                    if (won) output.Add(1);
                    else output.Add(-1);
                }
            }

            input.Add(player);
            input.Add(1);


            learnInput.WriteLine(input);
            learnOutput.WriteLine(output);
        }
        iaBack.Execute(IA.Behaviour.LEARN, LearnInputPath, LearnOutputPath, false);
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
