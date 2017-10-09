﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigAI : CommonAI {
    /// <summary>
    /// GameObject laseru prasete
    /// </summary>
    public GameObject laser;
    /// <summary>
    /// Jak dlouho bude laser po vystřelení aktivní
    /// </summary>
    public float laserDuration;
    /// <summary>
    /// Jak rychle prase střílí
    /// </summary>
    public float laserCooldown;
    /// <summary>
    /// Aktuální doba cooldownu pro střelbu
    /// </summary>
    public float currentLaserCooldown;
    /// <summary>
    /// Jak dlouho trvá nabití laserů
    /// </summary>
    public float batteryCharging;
    /// <summary>
    /// Aktuální oba nabíjení baterie
    /// </summary>
    public float currentBatteryCharging;
    /// <summary>
    /// Kolik střel, než bude prase potřebovat nabíjet baterii
    /// </summary>
    public float laserAmmo;
    /// <summary>
    /// Aktuální hodnota munice
    /// </summary>
    public float currentLaserAmmo;
    /// <summary>
    /// Chrání tohle prase nějaký pták?
    /// </summary>
    public bool isProtected = false;

	public override void Start () {
        base.Start();
        
        SessionController.instance.pigsInScene.Add(this.gameObject);
        CheckZebirko();
        MakeSpuntiksKamikaze();
        MakeBirdsProtect();

        currentLaserAmmo = laserAmmo;
	}

    public override void Update()
    {
        base.Update();

        if(currentState == AIStates.shootLaser)
        {
            ShootLaser();
        } else if(currentState == AIStates.laserActive)
        {
            Debug.Log("Laser s active!");
        } else if(currentState == AIStates.laserCharging)
        {
            Debug.Log("Charging!");
        }
    }

    /*
    public override void EnemyDeathSound()
    {
        GameManager.instance.GetComponent<SoundManager>().PlaySound(GameManager.instance.GetComponent<SoundManager>().pigDeath);
    }
    */

    public override void DestroyThis()
    {        
        SessionController.instance.pigsInScene.Remove(this.gameObject);
        base.DestroyThis();
    }

    /// <summary>
    /// Vypne laser, když prase zemře
    /// </summary>
    public void DisableEverything()
    {
        laser.SetActive(false);
    }

    /// <summary>
    /// Jak se ve hře objeví prase, donutí všechny sputniky, aby přešli na kamikazi
    /// </summary>
    void MakeSpuntiksKamikaze()
    {
        foreach(GameObject sputnik in SessionController.instance.sputniksInScene)
        {
            sputnik.GetComponent<SputnikAI>().PigAppears();
        }
    }

    void MakeBirdsProtect()
    {
        foreach(GameObject bird in SessionController.instance.birdsInScene)
        {
            bird.GetComponent<BirdAI>().PigAppear();
        }
    }
    
    /// <summary>
    /// Zkontroluje, zda již není ve hře Kampitán Žebírko, pokud ano, zavolá fci ZebirkoAppears()
    /// </summary>
    void CheckZebirko()
    {
        if(SessionController.instance.bossInScene != null &&
            SessionController.instance.bossInScene.GetComponent<BossAI>().bossType == BossType.zebirko)
        {
            ZebirkoAppears();
        }
    }

    /// <summary>
    /// Pokud se objeví kapitán Žebírko, zvýším maximální počet munice baterie
    /// </summary>
    public void ZebirkoAppears()
    {
        laserAmmo = laserAmmo + 2;
    }

    /// <summary>
    /// Čekám dobu naíjení baterie a pak se přepnu do stavu, kdy bude prase střílet.
    /// Zakážu, aby se střílelo hned a obnovím počet munice na max.
    /// </summary>
    /// <returns></returns>
    IEnumerator Charging()
    {
        yield return new WaitForSeconds(batteryCharging);
        SwitchToNextState(AIStates.shootLaser);
        GetComponent<Animator>().SetBool("isCharging", false);
        GetComponent<Animator>().SetBool("sAttack", false);
        currentLaserAmmo = laserAmmo;
    }
	
    /// <summary>
    /// Čekám po dobu délky střelby laseru. Pokud došla munice přepnu se do stavu, kde si začnu nabíjet baterie.
    /// Jinak se přepnu do normálního stavu.
    /// </summary>
    /// <returns></returns>
    IEnumerator LaserShoot()
    {
        yield return new WaitForSeconds(laserDuration);
        if(currentLaserAmmo <= 0)
        {
            SwitchToNextState(AIStates.laserCharging);
            GetComponent<Animator>().SetBool("isCharging", true);            
            laser.SetActive(false);
            Debug.Log("No ammo!");
            StartCoroutine(Charging());

        }
        else
        {
            SwitchToNextState(AIStates.shootLaser);
            GetComponent<Animator>().SetBool("sAttack", false);
            
            laser.SetActive(false);
        }
        
    }

    /// <summary>
    /// Prase je ve stavu, kdy střílí lasery, pokud není zrovna cooldown, vystřelí a přejde do dalšího stavu
    /// </summary>
    public void ShootLaser()
    {
        if(currentLaserCooldown > 0)
        {
            currentLaserCooldown -= Time.deltaTime;
        }
        else
        {
            currentLaserCooldown = laserCooldown;
            GetComponent<Animator>().SetBool("sAttack", true);
            laser.SetActive(true);
            currentLaserAmmo--;
            SwitchToNextState(AIStates.laserActive);
            StartCoroutine(LaserShoot());
        }
    }
}
