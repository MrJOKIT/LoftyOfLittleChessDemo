using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private GameObject player;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            player = col.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        player = null;
    }
    
}
