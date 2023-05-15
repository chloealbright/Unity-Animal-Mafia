using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalShooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    public int maxBullets = 6;
    public float shootCooldown = 0.5f; 
    public float reloadCooldown = 2f; 

    private int remainingBullets; 
    private bool canShoot = true;
    private bool canReload = true;

    private void Start()
    {
        remainingBullets = maxBullets;
    }

    void Update()
    {
        if (canShoot && Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        
        if (canReload && Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    void Shoot()
    {
        if (remainingBullets > 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(0f, 0f, 180f));
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(-firePoint.up * bulletForce, ForceMode2D.Impulse);
            remainingBullets--;
        }

        if (remainingBullets == 0)
        {
            canShoot = false;
            StartCoroutine(EnableShootingAfterCooldown(shootCooldown));
        }
    }

    IEnumerator EnableShootingAfterCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
        canReload = true;
        remainingBullets = maxBullets;
    }

    void Reload()
    {
        if (canReload)
        {
            Debug.Log("Reload");
            canShoot = false;
            canReload = false;
            StartCoroutine(EnableShootingAfterCooldown(reloadCooldown));
        }
    }
}
