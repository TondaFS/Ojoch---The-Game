﻿using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public float musicVolume = 1;
    public float soundsVolume = 1;

    //Sound clips
    public AudioClip clipShoot;
    public AudioClip clipDamage1;
    public AudioClip clipDamage2;
    public AudioClip clipGrab;
    public AudioClip clipAk47;
    public AudioClip clipGood;
    public AudioClip clipBad;

    public AudioClip clipEnemyHit;
    public AudioClip clipBobblePoop;

    public AudioSource soundAudioSource; 
    
    void Start () {
        soundAudioSource = GetComponent<AudioSource>();
    }	

    public void PlaySound(AudioClip sound){
        soundAudioSource.PlayOneShot(sound, soundsVolume);
    }

    public void PlayRandom(params AudioClip[] clips) {
        int randomIndex = Random.Range(0, clips.Length);
        AudioClip clip = clips[randomIndex];
        soundAudioSource.PlayOneShot(clip, soundsVolume);
    }

    public void setPitch(AudioSource source, float pitch) {
        source.pitch = pitch;
    }    
}
