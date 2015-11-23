﻿using UnityEngine;
using System.Collections;

public class StatueAttackScript : MonoBehaviour {

    public bool heartAttack = false;
    private WeaponScript weapon;

    void Start() {
        weapon = GetComponent<WeaponScript>();
    }    

    // Update is called once per frame
    void Update () {
        if (heartAttack) {
            weapon.StatueAttack(true, new Vector2(1, 0));           
            weapon.StatueAttack(true, new Vector2(1, 0.3f));
            weapon.StatueAttack(true, new Vector2(1, -0.7f));
            weapon.StatueAttack(true, new Vector2(1, -0.3f));
            weapon.StatueAttack(true, new Vector2(1, -0.9f));
            heartAttack = false;            
        }
	}
}
