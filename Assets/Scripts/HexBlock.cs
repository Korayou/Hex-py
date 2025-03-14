using UnityEngine.UI;

public class HexBlock
{
    public Button Button { get; set; }
    public HexBlockType Type { get; set; }
}

public enum HexBlockType
{
    Empty = 0,
    Red = 1,
    Blue = -1
}