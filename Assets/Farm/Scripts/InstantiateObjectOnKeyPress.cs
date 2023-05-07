using System.Collections;
using System.Collections.Generic;
using InventoryCont.Model;
using UnityEngine;

public class InstantiateObjectOnKeyPress : MonoBehaviour
{
    // The prefab to instantiate
    [SerializeField] GameObject Carrot_Prefab;
    [SerializeField] GameObject Potato_Prefab;
    [SerializeField] GameObject Tomato_Prefab;
    [SerializeField] GameObject Pumkin_Prefab;
    [SerializeField] GameObject Wheat_Prefab;

    //passing the player as an object to obtain it's location
    [SerializeField] Transform PlayerTransform; // The transform of the player GameObject

    //range for area of the carrot patch;
    [SerializeField] float carrot_patch_x_min;
    [SerializeField] float carrot_patch_x_max;
    [SerializeField] float carrot_patch_y_min;
    [SerializeField] float carrot_patch_y_max;

    //range for the area of the potato patch;
    [SerializeField] float potato_patch_x_min;
    [SerializeField] float potato_patch_x_max;
    [SerializeField] float potato_patch_y_min;
    [SerializeField] float potato_patch_y_max;

    //range for the area of the tomato patch;
    [SerializeField] float tomato_patch_x_min;
    [SerializeField] float tomato_patch_x_max;
    [SerializeField] float tomato_patch_y_min;
    [SerializeField] float tomato_patch_y_max;

    //range for the area of the pumkin patch;
    [SerializeField] float pumkin_patch_x_min;
    [SerializeField] float pumkin_patch_x_max;
    [SerializeField] float pumkin_patch_y_min;
    [SerializeField] float pumkin_patch_y_max;

    //range for the area of the pumkin patch;
    [SerializeField] float wheat_patch_x_min;
    [SerializeField] float wheat_patch_x_max;
    [SerializeField] float wheat_patch_y_min;
    [SerializeField] float wheat_patch_y_max;

    [SerializeField]
    private InventorySO inventoryData;

    public bool HasItem(string item_name)
    {
        //int check_ID = (inventoryData.inventoryItemStructs[0].item.ID);
        //Debug.Log("ID of the item in the inventory: " + check_ID);

        //Debug.Log("Name of Item: " + item_Name);

        int inventory_Size = (inventoryData.inventoryItemStructs.Count);
        //Debug.Log("Size of Inventory: " + inventory_Size);

        for (int pos = 0; pos < inventory_Size; pos++)
        {
            string item_Name = (inventoryData.inventoryItemStructs[pos].item.Name);

            if (item_Name == item_name)
                return true;
        }
        return false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) //player pressed the interaction button
        {
            //check for the player location in order to allow for the player to plant the correct plant

            if(PlayerTransform.position.x <= potato_patch_x_max && PlayerTransform.position.x >= potato_patch_x_min &&
               PlayerTransform.position.y <= potato_patch_y_max && PlayerTransform.position.y >= potato_patch_y_min)
            {
                Vector3 spawnPosition = PlayerTransform.position + (PlayerTransform.forward);

                // Instantiate the object at the calculated position
                Instantiate(Potato_Prefab, spawnPosition, Quaternion.identity);
            }
            else if(PlayerTransform.position.x <= carrot_patch_x_max && PlayerTransform.position.x >= carrot_patch_x_min &&
                    PlayerTransform.position.y <= carrot_patch_y_max && PlayerTransform.position.y >= carrot_patch_y_min &&
                    (HasItem("Carrot Seed") == true))
            {
                Vector3 spawnPosition = PlayerTransform.position + (PlayerTransform.forward);
   
                Instantiate(Carrot_Prefab, spawnPosition, Quaternion.identity);
            }
            else if (PlayerTransform.position.x <= tomato_patch_x_max && PlayerTransform.position.x >= tomato_patch_x_min &&
                    PlayerTransform.position.y <= tomato_patch_y_max && PlayerTransform.position.y >= tomato_patch_y_min)
            {
                Vector3 spawnPosition = PlayerTransform.position + (PlayerTransform.forward);

                Instantiate(Tomato_Prefab, spawnPosition, Quaternion.identity);
            }
            else if (PlayerTransform.position.x <= pumkin_patch_x_max && PlayerTransform.position.x >= pumkin_patch_x_min &&
                    PlayerTransform.position.y <= pumkin_patch_y_max && PlayerTransform.position.y >= pumkin_patch_y_min)
            {
                Vector3 spawnPosition = PlayerTransform.position + (PlayerTransform.forward);

                Instantiate(Pumkin_Prefab, spawnPosition, Quaternion.identity);
            }
            else if(PlayerTransform.position.x <= wheat_patch_x_max && PlayerTransform.position.x >= wheat_patch_x_min &&
                    PlayerTransform.position.y <= wheat_patch_y_max && PlayerTransform.position.y >= wheat_patch_y_min)
            {
                Vector3 spawnPosition = PlayerTransform.position + (PlayerTransform.forward);

                Instantiate(Wheat_Prefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}


