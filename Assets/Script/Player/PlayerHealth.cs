using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Stats")] 
    [SerializeField] private int deathCount;
    [SerializeField] private int maxLife;
    [SerializeField] private int life;
    private PlayerController _playerController;
    private Animator _animator;
    private PlayerAnimation _playerAnimation;
    private InputManager _inputManager;
    private bool isDead = false;
    private bool isHurt;
    [SerializeField] private GameObject[] lifeImage;

    private float time;
    private float timer = 0.4f;
    
    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _animator = GetComponent<Animator>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        _inputManager = GetComponent<InputManager>();
        
        maxLife = _playerController.maxLifeCount;
        life = _playerController.lifeCount;
        
    }

    public int MaxLife
    {
        get { return maxLife; }
        set { maxLife = value; }
    }
    public int Life
    {
        get { return life; }
        set { life = value; }
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

    private void Update()
    {
        if (life <= 0)
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
        
        UpdateLifeImage();
    }
    
    private void Dead()
    {
        if (!isDead)
        {
            _animator.SetTrigger("Dead");
            isDead = true;
        }
        _inputManager.OpenMenu();
        _playerController.CanMove = false;
        _playerAnimation.State = PlayerAnimation.PlayerState.Dead;
    }

    public void PlayerTakeDamage()
    {
        var lifeArray = life - 1;
        lifeImage[lifeArray].SetActive(false);
        life--;

        _playerController.CanMove = false;
        _playerAnimation.State = PlayerAnimation.PlayerState.Hurt;
        isHurt = true;


    }

    private void UpdateLifeImage()
    {
        if (life < maxLife)
        {
            for (int i = life; i < lifeImage.Length; i++)
            {
                lifeImage[i].SetActive(false);
            }
        }
        
    }
    
    public void PlayerTakeHealth()
    {
        if (life < maxLife)
        {
            life++;
            var lifeArray = life - 1;
            lifeImage[lifeArray].SetActive(true);
            
        }
    }
}
