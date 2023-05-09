using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using InventoryCont.Model;
using ShopCont.UI;

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
        public TMP_Text goldUI; // gold amount
        public int gold;
        
        public ShopController shopController;
        
        

        

        //private sellMouseFollower MouseFollower;



        public void Awake(){
            //MouseFollower.Toggle(false)
            for(int i =0; i< sellItemsSO.Length; i++){
                sellPanelsGO[i].SetActive(true);
            }
            goldUI.text = "Gold: " + gold.ToString();
            LoadPanels();
            CheckSellable();
        }



        public void AddGold(){
            //MouseFollower.Toggle(true)
            Debug.Log("Generate Gold");
            gold+=20;
            goldUI.text = "Gold: " + gold.ToString();
            CheckSellable();
        }

        public void CheckSellable(){
            Debug.Log("sellContent.activeInHierarchy");
            for(int i=0; i< sellItemsSO.Length; i++){
                if(inventoryData.ContainsItem(sellItemsSO[i]))
                    sellBtn[i].interactable = true;
                else
                    sellBtn[i].interactable = false;
            }
        }

        public void SellItem(int btnNo){
            int ItemsLeft;
            if(inventoryData.ContainsItem(sellItemsSO[btnNo])){
                Debug.Log("Sell item: " + sellItemsSO[btnNo].Name);
                ItemsLeft = inventoryData.RemoveItem(sellItemsSO[btnNo], 1);

                if(ItemsLeft == 0){
                    gold += sellItemsSO[btnNo].Cost;
                    goldUI.text = "Gold: "+ gold.ToString();
                    CheckSellable();
                }
            }            
        }

        public void BuySeeds(){
            shopController.BuySeeds();
        }

        // public void BuySeeds(){
        //     ShopUI.gameObject.SetActive(true);
        //     goldShopUI.gameObject.SetActive(false);
                
        // }

        


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
