using InventoryCont.Model;
using InventoryCont.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryCont
{
    public class InventoryController : MonoBehaviour
    {
        //dependency to inv page
        [SerializeField]
        private InventoryPage inventoryUI;

        [SerializeField]
        private InventorySO inventoryData;

        public List<InventoryItemStruct> initialItems = new List<InventoryItemStruct>();

        private void Start()
        {
            PrepareUI();
            PrepareInventoryData();
        }

        private void PrepareInventoryData()
        {
            inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            foreach (InventoryItemStruct item in initialItems)
            {
                if (item.isEmpty)
                    continue;
                inventoryData.AddItem(item);
            }
        }

        //updates inventory when performing change
        private void UpdateInventoryUI(Dictionary<int, InventoryItemStruct> inventoryState)
        {
            inventoryUI.ResetAllItems();
            foreach (var item in inventoryState)
            {
                inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
            }
        }

        private void PrepareUI()
        {
            inventoryUI.InitializeInventoryUI(inventoryData.Size);
            inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            inventoryUI.OnSwapItems += HandleSwapItems;
            inventoryUI.OnStartDragging += HandleDragging;
            inventoryUI.OnItemActionRequested += HandleItemActionRequest;
        }

        private void HandleItemActionRequest(int itemIndex)
        {

        }

        private void HandleDragging(int itemIndex)
        {
            InventoryItemStruct inventoryItemStruct = inventoryData.GetItemAt(itemIndex);
            if (inventoryItemStruct.isEmpty)
                return;
            inventoryUI.CreateDraggedItem(inventoryItemStruct.item.ItemImage, inventoryItemStruct.quantity);
        }

        private void HandleSwapItems(int itemIndex1, int itemIndex2)
        {
            inventoryData.SwapItems(itemIndex1, itemIndex2);
        }

        private void HandleDescriptionRequest(int itemIndex)
        {
            InventoryItemStruct inventoryItemStruct = inventoryData.GetItemAt(itemIndex);
            if (inventoryItemStruct.isEmpty)
            {
                inventoryUI.ResetSelection();
                return;
            }
            ItemSO item = inventoryItemStruct.item;
            inventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.name, item.Description);
        }

        //user pressing I to open inv
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (inventoryUI.isActiveAndEnabled == false)
                {
                    inventoryUI.Show();
                    foreach (var item in inventoryData.GetCurrentInventoryState())
                    {
                        inventoryUI.UpdateData(item.Key,
                            item.Value.item.ItemImage,
                            item.Value.quantity);
                    }
                }
                else
                {
                    inventoryUI.Hide();
                }
            }

        }
    }
}