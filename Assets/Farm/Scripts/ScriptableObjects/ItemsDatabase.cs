using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventoryCont.Model;
using System.Runtime.Serialization;

//[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/Database" )]

namespace InventoryCont.Model
{
    [CreateAssetMenu]
    public class ItemsDatabase : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField]
        private InventorySO inventoryData;
        
        public ItemSO[] items;

        // public Dictionary<InventoryItemStruct, int> GetId = new Dictionary<InventoryItemStruct, int>();
        // // trade off between performance and memory
        // public Dictionary<int, InventoryItemStruct> GetItem = new Dictionary<int, InventoryItemStruct>();
        public Dictionary<ItemSO, int> GetId = new Dictionary<ItemSO, int>();
        public Dictionary<int, ItemSO> GetItem = new Dictionary<int, ItemSO>();

        // public void OnAfterSerialize(){
        //     GetId = new Dictionary<ItemSO, int>();
        //     GetItem = new Dictionary<int, ItemSO>();
        //     int inventory_Size = (inventoryData.inventoryItemStructs.Count);
        //     for (int i = 0; i < inventory_Size; i++)
        //     {
        //         GetId.Add(Items[i], i);
        //         GetItem.Add(i, Items[i]);
        //     }

        // }

        

        public void OnBeforeSerialize(){
            //GetId = new Dictionary<InventoryItemStruct, int>();
            GetId = new Dictionary<ItemSO, int>();
            int inventory_Size = (inventoryData.inventoryItemStructs.Count);
            for (int i = 0; i < inventory_Size; i++)
            {
                GetId.Add(items[i], 1);

            }

        }

        public void OnAfterDeserialize()
        {
            // GetId = new Dictionary<InventoryItemStruct, int>();
            // GetItem = new Dictionary<int, InventoryItemStruct>();
            GetId = new Dictionary<ItemSO, int>();
            GetItem = new Dictionary<int, ItemSO>();
            int inventory_Size = inventoryData.inventoryItemStructs.Count;
            for (int i = 0; i < inventory_Size; i++)
            {
                GetId.Add(items[i], i);
                GetItem.Add(i, items[i]);
            }
        }


    }
}