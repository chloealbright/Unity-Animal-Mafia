using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using InventoryCont.UI;
using UnityEngine;

namespace InventoryCont.Model
{
    //stores info of how many items is in the inv
    [CreateAssetMenu]
    public class InventorySO : ScriptableObject
    {
        [SerializeField]
        public List<InventoryItemStruct> inventoryItemStructs;

        [field: SerializeField]
        public int Size { get; private set; } = 10;

        //for the state of inventory, informs about change
        public event Action<Dictionary<int, InventoryItemStruct>> OnInventoryUpdated;

        public void Initialize()
        {
            inventoryItemStructs = new List<InventoryItemStruct>();
            for (int i = 0; i < Size; i++)
            {
                inventoryItemStructs.Add(InventoryItemStruct.GetEmptyItem());
            }
        }
        public int AddItem(ItemSO item, int quantity)
        {
            //if item is not stackable, it will add to inventory in the next empty slot
            if (item.IsStackable == false)
            {
                for (int i = 0; i < inventoryItemStructs.Count; i++)
                {
                    while (quantity > 0 && IsInventoryFull() == false)
                    {
                        quantity -= AddItemToFirstFreeSlot(item, 1);
                    }
                    InformAboutChange();
                    return quantity;
                }
            }
            quantity = AddStackableItem(item, quantity);
            InformAboutChange();
            return quantity;

        }

        //adds item to first available slot
        private int AddItemToFirstFreeSlot(ItemSO item, int quantity)
        {
            InventoryItemStruct newItem = new InventoryItemStruct
            {
                item = item,
                quantity = quantity
            };
            for (int i = 0; i < inventoryItemStructs.Count; i++)
            {
                if (inventoryItemStructs[i].isEmpty)
                {
                    inventoryItemStructs[i] = newItem;
                    return quantity;
                }
            }
            return 0;
        }
        //checks  through all the items in the inventory and grabs the empty ones
        //if there are no empty slots, it means the inventory is full
        private bool IsInventoryFull()
            => inventoryItemStructs.Where(item => item.isEmpty).Any() == false;

        private int AddStackableItem(ItemSO item, int quantity)
        {
            for (int i = 0; i < inventoryItemStructs.Count; i++)
            {
                if (inventoryItemStructs[i].isEmpty)
                    continue;
                if (inventoryItemStructs[i].item.ID == item.ID) //if it has the same item ID
                {
                    //checking the amount allowed to take from stack before the slot becomes full
                    int amountPossibleToTake =
                        inventoryItemStructs[i].item.MaxStackSize - inventoryItemStructs[i].quantity;
                    //if there are more items then what we can put into the slot, would set the slot to max, maxing out the slot
                    //modifying the quantity from the parameter, so that we can put items into a new slot
                    if (quantity > amountPossibleToTake)
                    {
                        inventoryItemStructs[i] = inventoryItemStructs[i].ChangeQuantity(inventoryItemStructs[i].item.MaxStackSize);
                        quantity -= amountPossibleToTake;
                    }
                    //fill in items we need to stack
                    else
                    {
                        inventoryItemStructs[i] = inventoryItemStructs[i].ChangeQuantity(inventoryItemStructs[i].quantity + quantity);
                        InformAboutChange();
                        return 0;
                    }
                }
            }
            while (quantity > 0 && IsInventoryFull() == false)
            {
                int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize);
                quantity -= newQuantity;
                AddItemToFirstFreeSlot(item, newQuantity);
            }
            return quantity;
        }

        public void AddItem(InventoryItemStruct item)
        {
            AddItem(item.item, item.quantity);
        }

        // use this to update inventory class through inventory controller
        public Dictionary<int, InventoryItemStruct> GetCurrentInventoryState()
        {
            Dictionary<int, InventoryItemStruct> returnValue = new Dictionary<int, InventoryItemStruct>();
            for (int i = 0; i < inventoryItemStructs.Count; i++)
            {
                if (inventoryItemStructs[i].isEmpty)
                    continue;
                returnValue[i] = inventoryItemStructs[i];
            }
            return returnValue;
        }

        public InventoryItemStruct GetItemAt(int itemIndex)
        {
            return inventoryItemStructs[itemIndex];
        }

        public void SwapItems(int itemIndex1, int itemIndex2)
        {
            InventoryItemStruct item1 = inventoryItemStructs[itemIndex1];
            inventoryItemStructs[itemIndex1] = inventoryItemStructs[itemIndex2];
            inventoryItemStructs[itemIndex2] = item1;
            InformAboutChange();
        }

        public void InformAboutChange()
        {
            //check if something is assigned
            OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
        }

        //shermol added
        public void DeleteItem(int position)
        {
            inventoryItemStructs[position] = InventoryItemStruct.GetEmptyItem();
            InformAboutChange();
        }

    }

    //using struct so that it is easier to modify, in this case it is easier to modify the quantity value
    [Serializable]
    public struct InventoryItemStruct
    {
        public int quantity;
        public ItemSO item;
        public bool isEmpty => item == null;

        public InventoryItemStruct ChangeQuantity(int newQuantity)
        {
            return new InventoryItemStruct
            {
                item = this.item,
                quantity = newQuantity,
            };
        }

        public static InventoryItemStruct GetEmptyItem() => new InventoryItemStruct
        {
            item = null,
            quantity = 0,
        };
    }
}
