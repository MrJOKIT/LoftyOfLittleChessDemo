using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    private Animator _animator;
    [SerializeField] private bool isEnemy;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy died!");
        
        //Die Animation
        //_animator.SetBool("Dead",true);
        
        //Disable the enemy
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (isEnemy)
            {
                col.GetComponent<PlayerHealth>().PlayerTakeDamage(10);
            }
        }
    }
}
