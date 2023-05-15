using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimalShooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    public float maxBullets = 8;
    public float shootCooldown = 0.5f; 
    public float reloadCooldown = 2f; 

    private float remainingBullets; 
    private bool canShoot = true;
    private bool canReload = true;

    public Image ammoBar;

    public TMP_Text ammoText;

    private void Start()
    {
        remainingBullets = maxBullets;

        GameObject imageObject = GameObject.FindGameObjectWithTag("AmmoBar");
        ammoBar = imageObject.GetComponent<Image>();
        GameObject textObject = GameObject.FindGameObjectWithTag("AmmoValue");
        ammoText = textObject.GetComponent<TMP_Text>();
        ammoText.text = maxBullets.ToString() + "/" + maxBullets.ToString();
    }

    void Update()
    {
        if (canShoot && Input.GetButtonDown("Fire1"))
        {
            Shoot();
            ammoBar.fillAmount = remainingBullets / maxBullets;
            ammoText.text = remainingBullets.ToString() + "/" + maxBullets.ToString();
        }
        
        if (canReload && Input.GetKeyDown(KeyCode.R))
        {
            Reload();
            ammoBar.fillAmount = remainingBullets / maxBullets;
            ammoText.text = remainingBullets.ToString() + "/" + maxBullets.ToString();
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
