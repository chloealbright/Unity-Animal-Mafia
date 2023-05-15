using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalShooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    public int maxBullets = 6; // maximum number of bullets the player can shoot
    public float shootCooldown = 0.5f; // time between shots
    
    private int remainingBullets; // current number of remaining bullets
    private bool canShoot = true; // is the player allowed to shoot?
    private bool canReload = true; // is the player allowed to reload?

    private void Start()
    {
        remainingBullets = maxBullets;
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot && remainingBullets > 0 && Input.GetButtonDown("Fire1"))
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
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(0f, 0f, 180f));
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(-firePoint.up * bulletForce, ForceMode2D.Impulse);
        remainingBullets--;

        if (remainingBullets == 0)
        {
            canShoot = false;
            StartCoroutine(EnableShootingAfterCooldown());
        }
    }

    IEnumerator EnableShootingAfterCooldown()
    {
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
        remainingBullets = maxBullets;
    }

    void Reload()
    {
        StartCoroutine(EnableShootingAfterCooldown());
    }
}
