using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCuttable : ToolHit
{
    [SerializeField] Sprite grownPlant;
    [SerializeField] Sprite babyPlant;
    [SerializeField] float ttl = 60f; //time to leave, 1 min for tree to regrow
    private SpriteRenderer rend;


    [SerializeField] GameObject pickUpDrop;
    [SerializeField] int dropCount = 5;
    [SerializeField] float spread = 0.7f;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>(); //get the value of the current sprite, idk im just assuming
    }
    public override void Hit()
    {
        while(dropCount > 0)
        {
            dropCount -= 1;
            Vector3 position = transform.position;
            position.x += spread * UnityEngine.Random.value / 2;
            position.y += spread * UnityEngine.Random.value / 2;

            GameObject go = Instantiate(pickUpDrop);
            go.transform.position = position;
        }

        //Destroy(gameObject);
    }

    private void Update()
    {
        if(dropCount == 0)
        {
            ttl -= Time.deltaTime; //start deprecating time

            rend.sprite = babyPlant;

            if (ttl < 0)
            {
                rend.sprite = grownPlant;
                dropCount = 5; //reset the ammount of objects within the tree
                ttl = 60; //1 min for tree to regrow
            }
        }
    }
}
