using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int level = 0;
    public TMP_Text levelText;

    private void Start()
    {
        PlayerPrefs.SetInt("Level", level);
        UpdateLevelText();
    }

    public void AddLevel(int levelToAdd)
    {
        level += levelToAdd;
        Debug.Log(level);
        PlayerPrefs.SetInt("Level", level);
        UpdateLevelText();
    }

    private void UpdateLevelText()
    {
        level = PlayerPrefs.GetInt("Level");
        levelText.text = "Level: " + level.ToString();
    }
}
