using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Transform sfx;
    public LayerMask destroyMask;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerHealth>().PlayerTakeDamage(10);
            Instantiate(sfx, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            Instantiate(sfx, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (((1 << col.gameObject.layer) & destroyMask) != 0)
        {
            Destroy(gameObject);
        }
    }
}
