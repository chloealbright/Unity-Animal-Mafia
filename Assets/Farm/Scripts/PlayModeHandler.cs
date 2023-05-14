using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class PlayModeHandler : MonoBehaviour
{
    GameObject[] items;
    Item itemsToReset;

    private void Start(){
        items = GameObject.FindGameObjectsWithTag("Item");

    }
    
    // static PlayModeHandler(){
    //     EditorApplication.PlayModeStateChange += StateChange; 
    // }

    // private void StateChange(PlayModeStateChange state){
    //     switch (state)
    //     {
    //         case PlayModeStateChange.EnteredPlayMode:
    //             Debug.Log("play enter");
    //             break;
    //         case PlayModeStateChange.ExitingPlayMode:
    //             Debug.Log("play exit");
    //             ResetItems();
    //             break;
    //         default: return;
    //     }
    // }

    private void Update(){
        if (EditorApplication.isPlaying && EditorApplication.isPlayingOrWillChangePlaymode) //&& EditorApplication.isPlayingOrWillChangePlaymode
        {
            // Game is about to exit play mode

            itemsToReset.ResetItems(items);
        }
    }

}
