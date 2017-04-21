using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterAI : MonoBehaviour {
    [Header("Stop And Shoot", order = 1)]
    public Transform missile;
    public int ammo;
    private Vector3 missileLauncherPos;
    public float missileCooldown = 1;
    private float currentMissileCooldown;

    private CommonAI commonAIScript;

    public AIStates noMissileState;    
    void Start()
    {
        commonAIScript = GetComponent<CommonAI>();
    }

    void Update()
    {
        switch (commonAIScript.currentState)
        {
            case AIStates.stopAndShoot:
                Shoot();
                break;
            case AIStates.chaseAndShoot:
                Shoot();
                commonAIScript.Chase();
                break;
        }
        
    }

    /// <summary>
    /// Změní rychlost střelby o danou hodnoty
    /// </summary>
    /// <param name="change">Velikost změny</param>
    public void ChangeMissileCooldown(float change)
    {
        missileCooldown += change;
    }
    /// <summary>
    /// Starts shooting missiles
    /// </summary>
    public void Shoot()
    {
        if (currentMissileCooldown > 0)
        {
            currentMissileCooldown -= Time.deltaTime;
        }
        else
        {
            currentMissileCooldown = missileCooldown;
            gameObject.GetComponent<Animator>().SetTrigger("sAttack");
            ammo--;
        }

        if (ammo == 0)
        {
            GetComponent<CommonAI>().SwitchToNextState(noMissileState);

        }
    }
    /// <summary>
    /// Vystřelí novou střelu
    /// </summary>
    public void Launch()
    {
        missileLauncherPos = this.transform.GetChild(0).transform.position;
        Instantiate(missile, missileLauncherPos, Quaternion.identity);
    }
        
}
