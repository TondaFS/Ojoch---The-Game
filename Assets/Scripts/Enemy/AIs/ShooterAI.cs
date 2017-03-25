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


}
