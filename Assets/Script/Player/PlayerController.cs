using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, IDataPersistence
{
    
    private Vector3 lastCheckPoint;


    [Header("Player Setting")]
    private bool inCombat;

    public float maxHealthCount;
    public float healthCount;
    public float maxMpCount;
    public float mpCount;
    public float ultimatePoint;
    public int deathCount;

    public LayerMask wallMask;
    [SerializeField] private float playerSpeed;
    [SerializeField] private Camera m_camera;
    
    [Header("Player Panel")] 
    [SerializeField] private GameObject deadPanel;
    private bool isLeft;
    private bool canMove = true;
    private bool isTouchingWall;
    public static PlayerController instance;
    
    [Header("Ref")]
    private SoundManager soundManager;
    private PlayerHealth _playerHealth;
    private Rigidbody2D rb;
    private SpriteRenderer _spriteRenderer;
    private PlayerMana _playerMana;
    private PlayerUltimate _playerUltimate;
    private PlayerAnimation _playerAnimation;
    private PlayerJump _playerJump;

    public bool IsLeft
    {
        get { return isLeft; }
        set { isLeft = value; }
    }

    public bool IsTouchingWall
    {
        get { return isTouchingWall; }
        set { isTouchingWall = value; }
    }

    public bool CanMove
    {
        get { return canMove; }
        set { canMove = value; }
    }
    
    public bool InCombat
    {
        get { return inCombat; }
        set { inCombat = value; }
    }
    
    public Vector3 LastCheckPoint
    {
        get { return lastCheckPoint; }
        set { lastCheckPoint = value; }
    }
    
    void Start()
    {
        instance = this;
        _playerHealth = GetComponent<PlayerHealth>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
         rb = GetComponent<Rigidbody2D>();
        _playerMana = GetComponent<PlayerMana>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        _playerUltimate = GetComponent<PlayerUltimate>();
        _playerJump = GetComponent<PlayerJump>();

    }
    
    public void SaveData(GameData data)
    {
        data.maxHealthCount = _playerHealth.MaxHelath;
        data.healthCount = _playerHealth.Health;
        data.maxMpCount = _playerMana.MaxMp;
        data.mpCount = _playerMana.Mp;
        data.maxUltimatePoint = _playerUltimate.MaxUltimatePoint;
        data.ultimatePoint = _playerUltimate.UltimatePoint;
        data.lastCheckPoint = transform.position;
        data.deathCount = _playerHealth.DeathCount; 
    }
    public void LoadData(GameData data)
    {
        lastCheckPoint = data.lastCheckPoint;
        maxHealthCount = data.maxHealthCount;
        healthCount = data.healthCount;
        maxMpCount = data.maxMpCount;
        mpCount = data.mpCount;
        ultimatePoint = data.ultimatePoint;
        transform.position = data.lastCheckPoint;
        deathCount = data.deathCount;
    }
    
    private void Update()
    {

        if (soundManager == null)
        {
            soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        }

        if (canMove)
        {
            Movement();
        }
        
    }

    void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        Vector3 movment = new Vector3(x, 0f);
        transform.position += movment * playerSpeed * Time.deltaTime;

        if (x < 0  && !inCombat)
        {
            if (!isLeft)
            {
                Flip();
            }

            if (_playerJump.IsGround)
            {
                _playerAnimation.State = PlayerAnimation.PlayerState.Walk;
            }
            
        }
        else if (x > 0 && !inCombat)
        {
            if (isLeft)
            {
                Flip();
            }
            if (_playerJump.IsGround)
            {
                _playerAnimation.State = PlayerAnimation.PlayerState.Walk;
            }
        }
        else if (x == 0 && rb.velocity.y == 0 && !_playerHealth.IsDead && !inCombat)
        {
            _playerAnimation.State = PlayerAnimation.PlayerState.Idle;
        }
        

    }

    private void Flip()
    {
        isLeft = !isLeft;
        transform.Rotate(0f,180f,0f);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (((1 << col.gameObject.layer) & wallMask) != 0)
        {
            isTouchingWall = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (((1 << other.gameObject.layer) & wallMask) != 0)
        {
            isTouchingWall = false;
        }
    }
}
