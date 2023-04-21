using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    /*[Header("Jump Setting")]
    [SerializeField] private float jumpForce;

    [SerializeField] private LayerMask groundMask;
    private bool canJump = false;
    private bool _isJumping = false;
    private bool _isDoubleJumping = false;
    private bool _isDouble = false;
    private bool _isGrounded = false;
    private bool checkJumpBug = false;
    private bool fallingLong = false;
    //private bool jumpBug;
    private Rigidbody2D rb;
    private Animator _animator;
    private PlayerController _playerController;
    private PlayerHealth _playerHealth;
    private PlayerAnimation _playerAnimation;
    private PlayerDash _playerDash;

    private float time;
    private float timer = 0.5f;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;
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

            if (IsGround)
            {
                coyoteTimeCounter = coyoteTime;
            }
            else
            {
                coyoteTimeCounter -= Time.deltaTime;
            }
        
            if (_isGrounded && !canJump)
            {
                checkJumpBug = true;
            }
            else if (!_isGrounded)
            {
                checkJumpBug = false;
            }

            if (Input.GetButtonDown("Jump"))
            {
                jumpBufferCounter = jumpBufferTime;
            }
            else
            {
                jumpBufferCounter -= Time.deltaTime;
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
            else if (_isJumping && !_isGrounded && !canJump && !_isDoubleJumping && !checkJumpBug && !fallingLong)
            {
                time = 5f;
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

        if (coyoteTimeCounter > 0f && canJump == true && _isDoubleJumping == false && jumpBufferCounter > 0f )
        {
            checkJumpBug = false;
            fallingLong = false;
            rb.gravityScale = 2;
            //jumpBug = true;
            canJump = false;
            _isJumping = true;
            _isDoubleJumping = true;
            rb.velocity = Vector2.up * jumpForce;
            jumpBufferCounter = 0f;
            SoundManager.instace.Play(SoundManager.SoundName.Jump);
            
        }
        
        else if (!_isGrounded && !_isDouble && _isJumping == true && _isDoubleJumping == true && Input.GetKeyDown(KeyCode.UpArrow))
        {
            checkJumpBug = false;
            fallingLong = false;
            time = 0f;
            rb.gravityScale = 2;
            //jumpBug = true;
            _isDoubleJumping = false;
            _isDouble = true;
            rb.velocity = Vector2.up * jumpForce;
            coyoteTimeCounter = 0f;
            
            SoundManager.instace.Play(SoundManager.SoundName.DoubleJump);
        }
        
        

        if (rb.velocity.y > 0 && !_playerDash.IsDashing)
        {
            _playerAnimation.State = PlayerAnimation.PlayerState.Jump;
        }
        else if (rb.velocity.y < 0  && !_playerDash.IsDashing)
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
                _isDouble = false;
                //jumpBug = false;
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

    public void SetToDefault()
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
        _isDouble = false;
        //jumpBug = false;
    }*/




}
