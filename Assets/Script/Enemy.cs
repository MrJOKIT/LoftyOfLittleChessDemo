using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Knockback")]
    [SerializeField] private float knockBackForce;
    [SerializeField] private float knockBackForceUp;
    [Header("Health")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private bool isEnemy;
    private Animator _animator;
    private Rigidbody2D rb;
    private EnemyMovement enemyMovement;

    private int knockBackCount;
    [SerializeField] private int maxKnockBack;
    private bool antiKnockBack = false;
    private float antiKnockBackTime;
    [SerializeField] private float antiKnockBackTimer;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (knockBackCount < maxKnockBack)
        {
            antiKnockBack = false;
        }
        else if (knockBackCount >= maxKnockBack)
        {
            antiKnockBack = true;
            antiKnockBackTime += Time.deltaTime;
            if (antiKnockBackTime > antiKnockBackTimer)
            {
                knockBackCount = 0;
                antiKnockBackTime = 0f;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        
        
        if (!antiKnockBack)
        {
            KnockbackHit();
        }
        else
        {
            return;
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
    
    public void KnockbackHit()
    {
        knockBackCount++;
        Vector2 knockbackDirection = new Vector2(transform.position.x - enemyMovement.playerTransform.position.x,0);
        rb.velocity = new Vector2(knockbackDirection.x, knockBackForceUp) * knockBackForce;
    }
    
    

    
}
