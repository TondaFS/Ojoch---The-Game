using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class OjochMeowing : MonoBehaviour {

    public AudioSource meowingAudio;
    public float maxVolume = 1;

    void Awake()
    {
        meowingAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        // Na konzoli vypise jako tlacitko bylo zmackle
        {
            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(kcode))
                    Debug.Log("KeyCode down: " + kcode);
            }
        }
        */

        if (Input.GetButton("Fire2"))
        {
            meowingAudio.volume = Mathf.Clamp(meowingAudio.volume + (Time.deltaTime * 3), 0, maxVolume);
        }
        else
        {
            meowingAudio.volume = Mathf.Clamp(meowingAudio.volume - (Time.deltaTime * 3), 0, maxVolume);
        } 
    }
}
