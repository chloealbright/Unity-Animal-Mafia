using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEditor;

public class SaveSystem : MonoBehaviour
{
    GameObject[] items;
    // Item itemsToReset;
    private Scene originalScene;
    //private static GameObject instance;
    void Start()
    {
        // Store the original scene when the game object is created
        originalScene = gameObject.scene;
        items = GameObject.FindGameObjectsWithTag("Item");
        
    }

    // void Update(){
    //     if (EditorApplication.isPlaying && EditorApplication.isPlayingOrWillChangePlaymode) //&& EditorApplication.isPlayingOrWillChangePlaymode
    //     {
    //         // Game is about to exit play mode
    //         Debug.Log("Game is about to exit play mode");

    //         itemsToReset.ResetItems(items);
    //     }
    // }
    void Update(){//called every frame / ensures that the items are reset as soon as the game enters the play mode
    //not called during scene transitions
        if (EditorApplication.isPlaying && EditorApplication.isPlayingOrWillChangePlaymode) //&& EditorApplication.isPlayingOrWillChangePlaymode
        {
            // Game is about to exit play mode
            Debug.Log("Game is about to exit play mode");

            // Reset all the Item game objects
            foreach(GameObject item in items){
                Item itemScript = item.GetComponent<Item>();
                if (itemScript != null){
                    itemScript.ResetItems(items);
                }
            }
        }
    }

    /*
    not called when entering play mode. Without resetting the items in Update(), 
    the items won't reset until the game exits play mode
    */
    void OnSceneUnloaded(Scene scene){// when the player transitions to a different scene
        if(scene.buildIndex != originalScene.buildIndex){
            // Reset all the Item game objects
            foreach(GameObject item in items){
                Item itemScript = item.GetComponent<Item>();
                if (itemScript != null){
                    itemScript.ResetItems(items);
                }
            }

            SaveSystem[] saveSystems = Object.FindObjectsOfType<SaveSystem>();
            for(int i = 0; i < saveSystems.Length; i++){
                if (saveSystems[i] != this && saveSystems[i].gameObject.name == gameObject.name){
                    Destroy(gameObject);
                    return;
                }
            }
            DontDestroyOnLoad(gameObject);
            // // Destroy this SaveSystem script if it's not in the original scene
            // Destroy(gameObject);
        }
    }

    
    void OnDestroy()
    {
        // Unregister the OnSceneUnloaded method as a delegate for the SceneManager.sceneUnloaded event
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    

    
}
