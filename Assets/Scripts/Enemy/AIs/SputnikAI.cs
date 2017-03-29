using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SputnikAI : MonoBehaviour {
    CommonAI commonAI;

    void Start()
    {
        commonAI = GetComponent<CommonAI>();
        commonAI.startingState = AIStates.stopAndShoot;
        SessionController.instance.sputniksInScene.Add(this.gameObject);
        MakeRatsCharge();
        MakeSquirrelsMove();
    }

    /// <summary>
    /// Všem krysám ve hře změní stav na chargeAttack
    /// </summary>
    void MakeRatsCharge()
    {
        foreach(GameObject rat in SessionController.instance.ratsInScene)
        {
            rat.GetComponent<CommonAI>().currentState = AIStates.chargeAttack;
        }
    }

    /// <summary>
    /// Vynutí všechny veverky, aby se pohnuly vpřed a uvolnili Sputnikovi místo.
    /// </summary>
    void MakeSquirrelsMove()
    {
        foreach(GameObject squirrel in SessionController.instance.squirrelsInScene)
        {
            squirrel.GetComponent<SquirrelAI>().SputnikAppears();
        }
    }
    
}
