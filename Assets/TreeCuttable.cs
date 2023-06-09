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

    private BoxCollider2D boxCollider;

    private void Awake()
    {
        // Get the BoxCollider2D component attached to the game object
        boxCollider = GetComponent<BoxCollider2D>();

        if (boxCollider == null)
        {
            Debug.LogError("BoxCollider2D component not found!");
        }
    }


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
        while (dropCount >= 0 && (plant_regrow_time <= 0)) //checks if the object has items to drop and rend.sprite == grownPlant
        {
            dropCount -= 1;
            Vector3 position = transform.position;
            position.x += spread * UnityEngine.Random.value / 2;
            position.y += spread * UnityEngine.Random.value / 2;

            GameObject go = Instantiate(pickUpDrop);
            go.transform.position = position;

            if (dropCount == 0)
                Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (plant_regrow_time > 0)
        {
            plant_regrow_time -= Time.deltaTime; //start deprecating time
            time_to_sprout = plant_regrow_time; //checks the time left for plant to regrow

            rend.sprite = babyPlant;
        }

        if (plant_regrow_time < 0)
        {
            plant_regrow_time = 0;
        }

        if (plant_regrow_time == 0)
        {
            rend.sprite = grownPlant;
            //dropCount = 5; //reset the ammount of objects within the tree
            //plant_regrow_time = 60; //1 min for tree to regrow
        }

        if (rend.sprite == babyPlant)
        {
            boxCollider.enabled = false;
        }
        else
            boxCollider.enabled = true;
    }
}