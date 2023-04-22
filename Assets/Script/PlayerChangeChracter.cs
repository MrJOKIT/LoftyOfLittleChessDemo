using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerChangeChracter : MonoBehaviour
{

    public ParticleSystem changeEffect;
    [Header("Animator")]
    [SerializeField] private Animator kingAnimatorIcon;
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
    [SerializeField] private Material playerMats;
    
    [Header("Ref")] 
    private Animator playerAnimator;
    private PlayerHealth playerHealth;
    private PlayerController playerController;
    private PlayerMovement playerMovement;
    private SoundManager soundManager;
    private SineMovement sineMovement;

    private float changeTime;
    [Header("Change Time")]
    [SerializeField] private float changeTimeCounter;
    [SerializeField] private Image changeImageKing;
    [SerializeField] private Image changeImageQueen;
    [SerializeField] private TextMeshProUGUI countText;
    private int cooldown;
    private int i = 1;

    [Header("Sine Move")] 
    public Transform wrapQueen;
    public float sineSpeed = 1f;
    public float sineWave = 1f;
    
    
    private bool isKing;

    public bool IsKing
    {
        get { return isKing; }
        set { isKing = value; }
    }

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerController = GetComponent<PlayerController>();
        playerAnimator = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();
        sineMovement = GetComponent<SineMovement>();
        cooldown = Convert.ToInt32(changeTimeCounter);
        isKing = playerController.isKingController;
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        
    }

    private void Update()
    {
        
        changeTime += Time.deltaTime;
        changeImageKing.fillAmount = changeTime / changeTimeCounter;
        changeImageQueen.fillAmount = changeTime / changeTimeCounter;

        if (changeTime >= changeTimeCounter)
        {
            countText.fontSize = 24;
            countText.text = "Switch";
            skillChangeBack.SetFloat("_GlitchFade",0.05f);
        }
        else if (changeTime <= changeTimeCounter)
        {
            if (changeTime > i)
            {
                cooldown--;
                i++;
            }
            countText.fontSize = 36;
            countText.text = "" + cooldown;
            skillChangeBack.SetFloat("_GlitchFade",0);
        }
        
        
        if (Input.GetKeyDown(KeyCode.C) && changeTime > changeTimeCounter)
        {
            changeEffect.Play();
            SoundManager.instace.Play(SoundManager.SoundName.ChangeCharacter);
            if (isKing)
            {
                isKing = false;
                //transform.position = wrapQueen.transform.position;
                Instantiate(queenCutIn, cutInPos.position, Quaternion.identity,cutInPos);
                changeTime = 0f;
                i = 1;
                cooldown = Convert.ToInt32(changeTimeCounter);
            }
            else
            {
                isKing = true;
                Instantiate(kingCutIn, cutInPos.position, Quaternion.identity,cutInPos);
                changeTime = 0f;
                i = 1;
                cooldown = Convert.ToInt32(changeTimeCounter);
            }
        }
        
        if (isKing)
        {
            /*playerMats.SetFloat("_SineMoveFade", 0f);
            playerMats.SetFloat("_SineMoveOffset", 0f);*/
            /*playerMovement.groundLength = 1.07f;
            sineMovement.enabled = false;*/
            playerAnimator.SetLayerWeight(1,0f);
            playerAnimator.SetLayerWeight(2,1f);
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
            /*playerMats.SetFloat("_SineMoveFade", 1f);*/
            /*playerMovement.groundLength = 1.5f;
            sineMovement.enabled = false;*/
            playerAnimator.SetLayerWeight(2,0f);
            playerAnimator.SetLayerWeight(1,1f);
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
