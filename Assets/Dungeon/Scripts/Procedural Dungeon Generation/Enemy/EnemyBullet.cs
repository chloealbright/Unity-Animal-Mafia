using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameObject hitEffect;
    public float movementSpeed = 10f;

    private Transform player;
    private Vector3 direction;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        direction = (player.position - transform.position).normalized;

        // Set the collision layer of the bullet
        gameObject.layer = LayerMask.NameToLayer("EnemyBullet");
    }

    private void Update()
    {
        // Move the bullet towards the player's position
        transform.position += direction * movementSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 5f);
            Destroy(gameObject);
        }
        else
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 5f);
            Destroy(gameObject);
        }
    }
}
