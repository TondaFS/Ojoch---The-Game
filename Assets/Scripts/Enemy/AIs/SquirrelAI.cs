using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelAI : ShooterAI {
    /// <summary>
    /// Posunutí doleva, když se objeví sputnik
    /// </summary>
    public float sputnikFOS = 0;

    public override void Start()
    {
        base.Start();

        enemyType = EnemyType.squirrel;
        startingState = AIStates.stopAndShoot;
        SessionController.instance.squirrelsInScene.Add(this.gameObject);

        MakeBirdsCharge();
        CheckSputnik();
        CheckBoss();        
    }

    public override void AK47()
    {
        ChangeMissileCooldown(cooldownChange);
    }

    /// <summary>
    /// Veverka zvysi svou rychlost strelby
    /// </summary>
    public override void HalfHealth()
    {
        if (!halfDamageEffectDone)
        {
            Debug.Log("Veverka ma polovinu zivota... strili rychleji");
            ChangeMissileCooldown(cooldownChange);
            halfDamageEffectDone = true;
        }
    }

    /// <summary>
    /// Zničí tuto veverku
    /// </summary>
    public override void DestroyThis()
    {
        SessionController.instance.squirrelsInScene.Remove(this.gameObject);
        base.DestroyThis();
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
        Debug.Log("Sputnik is here");
        Debug.Log(flyOnScreenPos);
        flyOnScreenPos.x = sputnikFOS;
        flyOnScreenPosX = sputnikFOS;
        Debug.Log(flyOnScreenPos);
        SwitchToNextState(AIStates.flyOnScreen);
    }

    /// <summary>
    /// Jak se objeví ve hře Goldenstein, 
    /// </summary>
    public void GoldensteinAppears()
    {
        SwitchToNextState(AIStates.chaseAndShoot);
    }
}
