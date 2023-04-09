using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Header("Jump Setting")]
    [SerializeField] private float jumpForce;

    [SerializeField] private LayerMask groundMask;
    private bool canJump = false;
    private bool _isJumping = false;
    private bool _isDoubleJumping = false;
    private bool _isGrounded = false;
    private bool checkJumpBug = false;
    private bool fallingLong = false;
    private Rigidbody2D rb;
    private Animator _animator;
    private PlayerController _playerController;
    private PlayerHealth _playerHealth;
    private PlayerAnimation _playerAnimation;
    private PlayerDash _playerDash;

    private float time;
    private float timer = 0.5f;
    //private PlayerGrappling _playerGrappling;

    public bool IsGround
    {
        get { return _isGrounded; }
        set { _isGrounded = value; }
    }
    
    public bool IsJump
    {
        get { return _isJumping; }
        set { _isJumping = value; }
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
        _playerHealth = GetComponent<PlayerHealth>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        _playerDash = GetComponent<PlayerDash>();
        //_playerGrappling = GetComponent<PlayerGrappling>();
    }

    private void Update()
    {
        if (_playerController.CanMove)
        {
            Jump();
            if (_isGrounded)
            {
                SetAnimationToFalse();
            }
        
            if (_isGrounded && !canJump)
            {
                checkJumpBug = true;
            }
            else if (!_isGrounded)
            {
                checkJumpBug = false;
            }
        
            if (checkJumpBug)
            {
                timer = 0.5f;
                time += Time.deltaTime;
                if (time > timer)
                {
                    checkJumpBug = false;
                    _isGrounded = true;
                    canJump = true;
                    _isJumping = false;
                    _isDoubleJumping = false;
                    time = 0f;
                }
            }
            else if (!checkJumpBug && !fallingLong)
            {
                time = 0f;
            }
        }
        
    }

    void Jump()
    {

        if (canJump == true && _isDoubleJumping == false && Input.GetAxis("Jump") > 0)
        {
            checkJumpBug = false;
            fallingLong = false;
            rb.gravityScale = 2;
            canJump = false;
            _isJumping = true;
            _isDoubleJumping = true;
            rb.velocity = Vector2.up * jumpForce;
            SoundManager.instace.Play(SoundManager.SoundName.Jump);
            
        }
        
        else if (!_isGrounded && _isJumping == true && _isDoubleJumping == true && Input.GetKeyDown(KeyCode.W))
        {
            checkJumpBug = false;
            fallingLong = false;
            time = 0f;
            rb.gravityScale = 2;
            _isDoubleJumping = false;
            rb.velocity = Vector2.up * jumpForce;
            SoundManager.instace.Play(SoundManager.SoundName.DoubleJump);
        }

        if (rb.velocity.y > 0 && !_playerDash.IsDashing)
        {
            _playerAnimation.State = PlayerAnimation.PlayerState.Jump;
        }
        else if (rb.velocity.y < 0 && !_playerDash.IsDashing)
        {
            _playerAnimation.State = PlayerAnimation.PlayerState.Fall;
        }
        
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (_playerController.CanMove)
        {
            if (((1 << col.gameObject.layer) & groundMask) != 0)
            {
                _playerAnimation.SetFalseAllAnimatorBoolean();
                _animator.SetBool("IsGround",true);
                fallingLong = false;
                checkJumpBug = false;
                rb.gravityScale = 2f;
                canJump = true;
                _isGrounded = true;
                _isJumping = false;
                _isDoubleJumping = false;
            }
        }
        
    }
    
    private void OnCollisionExit2D(Collision2D col)
    {
        if (_playerController.CanMove)
        {
            if (((1 << col.gameObject.layer) & groundMask) != 0)
            {
                _animator.SetBool("IsGround",false);
                _isGrounded = false;
                _isJumping = true;
            }
        }
    }

    private void SetAnimationToFalse()
    {
        _animator.SetBool("IsJumping",false);
        
    }




}
