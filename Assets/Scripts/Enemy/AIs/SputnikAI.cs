using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SputnikAI : ShooterAI {
    KamikazeScript kamikazeScript;

    public override void Start()
    {
        base.Start();

        SessionController.instance.sputniksInScene.Add(this.gameObject);
        kamikazeScript = GetComponent<KamikazeScript>();
        MakeRatsCharge();
        MakeSquirrelsMove();
        CheckRakosnik();
        CheckPigs();
    }

    /*
    public override void EnemyDeathSound()
    {
        GameManager.instance.GetComponent<SoundManager>().PlaySound(GameManager.instance.GetComponent<SoundManager>().pokoutnikDeath);
    }
    */

    public override void DestroyThis()
    {
        SessionController.instance.sputniksInScene.Remove(this.gameObject);
        base.DestroyThis();
    }

    public void Test()
    {
        Debug.Log("Test");
    }

    public override void EnemyDamage(int damage)
    {
        Debug.Log("Damage");
        if (GetComponent<KamikazeScript>().exploded)
            score = 0;

        base.EnemyDamage(damage); 
        
        //hodit do common? nebudou to mít všichni nepřátele?
        if (hp <= damagedHP)
            GetComponent<Animator>().SetTrigger("isDamaged");
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
        ChangeMissileCooldown(-0.50f);
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
        if(currentState.Equals(AIStates.flyOnScreen))
        {
            startingState = AIStates.kamikaze;
        }
        else if(currentState.Equals(AIStates.kamikaze))
        {
            currentState = AIStates.kamikaze;            
        }

        kamikazeScript.ignited = true;
        movementSpeed += 1;
    }
}
