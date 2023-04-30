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
    public ShopItemSO[] shopItemsSO;
    public ShopTemplate[] shopPanels; //Title-Description-Cost, references script
    public GameObject[] shopPanelsGO; // references GameObject

    

    void Start(){
        for(int i =0; i< shopItemsSO.Length; i++){
            shopPanelsGO[i].SetActive(true);
        }
        goldUI.text = "Gold: " + gold.ToString();

        LoadPanels();
    }

    public void AddGold(){
        gold+=100;
        goldUI.text = "Gold: " + gold.ToString();
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

    public void LoadPanels(){
        for(int i=0; i<shopItemsSO.Length; i++){
            
            shopPanels[i].titleTxt.text = shopItemsSO[i].title;
            shopPanels[i].itemImage.sprite = shopItemsSO[i].itemImage;
            //shopPanels[i].itemImage = shopItemsSO[i].itemImage;
            shopPanels[i].descriptionTxt.text = shopItemsSO[i].description;
            shopPanels[i].costTxt.text = shopItemsSO[i].cost.ToString() + " Gold";
        }
    }
}
