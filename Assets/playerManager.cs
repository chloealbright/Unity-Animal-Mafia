using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{

    public static playerManager instance;

    private void Awake()
    {
        instance = this;
    }

    public GameObject player;
}