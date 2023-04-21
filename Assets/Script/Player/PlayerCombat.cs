using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator _animator;
    
    private PlayerMovement _playerMovement;
    private PlayerHealth _playerHealth;
    private PlayerAnimation _playerAnimation;
    private SoundManager soundManager;

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
        _playerMovement = GetComponent<PlayerMovement>();
        _playerHealth = GetComponent<PlayerHealth>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();

    }

    private void Update()
    {
        if (_playerMovement.CanMove)
        {
            if (Input.GetKeyDown(KeyCode.J) )
            {
                Attack();
            }
            else if (Input.GetKey(KeyCode.K) )
            {
                Counter();
            }
            else if (Input.GetKeyUp(KeyCode.K) )
            {
                _animator.SetBool("HeavyAttack",false);
            }
        }

        if (!_playerMovement.CanMove && attack1)
        {
            timer = 0.38f;
            time += Time.deltaTime;
            if (time > timer)
            {
                _playerMovement.CanMove = true;
                _playerMovement.InCombat = false;
                attack1 = false;
                time = 0f;
            }
        }
        else if (!_playerMovement.CanMove && attack2)
        {
            timer = 0.4f;
            time += Time.deltaTime;
            if (time > timer)
            {
                _playerMovement.CanMove = true;
                _playerMovement.InCombat = false;
                attack1 = false;
                _animator.SetBool("HeavyAttack",false);
                time = 0f;
            }
        }
        
    }

    private void Attack()
    {
        SoundManager.instace.Play(SoundManager.SoundName.Attack1);
        attack1 = true;
        _playerMovement.CanMove = false;
        _playerMovement.InCombat = true;
        
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
        _playerMovement.CanMove = false;
        _playerMovement.InCombat = true;
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
