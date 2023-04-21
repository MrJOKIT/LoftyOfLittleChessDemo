using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using UnityEditor.Experimental.GraphView;
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
    
    void Start()
    {
        _playerHealth = GetComponent<PlayerHealth>();
        _playerMana = GetComponent<PlayerMana>();
        _playerUltimate = GetComponent<PlayerUltimate>();
        playerChangeChracter = GetComponent<PlayerChangeChracter>();
        

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
        data.isKing = playerChangeChracter.IsKing;
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
    }
    
    
}
