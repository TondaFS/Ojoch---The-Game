using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Skript unikátního chévání krysy: kamikaze
/// </summary>
public class RatAI : MonoBehaviour {    
    void Start()
    {
        GetComponent<CommonAI>().startingState = AIStates.kamikaze;
        SessionController.instance.ratsInScene.Add(this.gameObject);

        if(SessionController.instance.sputniksInScene.Count > 0)
        {
            GetComponent<CommonAI>().startingState = AIStates.chargeAttack;
        }
    }    
}
