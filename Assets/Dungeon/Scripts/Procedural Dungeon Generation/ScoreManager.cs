using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public TMP_Text scoreText;

    private void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        Debug.Log(score);
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
