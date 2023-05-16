using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private float searchRange = 5f;
    [SerializeField] private float stopDuration = 1f;
    [SerializeField] private float minimumDistance = 1.5f;
    [SerializeField] private int maxHealth = 6;
    [SerializeField] private int scoreValue = 15;

    private Transform player;
    private bool isMoving = false;
    private bool canMove = true;
    private int currentHealth;
    private ScoreManager scoreManager;
    public HealthBarBehaviour healthBar;

    private bool isFrozen = true;
    private float freezeDuration = 3f;

    private float moveTime;
    private float timer;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = maxHealth;

        scoreManager = FindObjectOfType<ScoreManager>();
        healthBar.SetHealth(currentHealth, maxHealth);

        StartCoroutine(UnfreezeAfterDelay());
        SetRandomMoveTime();
    }

    private void SetRandomMoveTime()
    {
        moveTime = Random.Range(1f, 3f);
        timer = moveTime;
    }

    private IEnumerator UnfreezeAfterDelay()
    {
        yield return new WaitForSeconds(freezeDuration);
        isFrozen = false;
    }

    private void Update()
    {
        if (isFrozen)
            return;

        timer -= Time.deltaTime;

        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= searchRange && distanceToPlayer >= minimumDistance)
            {
                Vector3 targetPosition = player.position;

                if (!isMoving && canMove)
                {
                    StartCoroutine(MoveToPosition(targetPosition));
                }
            }
            else
            {
                if (timer <= 0)
                {
                    timer = Random.Range(1f, 3f);
                    Vector2 randomDirection = Random.insideUnitCircle.normalized;
                    Vector3 targetPosition = transform.position + new Vector3(randomDirection.x, randomDirection.y, 0);

                    if (!IsOutsidePlayableArea(targetPosition))
                    {
                        if (!isMoving)
                        {
                            StartCoroutine(MoveToPosition(targetPosition));
                        }
                    }
                }
            }
        }
    }

    private bool IsOutsidePlayableArea(Vector3 position)
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll(position);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Wall"))
            {
                return true;
            }
        }
        return false;
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        isMoving = true;
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            if (canMove)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
            }
            yield return null;
        }
        isMoving = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isFrozen)
            return;

        if (collision.gameObject.CompareTag("Player"))
        {
            StopMoving();
            StartCoroutine(ResumeMovementAfterDelay());
        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(1);
        }
    }

    private void StopMoving()
    {
        isMoving = false;
        canMove = false;
    }
    private IEnumerator ResumeMovementAfterDelay()
    {
        yield return new WaitForSeconds(stopDuration);
        canMove = true;
    }

    private void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        healthBar.SetHealth(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
            scoreManager.AddScore(scoreValue);
        }
        else
        {
            Vector3 knockbackDirection = (transform.position - player.position).normalized;
            StartCoroutine(Knockback(knockbackDirection));
        }
    }

    private IEnumerator Knockback(Vector3 direction)
    {
        float knockbackForce = 5f;
        float knockbackDuration = 0.2f;
        float timer = 0f;

        canMove = false;

        while (timer < knockbackDuration)
        {
            timer += Time.deltaTime;
            transform.position += direction * knockbackForce * Time.deltaTime;
            yield return null;
        }

        canMove = true;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
