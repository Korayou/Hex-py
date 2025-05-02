using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject hexagonPrefab;
    [SerializeField] private float hexagonRadius;

    [SerializeField] private int numberOfGames = 1000;

    private static int numberOfGamesLeft = -1;

    private readonly HexBlock[,] _hexagons = new HexBlock[7, 7];


    private readonly IPLayer _player1 = new IAManager();
    private readonly IPLayer _player2 = new IAManager();

    private IPLayer _currentPlayer;

    private void Start()
    {
        if (numberOfGamesLeft == -1)
            numberOfGamesLeft = numberOfGames;

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
        string GameName = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".csv";
        GameLogger log = new GameLogger(GameName);
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

            _hexagons[input.Item1, input.Item2].Type = _currentPlayer.Team;

            log.LogTurn(_hexagons, (int)_currentPlayer.Team, input);

            await Awaitable.MainThreadAsync();
            Display();
            if (HasWon())
                break;
        }
        await Awaitable.BackgroundThreadAsync();

        Debug.Log($"{_currentPlayer.Team} has won the game, {numberOfGamesLeft} games left");

        log.GameEnd((int)_currentPlayer.Team);

        (_player2 as IAManager)?.Learn(GameName);
        
        await Awaitable.MainThreadAsync();

        if (numberOfGames > 0)
        {
            numberOfGames--;
            SceneManager.LoadScene(0);
        }
        else
            Debug.Log("Games Over");
    }

    private bool HasWon()
    {
        var team = _currentPlayer.Team;

        var visited = new List<(int, int)>();
        var queue = new Queue<(int, int)>();

        var teamVector = team == Team.Red ? new Vector2Int(0, 1) : new Vector2Int(1, 0);

        for (var i = 0; i < 7; i++)
        {
            if (_hexagons[teamVector.x * i, teamVector.y * i].Type == team)
                queue.Enqueue((teamVector.x * i, teamVector.y * i));
        }

        while (queue.Count > 0)
        {
            var (x, y) = queue.Dequeue();

            if (team == Team.Red && x == 6)
                return true;

            if (team == Team.Blue && y == 6)
                return true;

            visited.Add((x, y));

            foreach (var neighbor in GetNeighbors(x, y))
            {
                if (visited.Contains((neighbor.Item1, neighbor.Item2)))
                    continue;

                queue.Enqueue((neighbor.Item1, neighbor.Item2));
            }
        }

        return false;
    }


    /// <summary>
    /// Get all the neighbors of a hexagon that have the same team
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private List<(int, int)> GetNeighbors(int x, int y)
    {
        var neighborsDirections = new[]
        {
            (1, 0),
            (-1, 0),
            (0, 1),
            (0, -1),
            (1, -1),
            (-1, 1)
        };

        var neighbors = new List<(int, int)>();
        var team = _hexagons[x, y].Type;

        foreach (var neighbor in neighborsDirections)
        {
            var (nx, ny) = (x + neighbor.Item1, y + neighbor.Item2);
            if (nx < 0 || nx >= 7 || ny < 0 || ny >= 7)
                continue;

            if (team != _hexagons[nx, ny].Type)
                continue;

            neighbors.Add((nx, ny));
        }

        return neighbors;
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
        if (_currentPlayer is HumanPlayer humanPlayer)
            humanPlayer.SetInput(x, y);
    }
}