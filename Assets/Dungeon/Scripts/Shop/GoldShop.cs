using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using InventoryCont.Model;


namespace ShopCont.UI{//
    public class GoldShop : MonoBehaviour
    {
        [SerializeField]
        private InventorySO inventoryData;

        public ShopManager shopUI;
        public ItemSO[] sellItemsSO; 
        public ShopTemplate[] sellPanels; // Title-Description-Cost, references script
        public GameObject[] sellPanelsGO; // references GameObject
        public Button[] sellBtn; // check if purchaseable
        public RectTransform contentPanel;
        public ShopTemplate sellTemplate;
        private BalancePage playerBalance; // Used for gold balance
        public TMP_Text goldUI; // gold amount
        public int gold;
        

        InventorySO inventoryItem;


        // public void Awake(){
        //     for(int i =0; i< sellItemsSO.Length; i++){
        //         sellPanelsGO[i].SetActive(true);
        //     }
        // }

        public void Start(){
            for(int i =0; i< sellItemsSO.Length; i++){
                sellPanelsGO[i].SetActive(true);
            }
            playerBalance = BalancePage.balanceInstance;
            // gold = playerBalance.GetPlayerBalance(); // Loads player balance from inventory/ balance script
            // goldUI.text = "Gold: " + gold.ToString();
            LoadPanels();
            // CheckSellable();
        }



        public void LoadBalance(){
            if(playerBalance != null){
                Debug.Log("GoldShop: LoadBalance");
                //gold+=20;
                gold = playerBalance.GetPlayerBalance();
                goldUI.text = "Gold: " + gold.ToString();
                CheckSellable();
            }
            else
                goldUI.text = "Balance unreachable";
        }

        public void CheckSellable(){
            Debug.Log("sellContent.activeInHierarchy");
            for(int i=0; i< sellItemsSO.Length; i++){
                if(HasItem(sellItemsSO[i].Name) == true)
                    sellBtn[i].interactable = true;
                else
                    sellBtn[i].interactable = false;
            }
        }

        public void SellItem(int btnNo){
            int ItemsLeft;
            if(HasItem(sellItemsSO[btnNo].Name) == true){
                Debug.Log("Sell item: " + sellItemsSO[btnNo].Name);
                
                playerBalance.Sell(sellItemsSO[btnNo].Cost);
                gold = playerBalance.GetPlayerBalance();
                ItemsLeft = RemoveItem(sellItemsSO[btnNo].Name, 1);
                // gold += sellItemsSO[btnNo].Cost;
                goldUI.text = "Gold: "+ gold.ToString();
                CheckSellable();
                
            }            
        }

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
                        inventoryData.DeleteItem(pos);

                        Debug.Log("here");
                    }
                }
            }
            return current_Quantity;
        }

        public void BuySeeds(){ // move between ShopUI and GoldShopUI
            shopUI.gameObject.SetActive(true);
            gameObject.SetActive(false);
                
        }

        
        public void LoadPanels(){
            for(int i=0; i<sellItemsSO.Length; i++){   
                sellPanels[i].titleTxt.text = sellItemsSO[i].Name;
                sellPanels[i].itemImage.sprite = sellItemsSO[i].ItemImage; //set panel's item img of sprite to SO image
                sellPanels[i].descriptionTxt.text = sellItemsSO[i].Description;
                sellPanels[i].costTxt.text = sellItemsSO[i].Cost.ToString() + " Gold";

            }
        }
    }
}
