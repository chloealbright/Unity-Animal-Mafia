using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsCharacterController : MonoBehaviour
{
    //CharacterController2D character;
    Rigidbody2D rgbd2d;

    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeOfInteractableArea = 1.2f;

    private void Awake()
    {
        //CharacterController = GetComponent<CharacterController2D>();
        rgbd2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.Space))
        {
            UseTool();
        }
    }

    private void UseTool()
    {
        //Vector2 position = rgbd2d.position + character.lastMotionVector * offsetDistance;
        Vector2 position = rgbd2d.position * offsetDistance;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);

        foreach (Collider2D c in colliders)
        {
            ToolHit hit = c.GetComponent<ToolHit>();

            if(hit != null)
            {
                hit.Hit();
                break;
            }
        }
    }
}
