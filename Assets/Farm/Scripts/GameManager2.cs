using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager2 : MonoBehaviour
{

    public static GameManager2 instance;

    private void Awake()
    {
        instance = this;
    }

    public GameObject player;
}
