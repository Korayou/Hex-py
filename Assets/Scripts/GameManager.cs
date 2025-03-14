using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject hexagonPrefab;
    [SerializeField] private float hexagonRadius;

    private readonly HexBlock[,] _hexagons = new HexBlock[7, 7];
    
    
    private IPLayer _player1;
    private IPLayer _player2;
    
    
    private bool _isWaitingForPlayer = true;

    private void Start()
    {
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
                Type = HexBlockType.Empty,
                Button = hexagon
            };
            
            hexagon.onClick.AddListener(() => OnHexagonClick(x, y));
        }

        StartCoroutine(nameof(GameLoop));
    }
    
    
    private IEnumerator GameLoop()
    {
        IPLayer currentPlayer = null;
        while (true)
        {
            currentPlayer = currentPlayer == _player1 ? _player2 : _player1;
            
            var input = currentPlayer.GetInput(_hexagons);
            
        }
    }


    private void OnHexagonClick(int x, int y)
    {
        Debug.Log($"Hexagon ({x}, {y}) clicked");
        
    }
}