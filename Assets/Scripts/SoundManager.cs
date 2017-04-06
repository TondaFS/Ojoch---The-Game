using UnityEngine;
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
    public AudioClip clipOjochDeath;       

    public AudioClip squirrelDeath;
    public AudioClip pokoutnikDeath;
    public AudioClip birdDeath;
    public AudioClip ratDeath;
    public AudioClip pigDeath;

    public AudioClip ghost;
    public AudioClip vodka;

    public AudioClip clipEnemyHit;
    public AudioClip clipBobblePoop;

    public AudioSource soundAudioSource; 
    
    void Start () {
        soundAudioSource = GetComponent<AudioSource>();
    }	

    //Prehraje zvuk
    public void PlaySound(AudioClip sound){
        soundAudioSource.pitch = 1;
        soundAudioSource.PlayOneShot(sound, soundsVolume);
    }

    public void PlaySoundPitchShift(AudioClip sound)
    {
        soundAudioSource.pitch = Random.Range(0.92f, 1.08f);
        soundAudioSource.PlayOneShot(sound, soundsVolume - 0.2f);
    }

    //Prehraje nahodny zvuk 
    public void PlayRandom(params AudioClip[] clips) {
        int randomIndex = Random.Range(0, clips.Length);
        AudioClip clip = clips[randomIndex];
        soundAudioSource.PlayOneShot(clip, soundsVolume);
    }

    //Nastavi Pitch
    public void setPitch(AudioSource source, float pitch) {
        source.pitch = pitch;
    }    

    //Vsechno ztlumi
    public void MuteEverything(bool bolean)
    {        
        soundAudioSource.mute = bolean;
        GameObject.Find("Music").GetComponent<AudioSource>().mute = bolean;
        GameObject.Find("Ojoch").GetComponent<AudioSource>().mute = bolean;
        GameObject.Find("Ojoch meowing").GetComponent<AudioSource>().mute = bolean;
    }
}
