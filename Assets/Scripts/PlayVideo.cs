using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof(AudioSource))]

public class PlayVideo : MonoBehaviour {

    public MovieTexture intro;

    private AudioSource introAudio;

	void Start () {
        GetComponent<RawImage>().texture = intro as MovieTexture;
        introAudio = GetComponent<AudioSource>();
        introAudio.clip = intro.audioClip;
        intro.Play();
        introAudio.Play();
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space) && intro.isPlaying)
        {
            intro.Pause();
        }
        else if(Input.GetKeyDown(KeyCode.Space) && !intro.isPlaying)
        {
            intro.Play();
        }
        else if (Input.anyKeyDown)
        {
            Application.LoadLevel(1);
        }
	}
}
