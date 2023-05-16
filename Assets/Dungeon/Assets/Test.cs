using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using InventoryCont.Model;

public class Test : MonoBehaviour
{
  [SerializeField] public TMP_Text text;

  public List<ItemSO> items = new List<ItemSO>(); // a list to store the items

  [DllImport("__Internal")]
  public static extern void GetJSON(string path, string objectName, string callback, string fallback);

  private void Start(){
    GetJSON("Gold", gameObject.name, "OnRequestSuccess", "OnRequestFailed");
  }



 private void OnRequestSuccess(string data)
    {
        // parse the received data into ItemSO objects and add them to the list
        items = ParseJSON(data);

        text.color = Color.green;
        text.text = data;
    }

    private void OnRequestFailed(string error)
    {
        text.color = Color.red;
        text.text = error;
    }

    private List<ItemSO> ParseJSON(string jsonData)
    {
      // Parse the JSON data into an array of JSON objects
      var jsonArray = JsonUtility.FromJson<JsonWrapper>($"{{\"array\":{jsonData}}}").array;
    
      //Added: 
      // Create a new list to store the parsed items
      List<ItemSO> parsedItems = new List<ItemSO>();
    
      // Loop through each JSON object in the array
      foreach (var jsonObject in jsonArray)
      {
        // Create a new ItemSO object and set its properties based on the JSON object
          ItemSO item = ScriptableObject.CreateInstance<ItemSO>();
          // item.Name = jsonObject["name"].ToString();
          // item.Quantity = jsonObject["quantity"].ToString();
          item.Name = jsonObject.name;
          item.Quantity = jsonObject.quantity;


          // Add the new ItemSO object to the itemList
          // items.Add(item);

          //Added
          // Add the new ItemSO object to the parsedItems list
          parsedItems.Add(item);
      }

      // return items;
      //Added
      return parsedItems;
    }


// Define a wrapper class to help parse the JSON data into an array
[Serializable]
public class JsonWrapper
{
  public ItemJson[] array;
}

// Define a class to represent the JSON data for each item
[Serializable]
public class ItemJson
{
    public string name;
    public int quantity;
    //...
}



}



