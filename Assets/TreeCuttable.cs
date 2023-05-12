using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCuttable : ToolHit
{
    [SerializeField] GameObject pickUpDrop;
    [SerializeField] int dropCount = 5;
    [SerializeField] float spread = 0.7f;

    [SerializeField] Sprite grownPlant;
    [SerializeField] Sprite babyPlant;

    [SerializeField] private float ttl_min = 0f; //minimum time for the plant to regrow
    [SerializeField] private float ttl_max = 0f; //maximum time for the plant to regrow

    private float plant_regrow_time; //regrow time of each plant

    [SerializeField] private float time_to_sprout;

    private SpriteRenderer rend;

   
    private void GenerateRandomNumber()
    {
        plant_regrow_time = Random.Range(ttl_min, ttl_max + 1);
    }

    private void Start()
    {
        GenerateRandomNumber();
        rend = GetComponent<SpriteRenderer>(); //get the value of the current sprite, idk im just assuming
        time_to_sprout = plant_regrow_time;
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
        if (dropCount == 0)
        {
            plant_regrow_time -= Time.deltaTime; //start deprecating time
            time_to_sprout = plant_regrow_time;

            rend.sprite = babyPlant;

            if (plant_regrow_time < 0)
            {
                rend.sprite = grownPlant;
                dropCount = 5; //reset the ammount of objects within the tree
                plant_regrow_time = 60; //1 min for tree to regrow
            }
        }
    }
}
