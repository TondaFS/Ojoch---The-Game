using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class OjochMeowing : MonoBehaviour {

    public AudioSource audio;
    public float maxVolume = 1;

    void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        {
            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(kcode))
                    Debug.Log("KeyCode down: " + kcode);
            }
        }

        if (Input.GetButton("Fire2"))
        {
            audio.volume = Mathf.Clamp(audio.volume + (Time.deltaTime * 3), 0, maxVolume);
        }
        else
        {
            audio.volume = Mathf.Clamp(audio.volume - (Time.deltaTime * 3), 0, maxVolume);
        } 
    }
}
