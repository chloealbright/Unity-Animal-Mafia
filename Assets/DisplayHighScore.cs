using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine;

public class DisplayHighScore : MonoBehaviour
{
    private DatabaseAccess databaseAccess;
    // Start is called before the first frame update
    private TextMeshPro highScoreOutPut;
    void Start()
    {
        databaseAccess = GameObject.FindGameObjectWithTag("DatabaseAccess").GetComponent<DatabaseAccess>();
        highScoreOutPut.GetComponentInChildren<TextMeshPro>();
        //Giving some time before invoking method
        Invoke("DisplayHighScoreInTextMesh", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private async void DisplayHighScoreInTextMesh()
    {
        var task = databaseAccess.GetScoresFromDataBase();
        //list of high score objects
        var result = await task;
        var output = "";
        foreach(var score in result)
        {
            output += score.UserName + "Score: " + score.Score;
        }
        highScoreOutPut.text = output;
 
    }
}

