using UnityEngine;

public class ExitTile : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dungeonGenerator.CorridorFirstGeneration();
        }
    }
}
