using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private float shootingRange = 3f;
    [SerializeField] private float searchRange = 5f;
    [SerializeField] private float minMoveTime = 1f;
    [SerializeField] private float maxMoveTime = 3f;

    private float moveTime;
    private float timer;
    public float health = 3;
    private float currentHealth;
    private bool isMoving = false;

    private ScoreManager scoreManager;

    public HealthBarBehaviour healthBar;

    private GameObject player;

    private bool isFrozen = true;
    private float freezeDuration = 3f;

    private void Start()
    {
        moveTime = Random.Range(minMoveTime, maxMoveTime);
        timer = moveTime;
        currentHealth = health;
        healthBar.SetHealth(currentHealth, health);
        Debug.Log(healthBar);

        scoreManager = FindObjectOfType<ScoreManager>();
        player = GameObject.FindGameObjectWithTag("Player");

        StartCoroutine(UnfreezeAfterDelay());
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
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distanceToPlayer <= shootingRange)
            {
                StopMoving();
            }
            else if (distanceToPlayer <= searchRange)
            {
                Vector3 directionToPlayer = player.transform.position - transform.position;
                Vector3 moveDirection = directionToPlayer.normalized;
                Vector3 targetPosition = player.transform.position;

                if (!isMoving)
                {
                    StartCoroutine(MoveToPosition(targetPosition));
                }
            }
            else
            {
                if (timer <= 0)
                {
                    timer = Random.Range(minMoveTime, maxMoveTime);
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
            RaycastHit2D hit = Physics2D.Linecast(transform.position, targetPosition, LayerMask.GetMask("Wall"));

            if (hit.collider != null)
            {
                Vector2 dirToTarget = (targetPosition - transform.position).normalized;
                Vector2 newTarget = hit.point + dirToTarget * 0.1f;
                targetPosition = new Vector3(newTarget.x, newTarget.y, transform.position.z);
            }

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
            yield return null;
        }
        isMoving = false;
    }

    private void StopMoving()
    {
        isMoving = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(1);
            healthBar.SetHealth(currentHealth, health);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            Vector3 oppositeDirection = Vector3.Reflect(transform.position - collision.transform.position, collision.contacts[0].normal);
            StartCoroutine(MoveToPosition(transform.position + oppositeDirection));
        }
    }
    private void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Debug.Log(scoreManager);
            Die();
            scoreManager.AddScore(10);
        }
        else
        {
            Vector3 knockbackDirection = (transform.position - player.transform.position).normalized;
            StartCoroutine(Knockback(knockbackDirection));
        }
    }

    private IEnumerator Knockback(Vector3 direction)
    {
        float knockbackForce = 2f;
        float knockbackDuration = 0.2f;
        float timer = 0f;

        while (timer < knockbackDuration)
        {
            timer += Time.deltaTime;
            transform.position += direction * knockbackForce * Time.deltaTime;
            yield return null;
        }
    }


    private void Die()
    {
        Destroy(gameObject);
    }
}