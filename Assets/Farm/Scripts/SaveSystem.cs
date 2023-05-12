using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SaveSystem : MonoBehaviour
{
    //private static GameObject instance;
    void Start()
    {
        //DontDestroyOnLoad(gameObject);
        //if (instance == null)
        //    instance = gameObject;
        //else
        //    Destroy(gameObject);
        for (int i = 0; i < Object.FindObjectsOfType<SaveSystem>().Length; i++){
            if (Object.FindObjectsOfType<SaveSystem>()[i] != this){
                if (Object.FindObjectsOfType<SaveSystem>()[i].name == gameObject.name){
                    Destroy(gameObject);
                }
            }
        }
        DontDestroyOnLoad(gameObject);
        // Find the EventSystem GameObject in the Farm scene and turn it off
        // GameObject farmEventSystem = GameObject.Find("Inventory Event");
        // EventSystem eventSystem = farmEventSystem.GetComponent<EventSystem>();
        // Subscribe to the SceneManager.sceneLoaded event
        //SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // // Function to be called when the scene is loaded
    // void OnSceneLoaded(Scene scene, LoadSceneMode mode){
    //     if (scene.name == "Barn"){
    //         if (farmEventSystem != null){
    //             if (eventSystem != null){
    //                 eventSystem.gameObject.SetActive(false);
    //                 //turning off event system to avoid duplicate events error
    //                 //alternative solution is Destroy(farmEventSystem);
    //                 /*
    //                 // Find the EventSystem GameObject in the Farm scene and destroy it
    //                 GameObject farmEventSystem = GameObject.Find("Inventory Event");
    //                 if (farmEventSystem != null)
    //                 {
    //                     Destroy(farmEventSystem);
    //                 }
    //                 */
    //             }
    //         }
    //     }
    //     if (scene.name == "Farm"){
    //         if (farmEventSystem != null){
    //             if (eventSystem != null){
    //                 eventSystem.gameObject.SetActive(true);   
    //             }
    //         }
    //     }
    // }
}
