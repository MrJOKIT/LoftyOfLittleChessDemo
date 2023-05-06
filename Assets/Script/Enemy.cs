using System;
using System.Collections;
using System.Collections.Generic;
using BarthaSzabolcs.Tutorial_SpriteFlash;
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
    public GameObject dropSplash;
    private Animator _animator;
    private Rigidbody2D rb;
    private EnemyMovement enemyMovement;
    private GameStageCheck gameStageCheck;
    private SimpleFlash simpleFlash;
    private GameManager gameManager;
    public bool isHurt;
    public bool rangeType;
    public bool isBoss;
    public GameObject ifBossDie;
    

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
        simpleFlash = GetComponent<SimpleFlash>();
        gameStageCheck = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameStageCheck>();
        if (!isBoss)
        {
            gameStageCheck.AddMonster(this.gameObject);
        }
        currentHealth = maxHealth;
        isHurt = false;
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
        if (!rangeType)
        {
            isHurt = true;
            _animator.SetBool("Hurt",true);
            
        }
        else
        {
            simpleFlash.Flash();
        }
        if (currentHealth <= 0)
        {
            GetComponent<Collider2D>().enabled = false;
            Die();
        }
        
        if (!antiKnockBack)
        {
            KnockbackHit();
        }


    }

    private void Die()
    {
        gameStageCheck.RemoveMonster(this.gameObject);
        Instantiate(dropSplash, transform.position, Quaternion.identity);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        if (isBoss)
        {
            ifBossDie.SetActive(false);
        }
        Destroy(gameObject);
    }
    
    public void KnockbackHit()
    {
        knockBackCount++;
        Vector2 knockbackDirection = new Vector2(transform.position.x - enemyMovement.playerTransform.position.x,0);
        rb.velocity = new Vector2(knockbackDirection.x, knockBackForceUp) * knockBackForce;
    }

    public void FinishDamage()
    {
        _animator.SetBool("Hurt",false);
        isHurt = false;
    }
    
}
