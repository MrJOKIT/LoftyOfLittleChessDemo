using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Detect")] 
    public Transform alertSymbol;
    public Transform symBolPosition;
    public LayerMask playerLayer;
    private float fountStopTime;
    [SerializeField] private float foundStopTimeCounter;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float stopDistance;
    [SerializeField] private float detectRadius;

    [Header("Combat")] 
    [SerializeField] private float normalDamage;
    [SerializeField] private float heavyDamage;
    private float monsterDamage;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius;
    private float delayAttack;
    [SerializeField] private float firstDelayAttack;
    [SerializeField] private float secondDelayAttack;
    private int attackCount = 0;
    private float delayAttackTime;
    private bool foundPlayer = false;
    private Collider2D playerCollider;
    public Transform playerTransform;

    private EnemyPatrol enemyPatrol;
    private Animator animator;
    private Enemy enemy;
    private bool isAttacking;
    private bool attackTwoReady;
    private bool symbolAlert = true;
    
    private bool onGround;
    public Transform groundCheck;
    public float groundLength;
    public LayerMask groundLayer;
    
    
    

    private void Start()
    {
        animator = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
        enemyPatrol = GetComponent<EnemyPatrol>();
        groundLength = enemyPatrol.groundLength;
        groundLayer = enemyPatrol.groundLayer;
    }

    private void Update()
    {
        onGround = Physics2D.Raycast(groundCheck.position, Vector2.down, groundLength, groundLayer);
        if (attackCount < 2)
        {
            attackTwoReady = false;
        }
        else if (attackCount >= 2)
        {
            attackTwoReady = true;
        }

        if (!foundPlayer)
        {
            Collider2D detectArea = Physics2D.OverlapCircle(transform.position, detectRadius,playerLayer);
            if (detectArea != null && !isAttacking)
            {
                enemyPatrol.enabled = false;
                Flip();
                
                if (symbolAlert)
                {
                    Instantiate(alertSymbol, symBolPosition.position, Quaternion.identity, transform);
                    animator.SetBool("Walk",false);
                    animator.SetBool("Idle",true);
                    symbolAlert = false;
                }
                
                fountStopTime += Time.deltaTime;
                if (fountStopTime > foundStopTimeCounter)
                {
                    foundPlayer = true;
                    playerCollider = detectArea;
                }
            }
            else
            {
                enemyPatrol.enabled = true;
            }
        }
        else if (foundPlayer && Physics2D.OverlapCircle(transform.position, detectRadius, playerLayer) == null)
        {
            enemyPatrol.enabled = true;
            animator.SetBool("Walk",false);
            animator.SetBool("Idle",true);
            foundPlayer = false;
            playerCollider = null;
            symbolAlert = true;
            fountStopTime = 0f;

        }
        else if (foundPlayer && onGround)
        {
            
            float playerDistance = Mathf.Abs(transform.position.x - playerTransform.position.x);

            if (playerDistance > stopDistance && !isAttacking && !enemy.isHurt)
            {
                delayAttackTime = 0f;
                animator.SetBool("Walk",true);
                animator.SetBool("Idle",false);
                delayAttack = firstDelayAttack;
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            }
            else if (playerDistance <= stopDistance)
            {
                animator.SetBool("Walk",false);
                delayAttackTime += Time.deltaTime;
                if (!isAttacking && delayAttackTime > delayAttack)
                {
                    animator.SetBool("Idle",false);
                    EnemyAttack();
                    delayAttack = secondDelayAttack;
                    delayAttackTime = 0f;
                }
                else
                {
                    animator.SetBool("Idle",true);
                }
            }
            
            Flip();
            
            if (playerTransform.gameObject.GetComponent<PlayerHealth>().IsDead)
            {
                foundPlayer = false;
                isAttacking = false;
                playerCollider = null;
                animator.SetBool("Walk",false);
                animator.SetBool("Attacking",false);
                animator.SetBool("Attacking2",false);
                animator.SetBool("Idle",true);
            }
        }
        else if (!onGround)
        {
            if (!foundPlayer)
            {
                enemyPatrol.PatrolFlip();
            }
            else if (foundPlayer)
            {
                Flip();
            }
            
            animator.SetBool("Walk",false);
            animator.SetBool("Idle",true);
        }
    }

    public void Flip()
    {
        if (playerTransform.position.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0,180,0);
        }
        else if (playerTransform.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0,0,0);
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        // Draw the detection range for the enemy
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

    private void EnemyAttack()
    {
        if (!attackTwoReady)
        {
            monsterDamage = normalDamage;
            animator.SetBool("Attacking",true);
        }
        else
        {
            monsterDamage = heavyDamage;
            animator.SetBool("Attacking2",true);
        }
        isAttacking = true;
        
        
    }

    public void HitPlayer()
    {
        Collider2D player = Physics2D.OverlapCircle(attackPoint.position, attackRadius, playerLayer);
        if (player.CompareTag("Player"))
        {
            attackCount++;
            if (player != null)
            {
                player.GetComponent<PlayerHealth>().PlayerTakeDamage(monsterDamage);
            }
            
            if (attackTwoReady)
            {
                attackTwoReady = false;
                attackCount = 0;
            }
        }
    }

    public void EnemyFinishAttack()
    {
        if (!attackTwoReady)
        {
            animator.SetBool("Attacking",false);
            animator.SetBool("Attacking2",false);
        }
        else 
        {
            animator.SetBool("Attacking",false);
            animator.SetBool("Attacking2",false);
        }
        isAttacking = false;
        
    }
    
}
