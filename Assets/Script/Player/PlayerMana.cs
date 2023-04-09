using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMana : MonoBehaviour
{
    [SerializeField] private Image mpImage;
    [SerializeField] private float maxMp;
    [SerializeField] private float mp;
    [SerializeField] private float regenTime;
    private bool _isRegenMp;
    private float time;

    private PlayerController _playerController;
    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
        maxMp = _playerController.maxMpCount;
        mp = _playerController.mpCount;
    }

    public float MaxMp
    {
        get { return maxMp; }
        set { maxMp = value; }
    }
    public float Mp
    {
        get { return mp; }
        set { mp = value; }
    }
    

    private void Update()
    {
        SpRegain();
    }
    
    

    private void SpRegain()
    {
        mpImage.fillAmount = mp;
        if (mp < maxMp)
        {
            mp += Time.deltaTime * regenTime;
        }
        else if (mp >= maxMp)
        {
            mp = 1;
        }
    }
}
