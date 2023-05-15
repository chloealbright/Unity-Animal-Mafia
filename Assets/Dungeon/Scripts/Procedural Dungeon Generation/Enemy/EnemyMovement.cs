using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private float range = 5f;
    [SerializeField] private float minMoveTime = 1f;
    [SerializeField] private float maxMoveTime = 3f;

    private float moveTime;
    private float timer;
    public int health = 3;
    private int currentHealth;
    private bool isMoving = false;

    // Reference to the score manager
    private ScoreManager scoreManager;

    private void Start()
    {
        moveTime = UnityEngine.Random.Range(minMoveTime, maxMoveTime);
        timer = moveTime;
        currentHealth = health;

        scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && Vector3.Distance(transform.position, player.transform.position) < range)
        {
            Vector3 directionToPlayer = player.transform.position - transform.position;
            Vector3 moveDirection = Vector3.Normalize(directionToPlayer);
            Vector3 targetPosition = player.transform.position + moveDirection * (range * 0.75f);

            // Check if the target position is within the playable area
            if (!IsOutsidePlayableArea(targetPosition))
            {
                if (!isMoving)
                {
                    StartCoroutine(MoveToPosition(targetPosition));
                }
            }
        }
        else
        {
            Vector2 randomDirection = UnityEngine.Random.insideUnitCircle.normalized;
            Vector3 targetPosition = transform.position + new Vector3(randomDirection.x, randomDirection.y, 0);

            // Check if the target position is within the playable area
            if (!IsOutsidePlayableArea(targetPosition))
            {
                if (!isMoving)
                {
                    StartCoroutine(MoveToPosition(targetPosition));
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(1);
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
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
