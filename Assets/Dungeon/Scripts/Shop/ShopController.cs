using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using ShopCont.Model;
using ShopCont.UI;
using UnityEngine.Events;
using InventoryCont.Model;
using UnityEngine.UI;

// Control Shop pop up menu for player
//namespace ShopCont{
public class ShopController : MonoBehaviour
{

    public ShopManager shopUI; 
    public GoldShop goldShopUI;
    public ItemSO shopData;
    //public bool canInteract = false;
    public GameObject player;

    public void Start(){
        player = GameObject.FindWithTag("Player");
        // player.gameObject.SetActive(true);
        shopUI.gameObject.SetActive(false);
        goldShopUI.gameObject.SetActive(false);
    }


    //user pressing P to open shop
    // public void Update() // function is called once per frame
    // {
    //     if (Input.GetKeyDown(KeyCode.P))
    //     {
    //         shopUI.gameObject.SetActive(!shopUI.gameObject.activeSelf);
    //         player.SetActive(!player.activeSelf);
    //         // player.gameObject.SetActive(!player.gameObject.activeSelf);
    //         //if Shop is loaded when P is pressed then hide it, & vice versa
    //         //mouseFollower.Toggle(!mouseFollower);
    //         // player.gameObject.SetActive(!player.gameObject.activeSelf);
    
    //     }

    // }

    public void BuySeeds(){
        shopUI.gameObject.SetActive(true);
        goldShopUI.gameObject.SetActive(false);
            
    }
    public void SellCrops(){
        goldShopUI.gameObject.SetActive(true);
        shopUI.gameObject.SetActive(false);
    }




    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player"){
            shopUI.gameObject.SetActive(!shopUI.gameObject.activeSelf);
            //player.SetActive(!player.activeSelf);
            //canInteract = true;
        } 
        
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player"){
            shopUI.gameObject.SetActive(!shopUI.gameObject.activeSelf);
            //player.SetActive(!player.activeSelf);
        } 
    
    }



}


//}
