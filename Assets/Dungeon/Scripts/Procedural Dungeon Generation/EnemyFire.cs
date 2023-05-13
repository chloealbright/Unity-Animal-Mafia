using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public Transform firePoint;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float bulletForce = 5f;
    [SerializeField] private float range = 10f;

    private GameObject player;
    private bool canFire = true;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && Vector3.Distance(transform.position, player.transform.position) < range && canFire)
        {
            StartCoroutine(Fire());
        }
    }

    private IEnumerator Fire()
    {
         canFire = false;

        Vector2 direction = (player.transform.position - firePoint.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * bulletForce;

        yield return new WaitForSeconds(fireRate);

        canFire = true;
    }
}
