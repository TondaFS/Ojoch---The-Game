﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelAI : MonoBehaviour {
    CommonAI commonAI;

    void Start()
    {
        commonAI = GetComponent<CommonAI>();
        commonAI.startingState = AIStates.stopAndShoot;
        SessionController.instance.squirrelsInScene.Add(this.gameObject);

        if (SessionController.instance.sputniksInScene.Count > 0)
        {
            SputnikAppears();
        }

        if(SessionController.instance.bossInScene != null &&
            (SessionController.instance.bossInScene.GetComponent<BossAI>().bossType == BossType.goldenstein))
        {
            commonAI.SwitchToNextState(AIStates.chaseAndShoot);
        }
    }

    /// <summary>
    /// Jak se objeví sputnik ve hře, posune se směrem doleva
    /// </summary>
    public void SputnikAppears()
    {
        commonAI.flyOnScreenPosX = 0.75f;
        commonAI.SwitchToNextState(AIStates.flyOnScreen);
    }

    /// <summary>
    /// Jak se objeví ve hře Goldenstein, 
    /// </summary>
    public void GoldensteinAppears()
    {
        commonAI.SwitchToNextState(AIStates.chaseAndShoot);
    }
}
