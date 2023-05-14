using UnityEngine;

public class ExitTile : MonoBehaviour
{
    private CorridorFirstDungeonGenerator dungeonGenerator;

    private void Start()
    {
        dungeonGenerator = FindObjectOfType<CorridorFirstDungeonGenerator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && dungeonGenerator != null)
        {
            Debug.Log(dungeonGenerator);
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                playerObject.transform.position = new Vector3(0f, 0f, playerObject.transform.position.z);
                TilemapVisualizer tilemapVisualizer = FindObjectOfType<TilemapVisualizer>();
                Debug.Log(tilemapVisualizer);
                if (tilemapVisualizer != null)
                {
                    tilemapVisualizer.Clear();
                }
                dungeonGenerator.CorridorFirstGeneration();
            }
        }
    }

}
