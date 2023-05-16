using InventoryCont.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class Item : MonoBehaviour
{
    [field: SerializeField]
    public ItemSO InventoryItem { get; private set; }

    [field: SerializeField]
    public int ID { get; set; }

    [field: SerializeField]
    public int Quantity { get; set; } = 1;

    [field: SerializeField]
    bool pickedUp = false;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private float duration = 0.3f;

    private Scene originalScene;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = InventoryItem.ItemImage;
        // Store the original scene when the game object is created
        originalScene = gameObject.scene;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    public void DestroyItem()
    {
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(AnimateItemPickup());
    }

    private IEnumerator AnimateItemPickup()
    {
        audioSource.Play();
        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.zero;
        float currentTime = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            transform.localScale =
                Vector3.Lerp(startScale, endScale, currentTime / duration);
            yield return null;
        }
        gameObject.SetActive(false);
        pickedUp = true;

        //Destroy(gameObject);
        Debug.Log("Item.cs: gameObject destroyed on AnimatePickup");
    }

    void OnSceneUnloaded(Scene scene){
        //if item was picked up previously, keep it disabled 
        if(scene.buildIndex == originalScene.buildIndex && pickedUp == true){
            gameObject.SetActive(false);
            Debug.Log("Item.cs: buildIndex == originalScene && pickedUp == true, gameObject = false");

        }

    }

    private void OnApplicationQuit(){
        if(EditorApplication.isPlaying && EditorApplication.isPlayingOrWillChangePlaymode){ //if player is is in play mode or about to switch to it
            Debug.Log("Item.cs: Game is about to exit play mode reset gameObj");
            ResetItem(gameObject);

        }
    }
    

    private void ResetItem(GameObject item){ 
        
        item.SetActive(true);
        item.GetComponent<SpriteRenderer>().enabled = true;
        item.GetComponent<Collider2D>().enabled = true;
        pickedUp = false;
        
        // Item resetItem = item.GetComponent<Item>();
        // resetItem.Quantity = 1;
        // item.SetActive(true);
        // resetItem.GetComponent<Collider2D>().enabled = true;
        Debug.Log("Item.cs: ResetItem on OnApplicationQuit");
    }
}
