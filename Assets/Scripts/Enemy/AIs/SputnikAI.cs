using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SputnikAI : ShooterAI {
    KamikazeScript kamikazeScript;   

    public override void Start()
    {
        base.Start();

        enemyType = EnemyType.sputnik;
        SessionController.instance.sputniksInScene.Add(this.gameObject);
        kamikazeScript = GetComponent<KamikazeScript>();
        MakeSquirrelsMove();
        CheckRakosnik();
        CheckPigs();
    }    
    public override void DestroyThis()
    {
        SessionController.instance.sputniksInScene.Remove(this.gameObject);
        base.DestroyThis();
    }
    
    /// <summary>
    /// Kontroluje, jestli Sputnik nezemrel pres exploz, pokud ano, nedostane hrac zadne skore
    /// </summary>
    /// <param name="damage"></param>
    public override void EnemyDamage(int damage)
    {
        if (GetComponent<KamikazeScript>().exploded)
            score = 0;

        base.EnemyDamage(damage); 
            
    }

    /// <summary>
    /// Prepnuti na poskozeneho sputnika a zrychleni rychlosti strelby
    /// </summary>
    public override void HalfHealth()
    {
        if (!halfDamageEffectDone)
        {
            Debug.Log("Sputnik ma polovinu zivota a strili rychleji");
            GetComponent<Animator>().SetTrigger("isDamaged");
            ChangeMissileCooldown(cooldownChange);
            halfDamageEffectDone = true;
        }        
    }

    /// <summary>
    /// Zrychli sputnikovu rychlost strelby
    /// </summary>
    public override void AK47()
    {
        ChangeMissileCooldown(cooldownChange);
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
        ChangeMissileCooldown(2*cooldownChange);
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
        if(currentState.Equals(AIStates.flyOnScreen))
        {
            startingState = AIStates.kamikaze;
        }
        else if(!currentState.Equals(AIStates.kamikaze))
        {
            SwitchToNextState(AIStates.kamikaze);
        }       

        kamikazeScript.ignited = true;
        ChangeMovementSpeed(movementChange);
    }
}
