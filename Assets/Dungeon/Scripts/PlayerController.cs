using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //public so it appease in the unity inspector
    public float moveSpeed = 1f; //can change this value later in the unity editor
    public ContactFilter2D movementFilter;
    public  float collisionOffset = 0.05f;
    public SwordAttack swordAttack; // to get access of sword attack class

    Vector2 movementInput; // inputs: 1 for x 1 for y
    Rigidbody2D rb; // on game start, get reference
    SpriteRenderer spriteRenderer; // for animation direction
    Animator animator; 
    bool canMove = true; 

    // private list of cast Collisions
    // when the script starts this will create an empty list
    // when FixedUpdate runs, castCollisions will populate this list
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    public Dungeon_VectorValue startingPosition;// for player position during scene transitions
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform.position = startingPosition.initialValue;
        
    }

   
    private void FixedUpdate(){
        if(canMove){
            // if input !0, try to move the player
            if(movementInput != Vector2.zero){// direction of the vector
                bool success = TryMove(movementInput);

                if(!success && movementInput.x>0){ //if player can't move in both x&y directions check if it can move in 1 direction
                    success = TryMove(new Vector2(movementInput.x, 0));
                }
                else if(!success && movementInput.y>0){
                        success = TryMove(new Vector2(0,movementInput.y));
                }// enables verticle player movement on both keys up/down-left up-down/right

                //set animation for player movement
                animator.SetBool("isMoving", success);
            }
            else{
                animator.SetBool("isMoving", false);
            }
            // set animation to player direction when movement = true
            if(movementInput.x <0){// player is moving to the left x axis
                spriteRenderer.flipX = true;
                // swordAttack.attackDirection = SwordAttack.AttackDirection.left; // calls AttackDirection case Left
            }
            else if(movementInput.x >0){
                spriteRenderer.flipX= false;
                // swordAttack.attackDirection = SwordAttack.AttackDirection.right; // calls AttackDirection case Right
            }
            //in the case of movementInput == 0, player isn't moving left or right
        }
         
        
    }

    private bool TryMove(Vector2 direction){
        
        // check if player can move using RigidBody2D
        int count = rb.Cast(
            direction, // x & y direction between -1 & 1 represents the direction from the body to look for collisions
            movementFilter, // settings that determine where a collision can occur on such as layers to collide with
            castCollisions, // list of collisions to store the found collisions into after the Cast is finished
            moveSpeed * Time.fixedDeltaTime + collisionOffset); // magnitude of the vector= the amount to cast equal to the movement + offset
        
        if(count == 0){
            // gives total magnitude/direction to add to the player's current position
            // and ensures movement is consistent regardless of time it takes to load the frames
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        }
        else{
            return false;
        }
        

    }

    void OnMove(InputValue movementValue){//input value from InputSystem
        //xy direction character is moving
        movementInput = movementValue.Get<Vector2>();
    }

    void OnFire(){
        //TEST fire pressed on left mouse click & move 
        //print("Fire pressed");
        animator.SetTrigger("swordAttack");

    }

    public void SwordAttack(){
        LockMovement();
        if(spriteRenderer.flipX == true){
            swordAttack.AttackLeft();
        }
        else{
            swordAttack.AttackRight();
        }
    }

    public void LockMovement(){
        canMove = false;
    }
    public void UnlockMovement(){
        canMove = true;
    }
    public void EndSwordAttack(){
        UnlockMovement(); 
        swordAttack.StopAttack();
    }
    
}
