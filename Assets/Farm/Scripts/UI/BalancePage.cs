using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BalancePage : MonoBehaviour
{
    public TMP_Text goldUI; // gold amount
    [field: SerializeField]
    public int gold { get; set; } = 100;
    
    // Start is called before the first frame update
    private void Awake()
    {
        Hide();
        goldUI.text = "Gold Balance: " + gold.ToString();
        
    }
    public void Purchase(int cost){
        gold -= cost;

    }

    public void Sell(int price){
        gold -= price;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
