using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTile : MonoBehaviour
{
    private CorridorFirstDungeonGenerator dungeonGenerator;
    private ScoreManager scoreManager;
    private LevelManager levelManager;

    public GameObject fadeOutPanel;
    public float fadeWait;

    private int currentLevel = 1;

    private void Start()
    {
        dungeonGenerator = FindObjectOfType<CorridorFirstDungeonGenerator>();
        scoreManager = FindObjectOfType<ScoreManager>();
        levelManager = FindObjectOfType<LevelManager>();

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
                scoreManager.AddScore(100);
                levelManager.AddLevel(1);
                dungeonGenerator.CorridorFirstGeneration();
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

    //private void IncrementLevel()
    //{
    //    currentLevel++;
    //    Debug.Log("Current Level: " + currentLevel);
    //    PlayerPrefs.SetInt("Level", currentLevel);
    //}
    public int GetCurrentLevel()
    {
        return currentLevel;
    }
}
