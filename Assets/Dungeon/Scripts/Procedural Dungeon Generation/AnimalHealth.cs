using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimalHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    public Image healthBar;

    public TMP_Text healthText;

    private void Start()
    {
        currentHealth = maxHealth;

        GameObject imageObject = GameObject.FindGameObjectWithTag("HealthBar");
        healthBar = imageObject.GetComponent<Image>();
        GameObject textObject = GameObject.FindGameObjectWithTag("HealthValue");
        healthText = textObject.GetComponent<TMP_Text>();
        healthText.text = maxHealth.ToString();
    }

   void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            currentHealth -= 25;
            healthBar.fillAmount = currentHealth/ maxHealth;
            healthText.text = currentHealth.ToString();
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }


    public void Heal(int amount)
    {
        //currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    private void Die()
    {
        // Handle the animal's death
        Destroy(gameObject);
    }
}