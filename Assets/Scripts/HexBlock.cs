using UnityEngine.UI;

public class HexBlock
{
    public Button Button { get; set; }
    public Team Type { get; set; }
}

public enum Team
{
    None = 0,
    Red = 1,
    Blue = -1
}