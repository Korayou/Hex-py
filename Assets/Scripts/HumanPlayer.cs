using System.Threading.Tasks;

public class HumanPlayer : IPLayer
{
    public Team Team { get; set; }
    
    private TaskCompletionSource<(int,int)> _currentPlayerTcs;
    
    
    public (int, int) GetInput(HexBlock[,] hexagons)
    {
        var task = WaitForInput();
        task.Wait();
        return task.Result;
    }


    private Task<(int, int)> WaitForInput()
    {
        _currentPlayerTcs = new TaskCompletionSource<(int, int)>();
        return _currentPlayerTcs.Task;
    }

    public void SetInput(int x, int y)
    {
        _currentPlayerTcs.SetResult((x, y));
    }
}