using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject hexagonPrefab;
    [SerializeField] private float hexagonRadius;

    private void Start()
    {
        for (var i = -3; i < 4; i++)
        for (var j = -3; j < 4; j++)
        {
            var position = (Vector2.right * j + new Vector2(0.5f, -Mathf.Cos(-Mathf.PI/6)) * i) * hexagonRadius;
            Instantiate(hexagonPrefab, position, Quaternion.identity, transform);
        }
    }
}