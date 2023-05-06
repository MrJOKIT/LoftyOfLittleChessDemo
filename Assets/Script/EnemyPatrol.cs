using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public LayerMask groundLayer;
    public float patrolSpeed;
    public float groundLength;
    public Transform groundCheck;
    private Rigidbody2D rb;
    private EnemyMovement enemyMovement;
    private Animator animator;
    private bool onGround;
    private bool onRight = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyMovement = GetComponent<EnemyMovement>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        onGround = Physics2D.Raycast(groundCheck.position, Vector2.down, groundLength, groundLayer);

        if (onGround)
        {
            Patrol();
            animator.SetBool("Walk",true);
            animator.SetBool("Idle",false);
        }
        else if (!onGround)
        {
           PatrolFlip(); 
        }
                   
        
    }

    void Patrol()
    {
        transform.Translate(Vector3.right * patrolSpeed * Time.deltaTime);
    }

    public void PatrolFlip()
    {
        if (onRight)
        {
            transform.rotation = Quaternion.Euler(0,180,0);
            onRight = false;
        }
        else if (!onRight)
        {
            transform.rotation = Quaternion.Euler(0,0,0);
            onRight = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(groundCheck.position,Vector3.down * groundLength);
        
    }
}
