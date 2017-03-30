using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigAI : MonoBehaviour {
    CommonAI commonAI;

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
    /// Jak dlouho trvá nabití laserů
    /// </summary>
    public float laserCharging;

	void Start () {
        commonAI = GetComponent<CommonAI>();
        SessionController.instance.pigsInScene.Add(this.gameObject);
        CheckZebirko();
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
    /// Pokud se objeví kapitán Žebírko, zvýším rychlost střelby laserů
    /// </summary>
    public void ZebirkoAppears()
    {
        laserCooldown -= 0.5f;
    }
	
    public void ShootLaser()
    {
        //TO DO
    }

    public void ChargeLaser()
    {

    }



}
