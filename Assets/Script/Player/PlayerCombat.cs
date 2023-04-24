using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerCombat : MonoBehaviour
{
    public static PlayerCombat instance;
    private Animator _animator;
    
    private PlayerMovement _playerMovement;
    private PlayerHealth playerHealth;
    private Rigidbody2D rb;
    

    [SerializeField] private Transform vfxSlash;
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private Transform attackPoint;

    private bool delayAttack = false;
    private float delayAttackTime;
    public float delayAttackCounter;

    public int combo;
    public bool attacking;
    public AudioMixerGroup audioMixer;
    public AudioSource audioSource;
    public AudioClip[] sound;
    
    public LayerMask enemyLayers;
    
    
    public float AttackDamage
    {
        get { return attackDamage; }
        set { attackDamage = value; }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        playerHealth = GetComponent<PlayerHealth>();
        _animator = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();

    }

    public void Start_Combo()
    {
        _playerMovement.canMove = true;
        attacking = false;
        if (combo < 2)
        {
            combo++;
        }
    }

    public void Finish_Combo()
    {
        delayAttack = true;
        _playerMovement.CanMove = true;
        _animator.SetBool("IsAttack",false);
        attacking = false;
        combo = 0;
    }

    private void Update()
    {

        if (!playerHealth.IsDead)
        {
            Combo();
        }

        if (delayAttack)
        {
            delayAttackTime += Time.deltaTime;
            if (delayAttackTime >= delayAttackCounter )
            {
                delayAttack = false;
                delayAttackTime = 0f;
            }
        }
        

    }
    

    public void Combo()
    {
        if (Input.GetKeyDown(KeyCode.J) && !attacking && _playerMovement.onGround && _playerMovement.canMove )
        {
            delayAttackTime = 0f;
            rb.velocity = Vector2.zero;
            attacking = true;
            _playerMovement.CanMove = false;
            _animator.SetBool("IsAttack",true);
            _animator.SetTrigger("attack"+combo);
            audioSource.outputAudioMixerGroup = audioMixer;
            audioSource.clip = sound[combo];
            audioSource.Play();
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            // Damage them
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            }
        }
        else if (Input.GetKeyDown(KeyCode.J) && !attacking && !_playerMovement.onGround  && !delayAttack)
        {
            
            attacking = true;
            _playerMovement.CanMove = false;
            _animator.SetBool("IsAttack",true);
            _animator.SetTrigger("attackAir"+combo);
            audioSource.outputAudioMixerGroup = audioMixer;
            audioSource.clip = sound[combo];
            audioSource.Play();
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            // Damage them
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            }
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

    public void InstanceSfx()
    {
        Instantiate(vfxSlash, attackPoint.transform.position,attackPoint.transform.rotation);
    }
    
    
}
