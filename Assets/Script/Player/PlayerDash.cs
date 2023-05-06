using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [Header("Dash System")] 
    public LayerMask playerLayer;
    public LayerMask enemyLayer;
    public float wallCheck;
    public LayerMask wallMask;
    private bool isDashing = false;
    private bool canDash = true;
    private bool isWall;
    public Transform wallCheckPos;

    private float dashTimeLeft;
    private float lastImageXpos;
    private float lastDash = -100f;
    
    public float dashTime;
    public float dashSpeed;
    public float distanceBetweenImage;
    public float dashCoolDown;

    private int facingDirection = 1;
    
    private SoundManager _soundManager;
    private PlayerMovement _playerMovement;
    private Rigidbody2D rb;
    private PlayerAnimation _playerAnimation;
    private Animator _animator;
    private PlayerMana _playerMana;
    private PlayerHealth playerHealth;

    public bool CanDash
    {
        get { return canDash; }
        set { canDash = value; }
    }

    public bool IsDashing
    {
        get { return isDashing; }
        set { isDashing = value; }
    }
    private void Start()
    {
        _soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        _animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        _playerMana = GetComponent<PlayerMana>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        isWall = Physics2D.Raycast(wallCheckPos.position, Vector2.right, wallCheck,wallMask);
        Dash();
        CheckDash();
        if (!_playerMovement.FacingRight)
        {
            facingDirection = -1;
            wallCheck = -1f;
        }
        else if (_playerMovement.FacingRight)
        {
            facingDirection = 1;
            wallCheck = 1f;
        }
    }

    private void AttempTodash()
    {
        Physics2D.IgnoreLayerCollision(7,10,true);
        SoundManager.instace.Play(SoundManager.SoundName.Dash);
        _playerMovement.CreateDust();
        _animator.SetBool("Dash",true);
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;

        PlayerAfterImagePool.instance.GetFromPool();
        lastImageXpos = transform.position.x;
    }

    private void Dash()
    {
        if (Input.GetButtonDown("Dash") && canDash && _playerMana.Mp >= 0.2f && _playerMovement.direction.x != 0) 
        {
            if (Time.time >= (lastDash + dashCoolDown))
            {
                _playerMana.Mp -= 0.2f;
                AttempTodash();
            }
        }
    }

    private void CheckDash()
    {
        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {
                
                rb.velocity = new Vector2(dashSpeed * facingDirection, rb.velocity.y);
                dashTimeLeft -= Time.deltaTime;

                if (Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImage)
                {
                    PlayerAfterImagePool.instance.GetFromPool();
                    lastImageXpos = transform.position.x;
                }
                
                
            }

            
            if (dashTimeLeft <= 0 || isWall)
            {
                Physics2D.IgnoreLayerCollision(7, 10, false);
                _animator.SetBool("Dash",false);
                rb.velocity = Vector2.zero;
                isDashing = false;
                _playerMovement.CanMove = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(wallCheckPos.position,Vector3.right * wallCheck);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            _animator.SetBool("Dash",false);
            dashTimeLeft = 0;
            rb.velocity = Vector2.zero;
            isDashing = false;
            //_playerAnimation.State = PlayerAnimation.PlayerState.Hurt;
            //playerHealth.IsHurt = true;
        }

        if (((1 << collision.gameObject.layer) & wallMask) != 0)
        {
            _animator.SetBool("Dash",false);
            dashTimeLeft = 0;
            rb.velocity = Vector2.zero;
            isDashing = false;
        }
    }
}
