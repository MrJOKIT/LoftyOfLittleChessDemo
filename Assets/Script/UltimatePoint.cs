using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimatePoint : MonoBehaviour
{
    private SoundManager soundManager;

    private void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            SoundManager.instace.Play(SoundManager.SoundName.TakeUltimate);
            col.GetComponent<PlayerUltimate>().UltimatePoint += 0.25f;
            Destroy(gameObject);
        }
    }
}
