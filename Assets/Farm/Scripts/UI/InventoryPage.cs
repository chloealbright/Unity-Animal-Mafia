using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryCont.UI
{
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

        private int currentlyDraggedItemIndex = -1;

        public event Action<int> OnDescriptionRequested, OnItemActionRequested, OnStartDragging;

        public event Action<int, int> OnSwapItems;

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

        internal void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description)
        {
            itemDescription.SetDescription(itemImage, name, description);
            DeselectAllItems();
            listOfItems[itemIndex].Select();
        }

        public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
        {
            if (listOfItems.Count > itemIndex)
            {
                listOfItems[itemIndex].SetData(itemImage, itemQuantity);
            }
        }

        private void HandleShowItemActions(InventoryItem inventoryItemUI)
        {
        }

        private void HandleEndDrag(InventoryItem inventoryItemUI)
        {
            ResetDraggedItem();
        }

        private void HandleSwap(InventoryItem inventoryItemUI)
        {
            int index = listOfItems.IndexOf(inventoryItemUI);
            if (index == -1)
            {
                return;
            }
            OnSwapItems?.Invoke(currentlyDraggedItemIndex, index);
            HandleItemSelection(inventoryItemUI);
        }

        private void ResetDraggedItem()
        {
            mouseFollower.Toggle(false);
            currentlyDraggedItemIndex = -1;
        }

        private void HandleBeginDrag(InventoryItem inventoryItemUI)
        {
            int index = listOfItems.IndexOf(inventoryItemUI);
            if (index == -1)
                return;
            currentlyDraggedItemIndex = index;
            HandleItemSelection(inventoryItemUI);
            OnStartDragging?.Invoke(index);
        }

        public void CreateDraggedItem(Sprite sprite, int quantity)
        {
            mouseFollower.Toggle(true);
            mouseFollower.SetData(sprite, quantity);
        }

        private void HandleItemSelection(InventoryItem inventoryItemUI)
        {
            int index = listOfItems.IndexOf(inventoryItemUI);
            if (index == -1)
                return;
            OnDescriptionRequested.Invoke(index);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            ResetSelection();
        }

        public void ResetSelection()
        {
            itemDescription.ResetDescription();
            DeselectAllItems();
        }

        private void DeselectAllItems()
        {
            foreach (InventoryItem item in listOfItems)
            {
                item.Deselect();
            }
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            ResetDraggedItem();
        }

        public void ClearInventory(InventoryPage page){
            page.ResetAllItems();
        }

        internal void ResetAllItems()
        {
            foreach (var item in listOfItems)
            {
                item.ResetData();
                item.Deselect();
            }
        }
    }
}