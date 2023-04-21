using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public enum PlayerState
    {
        Dash,
        Dead,
        Hurt,
        Attack,
        HeavyAttack,
        OtherNull,

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
        if (state == PlayerState.Dash)
        {
            SetFalseAllAnimatorBoolean();
            _animatorKing.SetBool("Dash",true);
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
        else if (state == PlayerState.OtherNull)
        {
            SetFalseAllAnimatorBoolean();
        }
    }

    public void SetFalseAllAnimatorBoolean()
    {
        _animatorKing.SetBool("HeavyAttack",false);
        _animatorKing.SetBool("IsDead",false);
        _animatorKing.SetBool("IsHurt",false);
        _animatorKing.SetBool("Dash",false);
    }
}
