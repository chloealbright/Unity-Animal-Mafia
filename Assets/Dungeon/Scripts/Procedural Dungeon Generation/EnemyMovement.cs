using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private float minMoveTime = 1f;
    [SerializeField] private float maxMoveTime = 3f;

    private float moveTime;
    private float timer;
    public int health = 3;
    private int currentHealth;
    private void Start()
    {
        moveTime = UnityEngine.Random.Range(minMoveTime, maxMoveTime);
        timer = moveTime;
        currentHealth = health;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Vector2 randomDirection = UnityEngine.Random.insideUnitCircle.normalized;
            Vector3 targetPosition = transform.position + new Vector3(randomDirection.x, randomDirection.y, 0);
            StartCoroutine(MoveToPosition(targetPosition));
            moveTime = UnityEngine.Random.Range(minMoveTime, maxMoveTime);
            timer = moveTime;
        }
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
            yield return null;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(1);
        }
    }
    private void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}
