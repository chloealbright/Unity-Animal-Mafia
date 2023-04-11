using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public Collider2D swordCollider;

    //get base value on start 
    Vector2 rightAttackOffset;
    
    //deal damage
    public float damage =3; // strength of sword

    // use in a switch state 
    // public enum AttackDirection{
    //     left, right
    // }

    //public AttackDirection attackDirection;

    private void Start(){
        //using Collider2D vs. BoxCollider for different shapes
        // swordCollider = GetComponent<Collider2D>();
        //^don't need box collider, it's not going to get the specific component 

        rightAttackOffset = transform.position;
    }

    // public void Attack(){
    //     switch(attackDirection){
    //         case AttackDirection.left:
    //             AttackLeft();
    //             break;
    //         case AttackDirection.right:
    //             AttackRight();
    //             break;

    //     }
    // }
    //in this case^ AttackRight and AttackLeft would be private void functions

    public void AttackRight(){
        //TESTING
        //print("Attack Right");
        swordCollider.enabled= true;
        transform.localPosition = rightAttackOffset;
    }
    
    public void AttackLeft(){
        //TESTING
        //print("Attack Left");
        swordCollider.enabled= true;
        transform.localPosition = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y);
    }

    public void StopAttack(){
        swordCollider.enabled= false;
    }

    // private void OnTriggerEnter2D(Collider2D other){
    //     //TESTING
    //     print("Trigger Enter");
    //     if(other.tag == "Enemy"){
    //         Enemy enemy = other.GetComponent<Enemy>();//get enemy script

    //         if(enemy != null){
    //             enemy.Health -= damage;
    //         }
    //     }
    // }
}
