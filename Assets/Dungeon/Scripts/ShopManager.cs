using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//generate coins, a check purchaseable, config data and reference data for scene
public class ShopManager : MonoBehaviour
{
    public int gold;
    public TMP_Text goldUI;

    void Start(){
        goldUI.text = "Gold: " + gold.ToString();

    }

    public void AddGold(){
        gold++;
        goldUI.text = "Gold" + gold.ToString();
        //CheckPurchaseable();
    }

    //user pressing S to open shop
    public void Update() // function is called once per frame
    {
        // if (Input.GetKeyDown(KeyCode.S))
        // {
        //     if (inventoryUI.isActiveAndEnabled == false)
        //     {
        //         inventoryUI.Show();
        //         foreach (var item in inventoryData.GetCurrentInventoryState())
        //         {
        //             inventoryUI.UpdateData(item.Key,
        //                 item.Value.item.ItemImage,
        //                 item.Value.quantity);
        //         }
        //     }
        //     else
        //     {
        //         inventoryUI.Hide();
        //     }
        // }

    }
}
