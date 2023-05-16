using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEditor;
using InventoryCont;
using InventoryCont.UI;

public class SaveSystem : MonoBehaviour
{
    InventoryController InvCont;
    private InventoryPage InvPage; 
    public GameObject InventoryUI;
    private Scene originalScene;
    private static SaveSystem instance;
    //called once per scene if the script is attached to a game object in each scene
    void Awake() //i.e ensure only one instance of the script exists across all scenes
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    

    
    //private static GameObject instance;
    void Start()
    {
        // Store the original scene when the game object is created
        originalScene = gameObject.scene;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
         SaveSystem[] saveSystems = Object.FindObjectsOfType<SaveSystem>();
        for(int i = 0; i < saveSystems.Length; i++){
            if (saveSystems[i] != this && saveSystems[i].gameObject.name == gameObject.name){
                this.gameObject.SetActive(false);//turn off duplicates of the gameObject
                //this ensures persistency across all scenes
                
                Debug.Log("SaveSys: found duplicates of SaveSystem");
            }
        }
        DontDestroyOnLoad(gameObject);

        
    }

    /*
    not called when entering play mode. Without resetting the items in Update(), 
    the items won't reset until the game exits play mode
    */
    void OnSceneUnloaded(Scene scene){// when the player transitions to a different scene
        if(scene.buildIndex != originalScene.buildIndex){
            if(SceneManager.GetSceneByBuildIndex(originalScene.buildIndex).IsValid()){
                Debug.Log("SaveSys: buildIndex != original");
                Destroy(gameObject);
                
            }
            // DON'T reset game objects because this will add them back during game play

            

            // SaveSystem[] saveSystems = Object.FindObjectsOfType<SaveSystem>();
            // for(int i = 0; i < saveSystems.Length; i++){
            //     if (saveSystems[i] != this && saveSystems[i].gameObject.name == gameObject.name){
            //         this.gameObject.SetActive(false);//turn off duplicates of the gameObject
            //           //this ensures persistency across all scenes
            //         //return;
            //         Debug.Log("SaveSys: found duplicates of SaveSystem");
            //     }
            // }
            DontDestroyOnLoad(gameObject);
            // // Destroy this SaveSystem script if it's not in the original scene
            // Destroy(gameObject);
        }
        if(scene.buildIndex == originalScene.buildIndex){
            Debug.Log("SaveSys: buildIndex == original");
            DontDestroyOnLoad(gameObject);
            // // Destroy this SaveSystem script if it's not in the original scene
            // Destroy(gameObject);
        }
    }  

    // private void OnApplicationQuit()
    // {
    //     InvPage = InvCont.inventoryUI;
    //     // Reset the InventoryUI when exiting play mode
    //     if (!Application.isPlaying)
    //     {
    //         InvPage.ClearInventory(InvPage);
    //     }
    // }

    

    
}
