using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using InventoryCont.Model;



// generate gold, later changing this to load balance
// check purchaseable/limits supplies that can be bought, 
// config data and reference data for scene i.e Load Panels
namespace ShopCont.UI{
    public class ShopManager : MonoBehaviour
    {
        public BalancePage playerBalance;
        public int gold;
        public TMP_Text goldUI; // gold amount
        public ItemSO[] shopItemsSO; // Prefab shopItems. CHANGED INSTANTIATION FROM ShopItemSO[]
        public ShopTemplate[] shopPanels; // Title-Description-Cost, references script
        public GameObject[] shopPanelsGO; // references GameObject
        public Button[] purchaseBtn; // check if purchaseable
        public RectTransform contentPanel;
        public ShopTemplate shopTemplate;

        [SerializeField]
        private InventorySO inventoryData;
        //public ShopController shopController;
        public GoldShop goldShopUI;
        
        //private ShopMouseFollower MouseFollower;



        public void Awake(){
            //MouseFollower.Toggle(false)
            for(int i =0; i< shopItemsSO.Length; i++){
                shopPanelsGO[i].SetActive(true);
            }
            gold = playerBalance.gold;
            goldUI.text = "Gold: " + gold.ToString();
            LoadPanels();
            CheckPurchaseable();
        }



        public void GenerateGold(){
            //MouseFollower.Toggle(true)
            Debug.Log("Generate Gold");
            //gold+=20;
            gold = playerBalance.gold;
            goldUI.text = "Gold: " + gold.ToString();
            CheckPurchaseable();
        }

        public void CheckPurchaseable(){
            //MouseFollower.Toggle(true);
            Debug.Log("shopContent.activeInHierarchy");
            gold = playerBalance.gold;
            for(int i=0; i< shopItemsSO.Length; i++){
                if(gold >= shopItemsSO[i].Cost)
                    purchaseBtn[i].interactable = true;
                else
                    purchaseBtn[i].interactable = false;
            }
        }


        public void PurchaseItem(int btnNo){
            gold = playerBalance.gold;
            if(gold >= shopItemsSO[btnNo].Cost){
                Debug.Log("Purchase item: " + shopItemsSO[btnNo].Name + " Cost: "+  shopItemsSO[btnNo].Cost);
                
                playerBalance.Purchase(shopItemsSO[btnNo].Cost);
                gold = playerBalance.gold;
                //gold -= shopItemsSO[btnNo].Cost;

                goldUI.text = "Gold: "+ gold.ToString();
                CheckPurchaseable();
                //Next task: Unlock item to set to inventory 
                Debug.Log("Purchase item: " + shopItemsSO[btnNo].Name);
            
                inventoryData.AddItem(shopItemsSO[btnNo], 1);
                
            }
        }

        // public void SellCrops(){
        //     shopController.SellCrops();
        // }
        public void SellCrops(){
            goldShopUI.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }


        public void LoadPanels(){
            for(int i=0; i<shopItemsSO.Length; i++){   
                shopPanels[i].titleTxt.text = shopItemsSO[i].Name;
                shopPanels[i].itemImage.sprite = shopItemsSO[i].ItemImage; //set panel's item img of sprite to SO image
                shopPanels[i].descriptionTxt.text = shopItemsSO[i].Description;
                shopPanels[i].costTxt.text = shopItemsSO[i].Cost.ToString() + " Gold";

            }
        }
    }

}




