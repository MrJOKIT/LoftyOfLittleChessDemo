using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, IDataPersistence
{
    
    private Vector3 lastCheckPoint;


    [Header("Player Save")]
    public float maxHealthCount;
    public float healthCount;
    public float maxMpCount;
    public float mpCount;
    public float ultimatePoint;
    public int deathCount;
    public bool isKingController;
    private bool isGround;

    [Header("Inventory")] 
    public TextMeshProUGUI coinText;
    private int coin;

    [Header("Ref")]
    private PlayerHealth _playerHealth;
    private PlayerMana _playerMana;
    private PlayerUltimate _playerUltimate;
    private PlayerChangeChracter playerChangeChracter;
    
    
    public Vector3 LastCheckPoint
    {
        get { return lastCheckPoint; }
        set { lastCheckPoint = value; }
    }

    public int Coin
    {
        get { return coin; }
        set { coin = value; }
    }
    
    void Start()
    {
        _playerHealth = GetComponent<PlayerHealth>();
        _playerMana = GetComponent<PlayerMana>();
        _playerUltimate = GetComponent<PlayerUltimate>();
        playerChangeChracter = GetComponent<PlayerChangeChracter>();
    }

    private void Update()
    {
        coinText.text = "" + coin;
    }

    public void SaveData(GameData data)
    {
        data.maxHealthCount = _playerHealth.MaxHelath;
        data.healthCount = _playerHealth.Health;
        data.maxMpCount = _playerMana.MaxMp;
        data.mpCount = _playerMana.Mp;
        data.maxUltimatePoint = _playerUltimate.MaxUltimatePoint;
        data.ultimatePoint = _playerUltimate.UltimatePoint;
        //แก้ตอนจะเปลื่ยนจากอิงตำแหน่งตาม CheckPoint
        data.lastCheckPoint = transform.position;
        //แก้ตอนจะเปลื่ยนจากอิงตำแหน่งตาม CheckPoint
        data.deathCount = _playerHealth.DeathCount;
        data.isKing = playerChangeChracter.IsKing;
        data.coin = coin;
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
        this.isKingController = data.isKing;
        coin = data.coin;
    }
    
    
}
