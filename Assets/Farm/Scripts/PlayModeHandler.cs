using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class PlayModeHandler : MonoBehaviour
{
    private GameObject[] item;
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

    private void Start(){
        item = GameObject.FindGameObjectsWithTag("Item");
    }

    private void Update(){
        if (!EditorApplication.isPlaying && EditorApplication.isPlayingOrWillChangePlaymode)
        {
            // Game is about to exit play mode
            ResetItems();
        }
    }

    
     private void ResetItems()
     { 

        // foreach(GameObject item in items){
        //     item.SetActive(true);
        //     item.Quantity = 1;
        //     //GetComponent<Collider2D>().enabled = true;
        // }

        for(int i = 0; i < item.Length; i++){
            //Item quantity = new Item();
            item[i].gameObject.SetActive(true);
            Item quantity = item[i].GetComponent<Item>();
            quantity.Quantity=1;
            // Item reset = item[i].GetComponent<Item>();
            // if (itemComponent != null){
            //     itemComponent.Quantity = 1;
            // }
            //may need to set collider to active
        }
        
     }

}
