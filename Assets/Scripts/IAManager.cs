using UnityEngine;

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

    public int Run(/*HexBlock list, int player*/)
    {
        int[] values = new int[51];
        // values[49] = player;
        // values[50] = 1;

        return 0;
    }

    public void Learn()
    {

    }

}
