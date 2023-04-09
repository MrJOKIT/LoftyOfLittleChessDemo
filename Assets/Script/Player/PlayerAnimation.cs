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
    private Animator _animator;
    private PlayerJump _playerJump;
    
    

    public PlayerState State
    {
        get { return state; }
        set { state = value; }
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _playerJump = GetComponent<PlayerJump>();
    }

    void Update()
    {
        if (state == PlayerState.Idle)
        {
            SetFalseAllAnimatorBoolean();
            _animator.SetBool("IsIdle",true);
        }
        else if (state == PlayerState.Walk)
        {
            SetFalseAllAnimatorBoolean();
            _animator.SetBool("IsWalking",true);
        }
        else if (state == PlayerState.Jump && !_playerJump.IsGround)
        {
            SetFalseAllAnimatorBoolean();
            _animator.SetBool("IsJumping",true);
        }
        else if (state == PlayerState.Fall && !_playerJump.IsGround)
        {
            SetFalseAllAnimatorBoolean();
            _animator.SetBool("IsFalling",true);
        }
        else if (state == PlayerState.Attack)
        {
            SetFalseAllAnimatorBoolean();
            _animator.ResetTrigger("NormalAttack");
            _animator.SetTrigger("NormalAttack");
        }
        else if (state == PlayerState.HeavyAttack)
        {
            SetFalseAllAnimatorBoolean();
            _animator.SetBool("HeavyAttack",true);
            /*_animator.ResetTrigger("HeavyAttack");
            _animator.SetTrigger("HeavyAttack");*/
        }
        else if (state == PlayerState.Dash)
        {
            SetFalseAllAnimatorBoolean();
            _animator.ResetTrigger("IsDashing");
            _animator.SetTrigger("IsDashing");
        }
        else if (state == PlayerState.Hurt)
        {
            SetFalseAllAnimatorBoolean();
            _animator.SetBool("IsHurt",true);
        }
        else if (state == PlayerState.Dead)
        {
            SetFalseAllAnimatorBoolean();
            _animator.SetBool("IsDead",true);
        }
    }

    public void SetFalseAllAnimatorBoolean()
    {
        _animator.SetBool("IsIdle",false);
        _animator.SetBool("IsWalking",false);
        _animator.SetBool("IsJumping",false);
        _animator.SetBool("IsFalling",false);
        _animator.SetBool("HeavyAttack",false);
        _animator.SetBool("IsDead",false);
        _animator.SetBool("IsHurt",false);
    }
}
