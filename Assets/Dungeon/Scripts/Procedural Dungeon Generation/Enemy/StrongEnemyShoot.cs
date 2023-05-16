using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongEnemyShoot : MonoBehaviour
{
    public GameObject hitEffect;
    public float movementSpeed = 10f;
    public float lifespan = 3f;
    private float timer;
    private Transform player;
    private Vector3 direction;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        direction = (player.position - transform.position).normalized;
        timer = 0f;
    }

    private void Update()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * movementSpeed * Time.deltaTime;
        if (timer >= lifespan)
        {
            DestroyBullet();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
            Destroy(gameObject);
        }
        else
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
            Destroy(gameObject);
        }
    }
    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
