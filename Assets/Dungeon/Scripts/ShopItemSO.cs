using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ShopMenu", menuName = "Scriptable objects/New Shop Item", order = 1)]
public class ShopItemSO : ScriptableObject //S.O-> reference & use data without needing to assign it to a game object
{
    public string title;
    public string description;
    public int cost; //use then convert to string as text
    public Sprite itemImage;
    //public Image itemImage;
    public bool isStackable;
    public int MaxStackSize;

}