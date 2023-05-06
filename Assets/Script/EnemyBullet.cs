using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public Transform sfx;
    public float damage;
    public LayerMask destroyMask;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerHealth>().PlayerTakeDamage(damage);
            Instantiate(sfx, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (col.CompareTag("Enemy"))
        {
            return;
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
