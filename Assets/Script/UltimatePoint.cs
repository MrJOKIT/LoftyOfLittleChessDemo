using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimatePoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<PlayerUltimate>().UltimatePoint += 0.25f;
            Destroy(gameObject);
        }
    }
}
