using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float cellSize = 0.16f;
    public Transform movePoint; 

    public LayerMask Collisions;
    public Animator animator;
    public EnemyMovement enemyMovement;
    void Start()
    {
        movePoint.parent = null;
        animator = GetComponent<Animator>();
        enemyMovement = FindObjectOfType<EnemyMovement>();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        if(Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal") * cellSize, 0f, 0f), .02f, Collisions))
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal") * cellSize, 0f, 0f);
                    enemyMovement.Move();
                }
            }
            if(Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical") * cellSize, 0f), .02f, Collisions))
                {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical") * cellSize, 0f);
                    enemyMovement.Move();
                }
            }
            // animator.SetBool("moving", false);
        }
        else
        {
            // animator.SetBool("moving", true);
        }
    }
}