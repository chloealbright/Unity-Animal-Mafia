using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTile : MonoBehaviour
{
    private CorridorFirstDungeonGenerator dungeonGenerator;
    private ScoreManager scoreManager;

    public GameObject fadeOutPanel;
    public float fadeWait;

    private void Start()
    {
        dungeonGenerator = FindObjectOfType<CorridorFirstDungeonGenerator>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && dungeonGenerator != null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                StartCoroutine(FadeController());

                playerObject.transform.position = new Vector3(0f, 0f, playerObject.transform.position.z);
                TilemapVisualizer tilemapVisualizer = FindObjectOfType<TilemapVisualizer>();
                tilemapVisualizer.Clear();
                dungeonGenerator.CorridorFirstGeneration();
                scoreManager.AddScore(100);
            }
        }
    }

    public IEnumerator FadeController()
    {
        if (fadeOutPanel != null)
        {
            GameObject panel = Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
            Destroy(panel, 1);
        }
        yield return new WaitForSeconds(fadeWait);
    }
}
