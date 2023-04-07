using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    public int sceneBuildIndex;

    //level move zoned enter, if collider is a player
    //move game to another scene

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Trigger Entered");
        //can use other.GetComponent<Player>() to see if the game object has a player component
        //tags work too
        if(other.tag == "Player")
        {
            //player entered, so move level
            print("Switching scene to" + sceneBuildIndex);
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        }
    }
}
