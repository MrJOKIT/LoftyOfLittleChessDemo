using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Stats")] 
    [SerializeField] private float immortalTime;
    [SerializeField] private int deathCount;
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;
    private PlayerController _playerController;
    private Animator _animator;
    private PlayerAnimation _playerAnimation;
    private InputManager _inputManager;
    private Knockback knockBack;
    private PlayerJump playerJump;
    private bool isDead = false;
    private bool isHurt;
    private bool immortalPlayer = false;
    [SerializeField] private Image hpImage;
    public Animator _animatorIcon;
    public PlayerMenu playerMenu;

    private float time;
    private float timer = 0.4f;
    
    
    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _animator = GetComponent<Animator>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        _inputManager = GetComponent<InputManager>();
        knockBack = GetComponent<Knockback>();
        playerJump = GetComponent<PlayerJump>();

        maxHealth = _playerController.maxHealthCount;
        health = _playerController.healthCount;
        
    }

    public float MaxHelath
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }
    public float Health
    {
        get { return health; }
        set { health = value; }
    }
    public int DeathCount
    {
        get { return deathCount; }
        set { deathCount = value; }
    }
    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }
    
    public bool IsHurt
    {
        get { return isHurt; }
        set { isHurt = value; }
    }

    private void Update()
    {
        if (health <= 0)
        {
            Dead();
        }
        
        if (isHurt)
        {
            time += Time.deltaTime;
            if (time > timer)
            {
                isHurt = false;
                _playerController.CanMove = true;
                time = 0f;
            }
        }
        
        UpdateHealth();
    }
    
    private void Dead()
    {
        if (!isDead)
        {
            _animator.SetTrigger("Dead");
            isDead = true;
        }
        playerMenu.OpenMenu();
        _playerController.CanMove = false;
        _playerAnimation.State = PlayerAnimation.PlayerState.Dead;
    }

    public void PlayerTakeDamage(float damage)
    {
        if (!immortalPlayer)
        {
            _animatorIcon.SetTrigger("Hurt");
            playerJump.SetToDefault();
            knockBack.KnockbackHit(transform);
            health -= damage;

            _playerController.CanMove = false;
            _playerAnimation.State = PlayerAnimation.PlayerState.Hurt;
            isHurt = true;
            immortalPlayer = true;
            StartCoroutine(ImmortalTime());
        }
    }

    private void UpdateHealth()
    {
        var currentHealthImage = health / maxHealth;
        hpImage.fillAmount = currentHealthImage;
    }
    
    public void PlayerTakeHealth(float healthUp)
    {
        if (health < maxHealth)
        {
            health += healthUp;
            if (health > maxHealth)
            {
                health = maxHealth;
            }
            
        }
    }
    
    private IEnumerator ImmortalTime()
    {
        yield return new WaitForSeconds(immortalTime);
        immortalPlayer = false;
    }
}
