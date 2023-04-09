using System;
using System.Collections;
using System.Collections.Generic;
using Micosmo.SensorToolkit.Example;
using UnityEngine;

public class PlayerThrowSpear : MonoBehaviour
{
    [SerializeField] private float spearSpeed;
    [SerializeField] private Transform spear;
    [SerializeField] private Transform spearSpawn;
    private PlayerHealth _playerHealth;

    private void Start()
    {
        _playerHealth = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if (!_playerHealth.IsDead)
        {
            if (Input.GetKeyUp(KeyCode.F))
            {
                ThrowSpear();
            }
        }
        
    }

    private void ThrowSpear()
    {
        Instantiate(spear, spearSpawn.position,spearSpawn.rotation);
    }
    
    private void Flip()
    {
        
        transform.Rotate(0f,180f,0f);
    }
}
