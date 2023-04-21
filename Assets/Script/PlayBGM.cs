using System;
using System.Collections;
using System.Collections.Generic;
using Script.Sound;
using UnityEngine;

public class PlayBGM : MonoBehaviour
{
    public SoundManager.SoundName soundName;
    private SoundManager soundManager;
    [SerializeField] private bool isPlay;

    private void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        if (isPlay)
        {
            SoundManager.instace.Play(soundName);
        }
        
    }
}
