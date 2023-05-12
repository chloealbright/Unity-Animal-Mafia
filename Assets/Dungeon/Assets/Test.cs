using System;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
  public TMP_Text text;


  [DllImport("__Internal")]
  public static extern void GetJSON(string path, string objectName, string callback, string fallback);

  private void Start()
  {
    GetJSON("example", gameObject.name, "OnRequestSuccess", "OnRequestFailed");
  }

  private void OnRequestSuccess(string data)
  {
    text.color = Color.green;
    text.text = data;

  }
  private void OnRequestFailed(string error)
  {
    text.color = Color.red;
    text.text = error;

  }
}
