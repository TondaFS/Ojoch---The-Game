using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof(AudioSource))]

public class PlayVideo : MonoBehaviour {

    public MovieTexture intro;
    public ScreenFader fader;

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
        if (Input.anyKeyDown)
        {
            introAudio.volume = 0;
            fader.FadeOutLoadNewScene("menu");
        }
        if (!intro.isPlaying)
        {
            fader.FadeOutLoadNewScene("menu");
        }
	}
}
