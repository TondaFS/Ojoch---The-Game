using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelAI : MonoBehaviour {
    CommonAI commonAI;
    public float sputnikFOS = 0;

    void Start()
    {
        commonAI = GetComponent<CommonAI>();
        commonAI.startingState = AIStates.stopAndShoot;
        SessionController.instance.squirrelsInScene.Add(this.gameObject);

        MakeBirdsCharge();
        CheckSputnik();
        CheckBoss();        
    }

    /// <summary>
    /// Zkontroluje, zda nejsou ve scéně nějacé sputnici, pokud an, zavolá fci SputnikAppears()
    /// </summary>
    void CheckSputnik()
    {
        if (SessionController.instance.sputniksInScene.Count > 0)
        {
            SputnikAppears();
        }
    }
    /// <summary>
    /// Zkontroluje, zda již není ve hře Goldenstein, pokud ano, zavolá funkci GoldensteinAppears()
    /// </summary>
    void CheckBoss()
    {
        if (SessionController.instance.bossInScene != null &&
            (SessionController.instance.bossInScene.GetComponent<BossAI>().bossType == BossType.goldenstein))
        {
            GoldensteinAppears();
        }
    }

    /// <summary>
    /// Změní všem žlutým ptákům ve hře stav na Chare útoks
    /// </summary>
    void MakeBirdsCharge()
    {
        if(SessionController.instance.pigsInScene.Count < 1)
        {
            foreach (GameObject bird in SessionController.instance.birdsInScene)
            {
                bird.GetComponent<CommonAI>().SwitchToNextState(AIStates.chargeAttack);
            }
        }
        
    }

    /// <summary>
    /// Jak se objeví sputnik ve hře, posune se směrem doleva
    /// </summary>
    public void SputnikAppears()
    {
        commonAI.flyOnScreenPosX = sputnikFOS;
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
