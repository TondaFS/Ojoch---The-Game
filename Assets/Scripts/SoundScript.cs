using UnityEngine;
using System.Collections;

public class SoundScript : MonoBehaviour {

    public AudioSource efxSource;       //zvukove efekty
    public static SoundScript instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
    public void PlaySingle(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }
    public void RandomSFX(params AudioClip[] clips) {
        int randomIndex = Random.Range(0, clips.Length);
        efxSource.clip = clips[randomIndex];
        efxSource.Play();

    }

	
}
