using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float cellSize = 0.16f;
    public Transform movePoint; 
    public LayerMask Collisions;
    public Animator animator;

    // New variables for random movement and player detection
    private float timer;
    private Vector3 direction;
    public float detectionRadius = 2f;
    public LayerMask playerLayer;

    void Start()
    {
        movePoint.parent = null;
        animator = GetComponent<Animator>();
    }

    public void Move()
    {
        // Check if the player is within the detection radius
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);
        if (playerCollider != null) {
            // Move towards the player
            movePoint.position = playerCollider.transform.position;
            animator.SetBool("moving", true);
        } else {
            // Move randomly
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
            if(Vector3.Distance(transform.position, movePoint.position) <= .05f)
            {
                // Get random direction
                direction = new Vector3(Random.Range(-1, 2) * cellSize, Random.Range(-1, 2) * cellSize, 0f);

                // Check if the new position is valid
                if (!Physics2D.OverlapCircle(movePoint.position + direction, .02f, Collisions)) {
                    movePoint.position += direction;
                }
                animator.SetBool("moving", false);
            }
            else
            {
                animator.SetBool("moving", true);
            }
        }
    }

    // Draw the detection radius in the editor for debugging
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}