using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShopCont.UI;
// using ShopMan.Model;
//using System;

public class ShopMouseFollower : MonoBehaviour
{
    public Canvas canvas;

    private ShopManager shop;

    public void Awake(){
        canvas = transform.root.GetComponent<Canvas>();
        shop = GetComponentInChildren<ShopManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
                (RectTransform)canvas.transform, Input.mousePosition, canvas.worldCamera, out position
            );
        transform.position = canvas.transform.TransformPoint(position);
        
    }

    // enable/disables mouse follow
    public void Toggle(bool val)
    {
        Debug.Log($"Item toggled { val }");
        gameObject.SetActive( val );
    }
}
