using System;
using System.Collections;
using System.Collections.Generic;
using Script.Sound;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;
            public static SoundManager instace;
            public enum SoundName
            {
                Jump,
                DoubleJump,
                Dash,
            }
        
            //How to use > SoundManager.instace.Play( SoundManager.SoundName.sound name); <//
            private void Awake()
            {
                if (instace == null)
                {
                    instace = this;
                }
                else
                {
                    Destroy( this );
                    return;
                }
                //DontDestroyOnLoad( gameObject );
            }
            /*void Start()
            {
                //play BGM1
                Play(SoundName.BGM1);
            }*/
        
            public void Play(SoundName name)
            {
                Sound sound = GetSound(name);
                if (sound.audioSource == null)
                {
                    sound.audioSource = gameObject.AddComponent<AudioSource>();
    
                    sound.audioSource.outputAudioMixerGroup = sound.AudioMixerGroup;
                    sound.audioSource.clip = sound.clip;
                    sound.audioSource.volume = sound.volume;
                    sound.audioSource.loop = sound.loop;
                    
                }
                
                sound.audioSource.Play();
            }
    
            public void Stop(SoundName name)
            {
                Sound sound = GetSound(name);
                sound.audioSource.Stop();
            }
        
            private Sound GetSound( SoundName name)
            {
                return Array.Find(sounds, s => s.soundName == name);
            }
}
