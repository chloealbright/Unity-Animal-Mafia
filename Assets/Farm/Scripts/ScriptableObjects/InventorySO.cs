using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO; //use for filestream in serialization
using UnityEditor; //for #if Unity 

namespace InventoryCont.Model
{
    //stores info of how many items is in the inv
    [CreateAssetMenu]
    public class InventorySO : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField]
        public List<InventoryItemStruct> inventoryItemStructs;

        [field: SerializeField]
        public int Size { get; private set; } = 10;

        //for the state of inventory, informs about change
        public event Action<Dictionary<int, InventoryItemStruct>> OnInventoryUpdated;

        // data persistency vars
        /*Chloe: when the database file is loaded in inventory controller, 
        it will override database location w/a saved version, 
        problem: can't save a SO within an SO
        workaround: manually set the database variable (i.e make it private) 
        and deserialize the SO
        so the JSON utility doesn't save/override the SO when we reload the game
        e.g avoid override during Save() (Json utility to Json)*/
        private ItemsDatabase database; 

        private int DatabaseCount;
         

        public string savePath; //Chloe: can use for multiple inventories/ save to diff locations
        //End data persistency vars

        public void Initialize()
        {
            inventoryItemStructs = new List<InventoryItemStruct>();
            for (int i = 0; i < Size; i++)
            {
                inventoryItemStructs.Add(InventoryItemStruct.GetEmptyItem());
            }
        }
        public int AddItem(ItemSO item, int id, int quantity)
        {
            //if item is not stackable, it will add to inventory in the next empty slot
            if(item.IsStackable == false)
            {
                for (int i = 0; i < inventoryItemStructs.Count; i++)
                {
                    while(quantity > 0 && IsInventoryFull() == false)
                    {
                        quantity -= AddItemToFirstFreeSlot(item, id, 1);
                    }
                    InformAboutChange();
                    return quantity;
                }
            }
            quantity = AddStackableItem(item, id, quantity);
            InformAboutChange();
            return quantity;
            
        }


        //adds item to first available slot 
        private int AddItemToFirstFreeSlot(ItemSO item, int id, int quantity)
        {  //Chloe: changing for database persistency
            InventoryItemStruct newItem = new InventoryItemStruct
            {
                item = item,
                id = id,
                quantity = quantity
                
            };
            for(int i = 0;i < inventoryItemStructs.Count; i++)
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

        private int AddStackableItem(ItemSO item, int id, int quantity)
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
            while(quantity > 0 && IsInventoryFull() == false)
            {
                int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize);
                quantity -= newQuantity;
                AddItemToFirstFreeSlot(item, id, newQuantity);
            }
            return quantity;
        }

        public void AddItem(InventoryItemStruct item)
        {//Chloe: changing for database persistency
            AddItem(item.item, item.id, item.quantity);
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

        private void InformAboutChange()
        {
            //check if something is assigned
            OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
        }

        public void DeleteItem(int position)
        {
            InformAboutChange();
            inventoryItemStructs[position] = InventoryItemStruct.GetEmptyItem();
            InformAboutChange();
        }

        
        //Chloe: adding database persistency
        //ensure the inventory state before the items are serialized
        public void OnBeforeSerialize(){// update item field 
            DatabaseCount=0;
            int inventory_Size = (inventoryItemStructs.Count);
            for (int i = 0; i < inventoryItemStructs.Count; i++)
            {
                DatabaseCount++;
                // inventoryItemStructs[i].item = database.GetItem[inventoryItemStructs[i].id];
                
            }

        }

        public void OnAfterDeserialize() 
        {
            int inventorySize = DatabaseCount;
            //int inventorySize = inventoryItemStructs.Count;
            //int inventorySize = database.items.Length; 
            //int inventorySize = database.inventoryData.Size;
            for (int i = 0; i < inventorySize; i++)
            {
                // Get the inventory item from the database using the ID
                ItemSO item = database.GetItem[inventoryItemStructs[i].id];
                
                // Update the existing inventory item with the retrieved item
                
                
                // Add the updated inventory item to the inventory
                AddItem(item, item.ID, 1);

                // List<InventoryItemStruct> inventoryItemStructs2 = new List<InventoryItemStruct>();
                // //Initialize();
                // //put database id in to retrieve inventory item
                // inventoryItemStructs2[i].item = database.GetItem[inventoryItemStructs2[i].id];
                // AddItem(inventoryItemStructs2[i].item);
                //inventoryItemStructs[i].item = database.GetItem[inventoryItemStructs[i].id];
                
            }
        }

        //NEXT need to save ID and amount to a file for saving and loading
        //after SO serializes the code will repopulate the inventory slot
        public void Save(){
            string saveData = JsonUtility.ToJson(this, true);
            BinaryFormatter bf = new BinaryFormatter();
            //Unity provides Application.persistentDataPath to save files out to a persistent path across multiple devidces
            FileStream file  = File.Create(string.Concat(Application.persistentDataPath, savePath)); //combining string for less garbage collection / memory
            bf.Serialize(file,saveData);
            file.Close();
        }

        //used in Inventory Controller to load the inventory from database
        public bool DatabaseExists(){
            if(File.Exists(string.Concat(Application.persistentDataPath, savePath))){
               return true;
            }
            else{
                return false;
            }
        }

        /*
        on Load 
        if we serialize file & save
        => then the ID = database index
        issue: we can't find the item object by its item.id 
        fix: use the database index to retrieve/restore item on Load

        */

        // open file if it exists for deserialization
        public void Load(){
            if(File.Exists(string.Concat(Application.persistentDataPath, savePath))){
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
                //convert file back to scriptable object
                JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this); //loading back items to inventory object
                file.Close();
            }

        }

        private void OnEnable(){// make sure the database is set up/code doesn't break
        #if UNITY_EDITOR //save in unity editor path when testing 
            database = (ItemsDatabase)AssetDatabase.LoadAssetAtPath("/Assets/Resources/Database.asset", typeof(ItemsDatabase));
        #else   //populate the database during game build from the Resource Folder
            database= Resources.Load<ItemDatabase>("Database");
        #endif
        }

        // public void DeserializeInventoryData(string inventoryDataString)
        // {
        //     if (string.IsNullOrEmpty(inventoryDataString))
        //     {
        //         return;
        //     }

        //     // Deserialize the inventory data string into a dictionary
        //     var inventoryDataDict = JsonUtility.FromJson<Dictionary<string, int>>(inventoryDataString);

        //     // Update the inventory with the deserialized data
        //     foreach (var keyParse in inventoryDataDict)
        //     {
        //         int index = int.Parse(keyParse.Key);
        //         InventoryItemStruct itemStruct = inventoryItemStructs[index];
        //         itemStruct.quantity = keyParse.Value;
        //         itemStruct.isEmpty = false;
        //         inventoryItemStructs[index] = itemStruct;
        //     }

        //     InformAboutChange();
        // }

        // public string SerializeInventoryData()
        // {
        //     // Create a dictionary to store the inventory data
        //     var inventoryDataDict = new Dictionary<string, int>();

        //     // Iterate over the inventory item structs and add non-empty items to the dictionary
        //     for (int i = 0; i < inventoryItemStructs.Count; i++)
        //     {
        //         var itemStruct = inventoryItemStructs[i];
        //         if (!itemStruct.isEmpty)
        //         {
        //             inventoryDataDict.Add(i.ToString(), itemStruct.quantity);
        //         }
        //     }

        //     // Serialize the dictionary into a JSON string
        //     return JsonUtility.ToJson(inventoryDataDict);
        // }
    }

    //using struct so that it is easier to modify, in this case it is easier to modify the quantity value
    [Serializable]
    public struct InventoryItemStruct
    {
        public int quantity;
        public ItemSO item;
        public int id;
        public bool isEmpty => item == null;

        public InventoryItemStruct ChangeQuantity(int newQuantity)
        {
            return new InventoryItemStruct
            {
                item = this.item,
                id = this.item.ID,
                quantity = newQuantity
            };
        }

        public static InventoryItemStruct GetEmptyItem() => new InventoryItemStruct
        {
            item = null,
            id = 0,
            quantity = 0,
        };


    }


}
