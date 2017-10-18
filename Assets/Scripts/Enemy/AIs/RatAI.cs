using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Skript unikátního chévání krysy: kamikaze
/// </summary>
public class RatAI : CommonAI { 
    
    /// <summary>
    /// Využití základní Start funkce z commonAi, změna počátečního stavu
    /// na kamikazi a přidání kontroly Sputniků a bosse krysáka
    /// </summary>
    public override void Start()
    {
        base.Start();

        enemyType = EnemyType.rat;
        startingState = AIStates.kamikaze;
        SessionController.instance.ratsInScene.Add(this.gameObject);
        
        CheckSputnik();
        CheckBoss();
    }

    /// <summary>
    /// Zvysi pohyb krysy po dosazeni poloviny zivotu
    /// </summary>
    public override void HalfHealth()
    {
        if (!halfDamageEffectDone)
        {
            ChangeMovementSpeed(movementChange);
            halfDamageEffectDone = true;
        }
    }

    /// <summary>
    /// Zrychli pohyb krysy po sebrani AK47
    /// </summary>
    public override void AK47()
    {
        ChangeMovementSpeed(movementChange);
    }

    /// <summary>
    /// Znici objekt a smaze zaznam o kryse v listu krys
    /// </summary>
    public override void DestroyThis()
    {
        SessionController.instance.ratsInScene.Remove(this.gameObject);
        base.DestroyThis();
    }

    /// <summary>
    /// Kontroluje, zda krysa nezemrela pres vybuch, pokud ano, hrac nedostane zadne skore
    /// </summary>
    /// <param name="damage"></param>
    public override void EnemyDamage(int damage)
    {
        if (GetComponent<KamikazeScript>().exploded)
            score = 0;

        base.EnemyDamage(damage);

    }

    /// <summary>
    /// Zkontroluje, zda již ve scéně nejsou nějací sputnici, pokud ano, donutí krysy udělat ChargeAttack
    /// </summary>
    void CheckSputnik()
    {
        if (SessionController.instance.sputniksInScene.Count > 0)
        {
            startingState = AIStates.chargeAttack;
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
        ChangeMovementSpeed(1);
    }
}
