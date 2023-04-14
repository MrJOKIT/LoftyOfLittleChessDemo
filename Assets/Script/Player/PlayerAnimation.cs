using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public enum PlayerState
    {
        Idle,
        Walk,
        Fall,
        Jump,
        Dead,
        Hurt,
        Attack,
        Dash,
        HeavyAttack,

    }

    [SerializeField] private PlayerState state;
    private Animator _animatorKing;
    
    private PlayerJump _playerJump;
    
    

    public PlayerState State
    {
        get { return state; }
        set { state = value; }
    }

    private void Start()
    {
        _animatorKing = GetComponent<Animator>();
        _playerJump = GetComponent<PlayerJump>();
    }

    void Update()
    {
        if (state == PlayerState.Idle)
        {
            SetFalseAllAnimatorBoolean();
            _animatorKing.SetBool("IsIdle",true);
        }
        else if (state == PlayerState.Walk)
        {
            SetFalseAllAnimatorBoolean();
            _animatorKing.SetBool("IsWalking",true);
        }
        else if (state == PlayerState.Jump && !_playerJump.IsGround)
        {
            SetFalseAllAnimatorBoolean();
            _animatorKing.SetBool("IsJumping",true);
        }
        else if (state == PlayerState.Fall && !_playerJump.IsGround)
        {
            SetFalseAllAnimatorBoolean();
            _animatorKing.SetBool("IsFalling",true);
        }
        else if (state == PlayerState.Attack)
        {
            SetFalseAllAnimatorBoolean();
            _animatorKing.ResetTrigger("NormalAttack");
            _animatorKing.SetTrigger("NormalAttack");
        }
        else if (state == PlayerState.HeavyAttack)
        {
            SetFalseAllAnimatorBoolean();
            _animatorKing.SetBool("HeavyAttack",true);
            /*_animator.ResetTrigger("HeavyAttack");
            _animator.SetTrigger("HeavyAttack");*/
        }
        else if (state == PlayerState.Dash)
        {
            SetFalseAllAnimatorBoolean();
            _animatorKing.ResetTrigger("IsDashing");
            _animatorKing.SetTrigger("IsDashing");
        }
        else if (state == PlayerState.Hurt)
        {
            SetFalseAllAnimatorBoolean();
            _animatorKing.SetBool("IsHurt",true);
            
        }
        else if (state == PlayerState.Dead)
        {
            SetFalseAllAnimatorBoolean();
            _animatorKing.SetBool("IsDead",true);
        }
    }

    public void SetFalseAllAnimatorBoolean()
    {
        _animatorKing.SetBool("IsIdle",false);
        _animatorKing.SetBool("IsWalking",false);
        _animatorKing.SetBool("IsJumping",false);
        _animatorKing.SetBool("IsFalling",false);
        _animatorKing.SetBool("HeavyAttack",false);
        _animatorKing.SetBool("IsDead",false);
        _animatorKing.SetBool("IsHurt",false);
    }
}
