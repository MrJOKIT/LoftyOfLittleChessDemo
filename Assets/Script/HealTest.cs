using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealTest : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerHealth>().PlayerTakeHealth(10);
        }
    }
}
