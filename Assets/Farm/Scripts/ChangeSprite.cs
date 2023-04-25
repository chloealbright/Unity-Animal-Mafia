using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    [SerializeField] float ttl = 10f; //time to leave

    private SpriteRenderer rend;
    private Sprite sapling, grownTree;

    //use this for initialization
    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        //Console.WriteLine("current Line");
        Sprite grownTree = Resources.Load<Sprite>("Assets/Farm/Trees/grownTree");
        Sprite sapling = Resources.Load<Sprite>("Assets/Farm/Trees/sapling");

        //rend.sprite = grownTree;
    }

    private void Update()
    {
        ttl -= Time.deltaTime; //decrement time
        if (ttl < 0)
        {
            if (rend.sprite == grownTree)
                rend.sprite = sapling;
            else if (rend.sprite == sapling)
                rend.sprite = grownTree;
        }

    }
}
