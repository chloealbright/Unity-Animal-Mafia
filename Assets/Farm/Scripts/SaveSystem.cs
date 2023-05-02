using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        for (int i = 0; i < Object.FindObjectsOfType<SaveSystem>().Length; i++)
        {
            if (Object.FindObjectsOfType<SaveSystem>()[i] != this)
            {
                if (Object.FindObjectsOfType<SaveSystem>()[i].name == gameObject.name)
                {
                    Destroy(gameObject);
                }

            }
        }
        DontDestroyOnLoad(gameObject);
    }
}
