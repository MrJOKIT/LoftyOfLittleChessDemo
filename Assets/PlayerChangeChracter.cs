using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerChangeChracter : MonoBehaviour
{
    [Header("Sprite")]
    [SerializeField] private Sprite king;
    [SerializeField] private Sprite queen;

    [Header("Animator")] 
    [SerializeField] private Animator kingAnimator;
    [SerializeField] private Animator kingAnimatorIcon;
    [SerializeField] private Animator queenAnimator;
    [SerializeField] private Animator queenAnimatorIcon;

    [Header("Cut-in")] 
    [SerializeField] private Transform cutInPos;
    [SerializeField] private GameObject kingCutIn;
    [SerializeField] private GameObject queenCutIn;

    [Header("Icon")] 
    [SerializeField] private GameObject kingIcon;
    [SerializeField] private GameObject kingChange;
    [SerializeField] private GameObject kingUltimate;
    [SerializeField] private GameObject queenIcon;
    [SerializeField] private GameObject queenChange;
    [SerializeField] private GameObject queenUltimate;

    [Header("Material")] 
    [SerializeField] private Material skillChange;
    [SerializeField] private Material skillChangeBack;
    
    [Header("Ref")] 
    private Animator playerAnimator;
    private PlayerHealth playerHealth;

    private float changeTime;
    [Header("Change Time")]
    [SerializeField] private float changeTimeCounter = 1f;
    [SerializeField] private Image changeImageKing;
    [SerializeField] private Image changeImageQueen;
    
    private bool isKing;

    public bool IsKing
    {
        get { return isKing; }
        set { isKing = value; }
    }

    private void Start()
    {
        isKing = true;
        playerAnimator = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        changeTime += Time.deltaTime;
        changeImageKing.fillAmount = changeTime / changeTimeCounter;
        changeImageQueen.fillAmount = changeTime / changeTimeCounter;
        var materailChange = changeTime / changeTimeCounter;
        var materailCurrent = materailChange / 2f;
        if (materailCurrent >= 0.4)
        {
            materailCurrent = 0.4f;
        }
        skillChange.SetFloat("_AddColorFade", materailCurrent);

        if (changeTime >= changeTimeCounter)
        {
            skillChangeBack.SetFloat("_FlameRadius",0.4f);
            skillChangeBack.SetFloat("_GlitchFade",0.05f);
        }
        else
        {
            skillChangeBack.SetFloat("_FlameRadius",5);
            skillChangeBack.SetFloat("_GlitchFade",0);
        }
        
        
        if (Input.GetKeyDown(KeyCode.C) && changeTime > changeTimeCounter)
        {
            if (isKing)
            {
                isKing = false;
                Instantiate(queenCutIn, cutInPos.position, Quaternion.identity,cutInPos);
                changeTime = 0f;
            }
            else
            {
                isKing = true;
                Instantiate(kingCutIn, cutInPos.position, Quaternion.identity,cutInPos);
                changeTime = 0f;
            }
        }
        
        if (isKing)
        {
            playerAnimator.SetLayerWeight(1,0f);
            playerAnimator.SetLayerWeight(2,1f);
            //playerAnimator = kingAnimator;
            queenIcon.SetActive(false);
            queenChange.SetActive(false);
            queenUltimate.SetActive(false);
            kingIcon.SetActive(true);
            kingChange.SetActive(true);
            kingUltimate.SetActive(true);
            playerHealth._animatorIcon = kingAnimatorIcon;
            
        }
        else
        {
            playerAnimator.SetLayerWeight(2,0f);
            playerAnimator.SetLayerWeight(1,1f);
            //playerAnimator = queenAnimator;
            kingIcon.SetActive(false);
            kingUltimate.SetActive(false);
            kingChange.SetActive(false);
            queenIcon.SetActive(true);
            queenChange.SetActive(true);
            queenUltimate.SetActive(true);
            playerHealth._animatorIcon = queenAnimatorIcon;
            
        }
    }
}
