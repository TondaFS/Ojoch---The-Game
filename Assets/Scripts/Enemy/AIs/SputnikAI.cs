﻿using System.Collections;
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
        CheckRakosnik();
        CheckPigs();
    }

    /// <summary>
    /// Zkontroluje, zda nejsou ve scéně už nějaká prasata. Pokud ano, zavolá fci PigAppears()
    /// </summary>
    void CheckPigs()
    {
        if(SessionController.instance.pigsInScene.Count > 0)
        {
            PigAppears();
        }
    }
    /// <summary>
    /// Zkontroluje, jestli už není ve scéně Rákosník, pokud ano, zavolám funkci RakosnikAppears()
    /// </summary>
    void CheckRakosnik()
    {
        if(SessionController.instance.bossInScene != null && 
            SessionController.instance.bossInScene.GetComponent<BossAI>().bossType == BossType.rakosnik)
        {
            RakosnikAppears();
        }
    }

    /// <summary>
    /// Jak se objeví Rákosník ve hře, zvýší rychlost střelby
    /// </summary>
    public void RakosnikAppears()
    {
        GetComponent<ShooterAI>().ChangeMissileCooldown(-0.50f);
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
    
    /// <summary>
    /// Jak se objeví ve hře prase, sputnik přestane střílet a spustí si svou kamikazi již se žahentutím
    /// a běžícím odpočtem - pro každého sputnika se může provést jen jednou.
    /// Pokud už je prase ve hře než se Sputnik objeví, nejdříve doletí na obrazovku a pak se přepne na kamikazi.
    /// </summary>
    public void PigAppears()
    {
        if(commonAI.currentState == AIStates.flyOnScreen)
        {
            commonAI.startingState = AIStates.kamikaze;
        }
        else if(commonAI.currentState != AIStates.kamikaze)
        {
            commonAI.currentState = AIStates.kamikaze;            
        }

        GetComponent<KamikazeScript>().ignited = true;
        commonAI.movementSpeed += 1;
    }
}