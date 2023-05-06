using UnityEngine;

public class ExitTile : MonoBehaviour
{
    public CorridorFirstDungeonGenerator dungeonGenerator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dungeonGenerator.CorridorFirstGeneration();
        }
    }
}