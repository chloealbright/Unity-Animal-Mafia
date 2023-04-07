using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public CollectableType type;

    //player walks into collectable
    //add collectable to player
    //delete collectable from then screen

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player)
        {
            player.inventory.Add(type);
            Destroy(this.gameObject);
        }
    }
}

public enum  CollectableType
{
    NONE, CARROT_SEED
}
