using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


// generate gold, later changing this to load balance
// check purchaseable/limits supplies that can be bought, 
// config data and reference data for scene i.e Load Panels
namespace ShopCont.UI{
    public class ShopManager : MonoBehaviour
    {
        public int gold;
        public TMP_Text goldUI; // gold amount
        public ShopItemSO[] shopItemsSO; // Prefab shopItems
        public ShopTemplate[] shopPanels; // Title-Description-Cost, references script
        public GameObject[] shopPanelsGO; // references GameObject
        public Button[] purchaseBtn; // check if purchaseable
        public RectTransform contentPanel;
        public ShopTemplate shopTemplate;

        //private ShopMouseFollower MouseFollower;



        public void Awake(){
            //MouseFollower.Toggle(false)
            for(int i =0; i< shopItemsSO.Length; i++){
                shopPanelsGO[i].SetActive(true);
            }
            goldUI.text = "Gold: " + gold.ToString();
            LoadPanels();
            CheckPurchaseable();
        }



        public void AddGold(){
            //MouseFollower.Toggle(true)
            Debug.Log("Generate Gold");
            gold+=100;
            goldUI.text = "Gold: " + gold.ToString();
            CheckPurchaseable();
        }

        public void CheckPurchaseable(){
            //MouseFollower.Toggle(true);
            Debug.Log("shopContent.activeInHierarchy");
            for(int i=0; i< shopItemsSO.Length; i++){
                if(gold >= shopItemsSO[i].cost)
                    purchaseBtn[i].interactable = true;
                else
                    purchaseBtn[i].interactable = false;
            }
        }

        public void PurchasItem(int btnNo){
            if(gold >= shopItemsSO[btnNo].cost){
                gold = gold - shopItemsSO[btnNo].cost;
                goldUI.text = "Gold: "+ gold.ToString();
                //Next task: Unlock item to set to inventory 
                CheckPurchaseable();
            }
        }

        public void LoadPanels(){
            for(int i=0; i<shopItemsSO.Length; i++){
                
                shopPanels[i].titleTxt.text = shopItemsSO[i].title;
                shopPanels[i].itemImage.sprite = shopItemsSO[i].itemImage; //set panel's item img of sprite to SO image
                shopPanels[i].descriptionTxt.text = shopItemsSO[i].description;
                shopPanels[i].costTxt.text = shopItemsSO[i].cost.ToString() + " Gold";
            }
        }
    }

}




