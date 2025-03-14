using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject hexagonPrefab;
    [SerializeField] private float hexagonRadius;

    private Button[,] hexagons = new Button[7, 7];

    private void Start()
    {
        for (var i = -3; i < 4; i++)
        for (var j = -3; j < 4; j++)
        {
            var position = (Vector2.right * j + new Vector2(0.5f, -Mathf.Cos(-Mathf.PI / 6)) * i) * hexagonRadius;
            var hexagon = Instantiate(hexagonPrefab, position, Quaternion.identity, transform)
                .GetComponentInChildren<Button>();
            hexagon.name = $"Hexagon ({i}, {j})";
            hexagons[i + 3, j + 3] = hexagon;
        }
    }
}