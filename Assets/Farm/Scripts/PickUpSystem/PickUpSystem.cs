using InventoryCont.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField]
    private InventorySO inventoryData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.GetComponent<Item>();
        if (item != null)
        {
            int reminder = inventoryData.AddItem(item.InventoryItem, item.ID, item.Quantity); //Modified for data persistency
            if (reminder == 0)
                item.DestroyItem(); //sets game object to false 
            else
                item.Quantity = reminder;
        }
    }
}
