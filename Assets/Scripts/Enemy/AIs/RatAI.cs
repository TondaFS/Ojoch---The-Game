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
        
        CheckSputnik();
        CheckBoss();
    }   
    
    /// <summary>
    /// Zkontroluje, zda již ve scéně nejsou nějací sputnici, pokud ano, donutí krysy udělat ChargeAttack
    /// </summary>
    void CheckSputnik()
    {
        if (SessionController.instance.sputniksInScene.Count > 0)
        {
            GetComponent<CommonAI>().startingState = AIStates.chargeAttack;
        }
    } 
    /// <summary>
    /// Zkontroluje, zda již ve scéně není Dmitrij Ivanovič Filipovič Myšov III, pokud ano, zavolá fci AlexanderAppears()
    /// </summary>
    void CheckBoss()
    {
        if (SessionController.instance.bossInScene != null &&
            (SessionController.instance.bossInScene.GetComponent<BossAI>().bossType == BossType.filipovic))
        {
            FilipovicAppears();
        }
    }
    /// <summary>
    /// Jak se ve hře objeví Alexander Mišov, zvýší kryse rychlost
    /// </summary>
    public void FilipovicAppears()
    {
        GetComponent<CommonAI>().ChangeMovementSpeed(1);
    }
}
