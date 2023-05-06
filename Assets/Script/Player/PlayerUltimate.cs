using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUltimate : MonoBehaviour
{
    [SerializeField] private TMP_Text ultimateInput;
    [SerializeField] private Image kingUltimateImage;
    [SerializeField] private Image queenUltimateImage;
    [SerializeField] private float maxUltimatePoint;
    [SerializeField] private float ultimatePoint;
    private PlayerController _playerController;
    private PlayerHealth _playerHealth;
    [SerializeField] private Material materialKing;
    [SerializeField] private Material materialQueen;
    [SerializeField] private Material backMaterial;
    private int fadePropertyID;
    //private float time;
    //private float timer = 0.38f;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
        ultimatePoint = _playerController.ultimatePoint;
        _playerHealth = GetComponent<PlayerHealth>();
        fadePropertyID = Shader.PropertyToID("_RainbowFade");
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
        kingUltimateImage.fillAmount = ultimatePoint;
        queenUltimateImage.fillAmount = ultimatePoint;

        var currentFade = kingUltimateImage.fillAmount / 2.5f;
        materialKing.SetFloat("_AddColorFade",currentFade);
        materialQueen.SetFloat("_AddColorFade",currentFade);
        if (ultimatePoint >= 1)
        {
            ultimatePoint = 1f;
            materialKing.SetFloat("_RainbowFade",1f);
            materialQueen.SetFloat("_RainbowFade",1f);
            backMaterial.SetFloat("_GlitchFade",0.25f);
            ultimateInput.text = "Q";
        }
        else
        {
            materialKing.SetFloat("_RainbowFade",0f);
            materialQueen.SetFloat("_RainbowFade",0f);
            backMaterial.SetFloat("_GlitchFade",0);
            ultimateInput.text = "";
        }

        if (ultimatePoint >= 1 && Input.GetKeyDown(KeyCode.Q))
        {
            ultimatePoint = 0;
            _playerHealth.PlayerTakeHealth(50);
            Debug.Log("Ultimate Skill!!!!");
        }
        else if (ultimatePoint > 1)
        {
            ultimatePoint = 1;
            Debug.Log("Ultimate is max");
        }
    }
}
