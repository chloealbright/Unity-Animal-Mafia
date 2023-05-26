using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShopCont.UI;

public class Movement : MonoBehaviour
{
    public float speed;

    public Animator animator;

    private Vector3 direction;

    public VectorValue startingPosition;

    public bool canMove;

    void Start()
    {
        //startingPosition.initialValue = startingPosition.defaultValue;
        transform.position = startingPosition.initialValue;
    }

    //get input from player
    //apply movement to sprite
    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        direction = new Vector3(horizontal, vertical);

        AnimateMovement(direction);
    }

    private void FixedUpdate()
    {
        //move the player
        //this.transform.position += direction * speed * Time.deltaTime;
        if(DialogueManager.GetInstance().dialogueIsPlaying)
        {
            this.transform.position += direction * 0 * Time.deltaTime;
        }
        else
        {
            this.transform.position += direction * speed * Time.deltaTime;
        }
    }

    //this function handles talking with the animator
    void AnimateMovement(Vector3 direction)
    {
        //check to see if the animator reference is set - check to see if the value of the animator is not null
        if(animator != null)
        {
            //check if the sprite is moving (direction vector x and y would be 0 if we dont receive an input we are not moving)
            if(direction.magnitude > 0)
            {
                animator.SetBool("isMoving", true);
                animator.SetFloat("horizontal", direction.x);
                animator.SetFloat("vertical", direction.y);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
        }
    }
}
