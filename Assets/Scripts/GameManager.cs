using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject hexagonPrefab;
    [SerializeField] private float hexagonRadius;

    private readonly HexBlock[,] _hexagons = new HexBlock[7, 7];


    private readonly IPLayer _player1 = new HumanPlayer();
    private readonly IPLayer _player2 = new HumanPlayer();
    
     private IPLayer _currentPlayer;
    

    private void Start()
    {
        _player1.Team = Team.Red;
        _player2.Team = Team.Blue;
        
        for (var i = -3; i < 4; i++)
        for (var j = -3; j < 4; j++)
        {
            var position = (Vector2.right * j + new Vector2(0.5f, -Mathf.Cos(-Mathf.PI / 6)) * i) * hexagonRadius;
            var hexagon = Instantiate(hexagonPrefab, position, Quaternion.identity, transform)
                .GetComponentInChildren<Button>();
            hexagon.name = $"Hexagon ({i}, {j})";

            var (x, y) = (i + 3, j + 3);
            _hexagons[x, y] = new HexBlock
            {
                Type = Team.None,
                Button = hexagon
            };

            hexagon.onClick.AddListener(() => OnHexagonClick(x, y));
        }

        _ = GameLoop();
    }


    private async Awaitable GameLoop()
    {
        while (true)
        {
            await Awaitable.BackgroundThreadAsync();

            _currentPlayer = _currentPlayer == _player1 ? _player2 : _player1;

            var input = _currentPlayer!.GetInput(_hexagons);

            if (_hexagons[input.Item1, input.Item2].Type != Team.None)
            {
                Debug.LogError("Invalid move restart the game");
                return;
            }

            _hexagons[input.Item1, input.Item2].Type =
                _currentPlayer == _player1 ? Team.Red : Team.Blue;

            await Awaitable.MainThreadAsync();
            Display();
        }
    }
    

    private void Display()
    {
        for (var i = 0; i < 7; i++)
        for (var j = 0; j < 7; j++)
        {
            var hexagon = _hexagons[i, j];
            hexagon.Button.interactable = hexagon.Type is Team.None;
            hexagon.Button.GetComponent<Image>().color = hexagon.Type switch
            {
                Team.None => Color.white,
                Team.Red => Color.red,
                Team.Blue => Color.blue,
                _ => Color.white
            };
        }
    }


    private void OnHexagonClick(int x, int y)
    {
        if(_currentPlayer is HumanPlayer humanPlayer)
            humanPlayer.SetInput(x, y);
    }
}