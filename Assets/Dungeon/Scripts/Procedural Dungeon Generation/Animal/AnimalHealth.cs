using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class AnimalHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    public Image healthBar;
    public TMP_Text healthText;

    private bool isImmune = false;
    private float immuneDuration = 1f;
    private float immuneTimer = 0f;

    private void Start()
    {
        currentHealth = maxHealth;

        GameObject imageObject = GameObject.FindGameObjectWithTag("HealthBar");
        healthBar = imageObject.GetComponent<Image>();
        GameObject textObject = GameObject.FindGameObjectWithTag("HealthValue");
        healthText = textObject.GetComponent<TMP_Text>();
        healthText.text = maxHealth.ToString();
    }

    private void Update()
    {
        if (isImmune)
        {
            immuneTimer -= Time.deltaTime;
            if (immuneTimer <= 0f)
            {
                isImmune = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemies"))
        {
            Debug.Log("Take Damage");
        }
        if (!isImmune && (collision.gameObject.CompareTag("EnemyBullet") || collision.gameObject.CompareTag("Enemies")))
        {
            currentHealth -= 25;
            healthBar.fillAmount = currentHealth / maxHealth;
            healthText.text = currentHealth.ToString();
            if (currentHealth <= 0)
            {
                Die();
                SceneManager.LoadScene("LoseScreen");
            }
            else
            {
                ApplyKnockback(collision);
                StartImmunity();
            }
        }
    }

    private void ApplyKnockback(Collision2D collision)
    {
        Vector3 knockbackDirection = (transform.position - collision.transform.position).normalized;
        float knockbackForce = 5f;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
    }

    private void StartImmunity()
    {
        isImmune = true;
        immuneTimer = immuneDuration;
    }

    public void Heal(int amount)
    {
        //currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
