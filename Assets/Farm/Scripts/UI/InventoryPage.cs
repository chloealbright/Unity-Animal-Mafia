using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPage : MonoBehaviour
{
    [SerializeField]
    private InventoryItem itemPrefab;

    [SerializeField]
    private RectTransform contentPanel;

    [SerializeField]
    private InventoryDescription itemDescription;

    [SerializeField]
    private MouseFollower mouseFollower;

    List<InventoryItem> listOfItems = new List<InventoryItem>();

    public Sprite image, image2;
    public int quantity;
    public string title, description;

    private int currentlyDraggedItemIndex = -1;

    private void Awake()
    {
        Hide();
        mouseFollower.Toggle(false);
        itemDescription.ResetDescription();
    }

    public void InitializeInventoryUI(int inventorysize)
    {
        for (int i = 0; i < inventorysize; i++) 
        {
            InventoryItem item = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            item.transform.SetParent(contentPanel);
            listOfItems.Add(item);
            item.OnItemClicked += HandleItemSelection;
            item.OnItemBeginDrag += HandleBeginDrag;
            item.OnItemDroppedOn += HandleSwap;
            item.OnItemEndDrag += HandleEndDrag;
            item.OnRightMouseBtnClick += HandleShowItemActions;
        }
    }

    private void HandleShowItemActions(InventoryItem inventoryItemUI)
    {
    }

    private void HandleEndDrag(InventoryItem inventoryItemUI)
    {
        mouseFollower.Toggle(false);
    }

    private void HandleSwap(InventoryItem inventoryItemUI)
    {
        int index = listOfItems.IndexOf(inventoryItemUI);
        if (index == -1)
        {
            mouseFollower.Toggle(false);
            currentlyDraggedItemIndex = -1;
            return;
        }
        listOfItems[currentlyDraggedItemIndex].SetData(index == 0 ? image : image2, quantity);
        listOfItems[index].SetData(currentlyDraggedItemIndex == 0 ? image : image2, quantity);
        mouseFollower.Toggle(false);
        currentlyDraggedItemIndex = -1;
    }

    private void HandleBeginDrag(InventoryItem inventoryItemUI)
    {
        int index = listOfItems.IndexOf(inventoryItemUI);
        if (index == -1)
            return;
        currentlyDraggedItemIndex = index;
        mouseFollower.Toggle(true);
        mouseFollower.SetData(index == 0? image : image2, quantity);
    }

    private void HandleItemSelection(InventoryItem inventoryItemUI)
    {
        itemDescription.SetDescription(image, title, description);
        listOfItems[0].Select();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        itemDescription.ResetDescription();

        listOfItems[0].SetData(image, quantity);
        listOfItems[1].SetData(image2, quantity);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
