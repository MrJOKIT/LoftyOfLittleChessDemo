using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator _animator;
    
    private PlayerController _playerController;
    private PlayerHealth _playerHealth;
    private PlayerJump _playerJump;
    private PlayerAnimation _playerAnimation;

    [SerializeField] private Transform vfxSlash;
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private Transform attackPoint;
    
    public LayerMask enemyLayers;

    private float time;
    private float timer;

    private bool attack1, attack2; 
    
    public float AttackDamage
    {
        get { return attackDamage; }
        set { attackDamage = value; }
    }
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
        _playerHealth = GetComponent<PlayerHealth>();
        _playerJump = GetComponent<PlayerJump>();
        _playerAnimation = GetComponent<PlayerAnimation>();

    }

    private void Update()
    {
        if (_playerController.CanMove)
        {
            if (Input.GetMouseButtonDown(0) && !_playerJump.IsJump)
            {
                Attack();
            }
            else if (Input.GetMouseButton(0) && !_playerJump.IsJump)
            {
                Counter();
            }
            else if (Input.GetMouseButtonUp(0) && !_playerJump)
            {
                _animator.SetBool("HeavyAttack",false);
            }
        }

        if (!_playerController.CanMove && attack1)
        {
            timer = 0.38f;
            time += Time.deltaTime;
            if (time > timer)
            {
                _playerController.CanMove = true;
                _playerController.InCombat = false;
                attack1 = false;
                time = 0f;
            }
        }
        else if (!_playerController.CanMove && attack2)
        {
            timer = 0.4f;
            time += Time.deltaTime;
            if (time > timer)
            {
                _playerController.CanMove = true;
                _playerController.InCombat = false;
                attack1 = false;
                _animator.SetBool("HeavyAttack",false);
                time = 0f;
            }
        }
        
    }

    private void Attack()
    {
        
        attack1 = true;
        _playerController.CanMove = false;
        _playerController.InCombat = true;
        // Play an attack animation

        _playerAnimation.State = PlayerAnimation.PlayerState.Attack;
        Instantiate(vfxSlash, attackPoint.transform.position,attackPoint.transform.rotation);
            
        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,attackRange,enemyLayers);
            
        // Damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            
        }
    }

    private void Counter()
    {
        
        attack2 = true;
        _playerController.CanMove = false;
        _playerController.InCombat = true;
        // Play an attack animation

        _playerAnimation.State = PlayerAnimation.PlayerState.HeavyAttack;
        //Instantiate(vfxSlash, attackPoint.transform.position, attackPoint.transform.rotation);

        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        
        Gizmos.DrawWireSphere(attackPoint.position,attackRange);
    }
    
    
}
