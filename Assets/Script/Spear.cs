using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    public float speed = 20f;
    public LayerMask pinLayer;
    private Rigidbody2D rb;
    private float time;
    private float timer = 15f;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time > timer)
        {
            Destroy(gameObject);
            time = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (((1 << col.gameObject.layer) & pinLayer) != 0)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
        else if (col.gameObject.CompareTag("Player"))
        {
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
}
