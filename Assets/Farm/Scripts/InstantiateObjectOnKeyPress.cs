using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateObjectOnKeyPress : MonoBehaviour
{
    // The prefab to instantiate
    [SerializeField] GameObject Carrot_Prefab;
    [SerializeField] GameObject Potato_Prefab;
    [SerializeField] GameObject Tomato_Prefab;
    [SerializeField] GameObject Pumkin_Prefab;

    //passing the player as an object to obtain it's location
    [SerializeField] Transform PlayerTransform; // The transform of the player GameObject

    //range for area of the carrot patch;
    [SerializeField] float carrot_patch_x_min;
    [SerializeField] float carrot_patch_x_max;
    [SerializeField] float carrot_patch_y_min;
    [SerializeField] float carrot_patch_y_max;

    //range for the area of the potato patch;
    [SerializeField] float potato_patch_x_min;
    [SerializeField] float potato_patch_x_max;
    [SerializeField] float potato_patch_y_min;
    [SerializeField] float potato_patch_y_max;

    //range for the area of the tomato patch;
    [SerializeField] float tomato_patch_x_min;
    [SerializeField] float tomato_patch_x_max;
    [SerializeField] float tomato_patch_y_min;
    [SerializeField] float tomato_patch_y_max;

    //range for the area of the pumkin patch;
    [SerializeField] float pumkin_patch_x_min;
    [SerializeField] float pumkin_patch_x_max;
    [SerializeField] float pumkin_patch_y_min;
    [SerializeField] float pumkin_patch_y_max;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) //player pressed the interaction button
        {
            //check for the player location in order to allow for the player to plant the correct plant

            if(PlayerTransform.position.x <= potato_patch_x_max && PlayerTransform.position.x >= potato_patch_x_min &&
               PlayerTransform.position.y <= potato_patch_y_max && PlayerTransform.position.y >= potato_patch_y_min)
            {
                Vector3 spawnPosition = PlayerTransform.position + (PlayerTransform.forward);

                // Instantiate the object at the calculated position
                Instantiate(Potato_Prefab, spawnPosition, Quaternion.identity);
            }
            else if(PlayerTransform.position.x <= carrot_patch_x_max && PlayerTransform.position.x >= carrot_patch_x_min &&
                    PlayerTransform.position.y <= carrot_patch_y_max && PlayerTransform.position.y >= carrot_patch_y_min)
            {
                Vector3 spawnPosition = PlayerTransform.position + (PlayerTransform.forward);

                Instantiate(Carrot_Prefab, spawnPosition, Quaternion.identity);
            }
            else if (PlayerTransform.position.x <= tomato_patch_x_max && PlayerTransform.position.x >= tomato_patch_x_min &&
                    PlayerTransform.position.y <= tomato_patch_y_max && PlayerTransform.position.y >= tomato_patch_y_min)
            {
                Vector3 spawnPosition = PlayerTransform.position + (PlayerTransform.forward);

                Instantiate(Tomato_Prefab, spawnPosition, Quaternion.identity);
            }
            else if (PlayerTransform.position.x <= pumkin_patch_x_max && PlayerTransform.position.x >= pumkin_patch_x_min &&
                    PlayerTransform.position.y <= pumkin_patch_y_max && PlayerTransform.position.y >= pumkin_patch_y_min)
            {
                Vector3 spawnPosition = PlayerTransform.position + (PlayerTransform.forward);

                Instantiate(Pumkin_Prefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}


