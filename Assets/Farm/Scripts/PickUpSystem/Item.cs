using InventoryCont.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [field: SerializeField]
    public ItemSO InventoryItem { get; private set; }

    [field: SerializeField]
    public int Quantity { get; set; } = 1;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private float duration = 0.3f;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = InventoryItem.ItemImage;
    }

    public void DestroyItem()
    {
        GetComponent<Collider2D>().enabled = false;
        //StartCoroutine(AnimateItemPickup());
    }

    //private IEnumerator AnimateItemPickup()
    //{
    //    audioSource.Play();
    //    Vector3 startScale = transform.localScale;
    //    Vector3 endScale = Vector3.zero;
    //    float currentTime = 0;
    //    while (currentTime < duration)
    //    {
    //        currentTime += Time.deltaTime;
    //        transform.localScale =
    //            Vector3.Lerp(startScale, endScale, currentTime / duration);
    //        yield return null;
    //    }
    //    Destroy(gameObject);
    //}

    Transform player;
    [SerializeField] float speed = 5f;
    [SerializeField] float pickUpDistance = 1.5f;
    [SerializeField] float ttl = 10f; //time to leave

    float grace_period = 1.5f;

    private void Awake()
    {
        player = playerManager.instance.player.transform;
    }

    private void Update()
    {
        if (grace_period > 0)
        {
            grace_period -= Time.deltaTime;
        }
        else
        {
            ttl -= Time.deltaTime;
            if (ttl < 0)
            {
                audioSource.Play();
                Destroy(gameObject);
            }

            float distance = Vector3.Distance(transform.position, player.position);
            if (distance > pickUpDistance)
            {
                return;
            }
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.position,
                speed * Time.deltaTime
                );

            if (distance < 0.1f)
            {
                Destroy(gameObject);
            }
        }
    }
}