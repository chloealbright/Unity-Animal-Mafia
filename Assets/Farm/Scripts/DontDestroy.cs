using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{
    //    DontDestroyOnLoad(gameObject); 
    //}

    private Scene originalScene;

    void Awake()
    {
        // Store the original scene when the game object is created
        originalScene = gameObject.scene;

        // Mark the game object as persistent
        DontDestroyOnLoad(gameObject);

        // Register a callback for the SceneManager.sceneUnloaded event
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    void OnDestroy()
    {
        // Unregister the OnSceneUnloaded method as a delegate for the SceneManager.sceneUnloaded event
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    void OnSceneUnloaded(Scene scene)
    {
        // // If the current scene is not the original scene, remove the game object from the scene
        if (scene.buildIndex != originalScene.buildIndex)
        {
            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
        }
    }
}
