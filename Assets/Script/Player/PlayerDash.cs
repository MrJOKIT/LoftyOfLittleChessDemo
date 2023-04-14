using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [Header("Dash System")] 
    private bool isDashing = false;
    private bool canDash = true;

    private float dashTimeLeft;
    private float lastImageXpos;
    private float lastDash = -100f;
    
    public float dashTime;
    public float dashSpeed;
    public float distanceBetweenImage;
    public float dashCoolDown;

    private int facingDirection = 1;
    
    private SoundManager _soundManager;
    private PlayerController _playerController;
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
        _playerController = GetComponent<PlayerController>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        _animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        _playerMana = GetComponent<PlayerMana>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        Dash();
        CheckDash();
        if (_playerController.IsLeft)
        {
            facingDirection = -1;
        }
        else if (!_playerController.IsLeft)
        {
            facingDirection = 1;
        }
    }

    private void AttempTodash()
    {
        _animator.SetBool("IsDash",true);
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;

        PlayerAfterImagePool.instance.GetFromPool();
        lastImageXpos = transform.position.x;
    }

    private void Dash()
    {
        if (Input.GetButtonDown("Dash") && canDash && _playerMana.Mp >= 0.2f) 
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
                _playerAnimation.State = PlayerAnimation.PlayerState.Dash;
                _playerController.CanMove = false;
                rb.velocity = new Vector2(dashSpeed * facingDirection, rb.velocity.y);
                dashTimeLeft -= Time.deltaTime;

                if (Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImage)
                {
                    PlayerAfterImagePool.instance.GetFromPool();
                    lastImageXpos = transform.position.x;
                }
            }

            if (dashTimeLeft <= 0 || _playerController.IsTouchingWall)
            {
                _animator.SetBool("IsDash",false);
                rb.velocity = Vector2.zero;
                isDashing = false;
                _playerController.CanMove = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            _animator.SetBool("IsDash",false);
            rb.velocity = Vector2.zero;
            isDashing = false;
            //_playerAnimation.State = PlayerAnimation.PlayerState.Hurt;
            //playerHealth.IsHurt = true;
        }
    }
}
