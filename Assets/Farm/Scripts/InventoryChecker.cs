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

    public int RemoveItem(string name, int quan)
    {
        int inventory_Size = (inventoryData.inventoryItemStructs.Count);

        int current_Quantity = -2;

        for (int pos = 0; pos < inventory_Size; pos++)
        {
            //make sure we are not looking at something that is null
            if (inventoryData.inventoryItemStructs[pos].item != null &&
                inventoryData.inventoryItemStructs[pos].item.Name == name)
            {
                //Debug.Log("current Quantity: " + current_Quantity);
                //depricate quantity by 1

                current_Quantity = inventoryData.inventoryItemStructs[pos].quantity;

                //Debug.Log("Quantity before pressing M: " + current_Quantity);

                inventoryData.inventoryItemStructs[pos] =
                    inventoryData.inventoryItemStructs[pos].ChangeQuantity(current_Quantity - 1);

                current_Quantity = inventoryData.inventoryItemStructs[pos].quantity;

                //Debug.Log("Quantity after pressing M: " + current_Quantity);

                //Debug.Log("current Quantity: " + inventoryData.inventoryItemStructs[pos].quantity);
                //if that current quantity is less than 0 we want to remove item
                if (current_Quantity <= 0)
                {
                    inventoryData.inventoryItemStructs[pos].DeleteItem();
                    inventoryData.InformAboutChange();
                    Debug.Log("here");
                }
            }
        }
        return current_Quantity;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            int ans = RemoveItem("Carrot Seed", 1);
            //Debug.Log("inventory pos " + ans);

        }
    }
}

