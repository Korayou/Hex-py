public interface IPLayer
{
    (int, int) GetInput(HexBlock[,] hexagons);
    
    Team Team { get; set; }
}