using InventoryCont.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : MonoBehaviour
{
    [field: SerializeField]
    public ItemSO InventoryItem { get; private set; }

    [field: SerializeField]
    public int Quantity { get; set; } = 1;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private float duration = 0.3f;

    private GameObject[] items;
    
    private void Start()
    {
        items = GameObject.FindGameObjectsWithTag("Item");
        GetComponent<SpriteRenderer>().sprite = InventoryItem.ItemImage;
    }


    

    //void OnEnable() => collectedItems = new List<Item>();
    /*
    void On Awake() => itemCollection.Changed += updateData();
    void On Destroy() => itemCollection.Changed -= updateData();
    void On enable scene change update the collected items
    void OnEnable() => updateData();
    void onn Validate() => data = GetComponent<SpriteRenderer>().sprite; 

    */
    

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
        Destroy(gameObject);
    }
}
