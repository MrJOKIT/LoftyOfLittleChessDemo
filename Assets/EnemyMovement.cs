using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Detect")]
    public LayerMask playerLayer;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float stopDistance;
    [SerializeField] private float detectRadius;

    [Header("Combat")] 
    [SerializeField] private float monsterDamage;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius;
    [SerializeField] private float delayAttack;
    private float delayAttackTime;
    private bool foundPlayer = false;
    private Collider2D playerCollider;
    public Transform playerTransform;

    private Animator animator;
    private bool isAttacking;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!foundPlayer)
        {
            Collider2D detectArea = Physics2D.OverlapCircle(transform.position, detectRadius,playerLayer);
            if (detectArea != null)
            {
                foundPlayer = true;
                playerCollider = detectArea;
                
                
            }
        }
        else if (foundPlayer && Physics2D.OverlapCircle(transform.position, detectRadius, playerLayer) == null)
        {
            animator.SetBool("Walk",false);
            animator.SetBool("Attacking",false);
            foundPlayer = false;
            playerCollider = null;
            
        }
        else if (foundPlayer)
        {
            float playerDistance = Mathf.Abs(transform.position.x - playerTransform.position.x);

            if (playerDistance > stopDistance)
            {
                animator.SetBool("Walk",true);
                animator.SetBool("Idle",false);
                animator.SetBool("Attacking",false);
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            }
            else if (playerDistance <= stopDistance)
            {
                animator.SetBool("Walk",false);
                
                if (!isAttacking)
                {
                    animator.SetBool("Idle",false);
                    EnemyAttack();
                }
                else
                {
                    animator.SetBool("Idle",true);
                    delayAttackTime += Time.deltaTime;
                    if (delayAttackTime > delayAttack)
                    {
                        isAttacking = false;
                        delayAttackTime = 0f;
                    }

                }
            }
            Flip();
            
            if (playerTransform.gameObject.GetComponent<PlayerHealth>().IsDead)
            {
                foundPlayer = false;
                playerCollider = null;
            }
        }
    }

    void Flip()
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
        isAttacking = true;
        animator.SetBool("Attacking",true);
    }

    public void HitPlayer()
    {
        Collider2D player = Physics2D.OverlapCircle(attackPoint.position, attackRadius, playerLayer);
        if (player.CompareTag("Player"))
        {
            player.GetComponent<PlayerHealth>().PlayerTakeDamage(monsterDamage);
        }
    }

    public void EnemyFinishAttack()
    {
        animator.SetBool("Attacking",false);
    }
    
}
