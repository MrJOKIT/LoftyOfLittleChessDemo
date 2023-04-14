using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUltimate : MonoBehaviour
{
    [SerializeField] private TMP_Text ultimateInput;
    [SerializeField] private Image ultimateImage;
    [SerializeField] private float maxUltimatePoint;
    [SerializeField] private float ultimatePoint;
    private PlayerController _playerController;
    private PlayerHealth _playerHealth;
    //private float time;
    //private float timer = 0.38f;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
        ultimatePoint = _playerController.ultimatePoint;
        _playerHealth = GetComponent<PlayerHealth>();
    }

    public float MaxUltimatePoint
    { 
        get { return maxUltimatePoint; }
        set { maxUltimatePoint = value; }
    }
    public float UltimatePoint
    {
        get { return ultimatePoint; }
        set { ultimatePoint = value; }
    }

    private void Update()
    {
        UltimateSkill();
        
    }

    private void UltimateSkill()
    {
        ultimateImage.fillAmount = ultimatePoint;
        if (ultimatePoint >= 1)
        {
            ultimatePoint = 1f;
            ultimateInput.text = "Q";
        }
        else
        {
            ultimateInput.text = "";
        }

        if (ultimatePoint >= 1 && Input.GetKeyDown(KeyCode.Q))
        {
            ultimatePoint = 0;
            _playerHealth.PlayerTakeHealth(10);
            Debug.Log("Ultimate Skill!!!!");
        }
        else if (ultimatePoint > 1)
        {
            ultimatePoint = 1;
            Debug.Log("Ultimate is max");
        }
    }
}
