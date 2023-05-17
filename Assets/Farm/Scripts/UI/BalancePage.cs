using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace InventoryCont.Model
{
    public class BalancePage : MonoBehaviour
    {
        [SerializeField]
        public GoldSO gold;
        public TMP_Text goldUI; // gold amount

        public static BalancePage balanceInstance;

        private void Awake(){
            if(balanceInstance == null){
                balanceInstance = this;
                // DontDestroyOnLoad(gameObject);
            }
            // else
            //     Destroy(gameObject);
        }
          
        // Start is called before the first frame update
        //first time = gamer gets 100
        //2nd time = data persistency 
        private void Start()
        {
            if(IsNewUser()== true)
                InitializeBalance();// BEFORE SERIALIZING, THIS IS ALWAYS TRUE
            else
                //do nothing bc serialization 
                //gold.Balance = new GoldSO();
                goldUI.text = "Gold: " + gold.Balance.ToString();

            Hide();
            
        }
      

        public bool IsNewUser()
        {
            // Check if the player's balance is already set
            if (gold.ExistingUser != false)
            {
                // The player's balance is already set, indicating they are not a new user
                return false;
            }
            
            // The player's balance is not set, indicating they are a new user
            return true;
        }

        public void InitializeBalance()
        {
            gold = ScriptableObject.CreateInstance<GoldSO>();
            gold.ExistingUser = true;
            gold.Balance = 100;
            goldUI.text = "Gold: " + gold.Balance.ToString();
        }

        private void UpdateBalance(int newBalance){
            gold.Balance = newBalance;
            //gold.UpdateBalance();
            goldUI.text = "Gold: " + gold.Balance.ToString();

        }

        public int GetPlayerBalance(){//helper function for ShopUIs
            return gold.Balance;
        }

        public void Purchase(int cost){
            gold.Balance -= cost;
            UpdateBalance(gold.Balance);

        }

        public void Sell(int price){
            gold.Balance -= price;
            UpdateBalance(gold.Balance);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

    }
}