using System.Collections;
using System.Collections.Generic;
using InventoryCont.Model;
using UnityEngine;

public class InventoryChecker : MonoBehaviour
{
    [SerializeField]
    private InventorySO inventoryData;
    
    public bool HasItem(string item_name)
    {
        //int check_ID = (inventoryData.inventoryItemStructs[0].item.ID);
        //Debug.Log("ID of the item in the inventory: " + check_ID);

        //Debug.Log("Name of Item: " + item_Name);

        int inventory_Size = (inventoryData.inventoryItemStructs.Count);
        //Debug.Log("Size of Inventory: " + inventory_Size);

        for(int pos = 0; pos < inventory_Size; pos++)
        {
            string item_Name = (inventoryData.inventoryItemStructs[pos].item.Name);

            if (item_Name == item_name)
                return true;
        }
        return false;
    }


    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.M))
    //    {
    //        bool ans = HasItem("Carrot Seed");
    //        Debug.Log(ans);
            
    //    }
    //}
}

