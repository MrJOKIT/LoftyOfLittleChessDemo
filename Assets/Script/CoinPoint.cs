using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPoint : MonoBehaviour
{
    private SoundManager soundManager;
    private Rigidbody2D rb;

    private void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.up * 2f;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            SoundManager.instace.Play(SoundManager.SoundName.TakeUltimate);
            col.GetComponent<PlayerController>().Coin += 100;
            Destroy(gameObject);
        }
    }
}
