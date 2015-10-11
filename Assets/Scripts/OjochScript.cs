﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Ovládání a chování Ojocha = Hráče
/// </summary>

public class OjochScript : MonoBehaviour {

    /// <summary>
    /// Proměnné
    /// </summary>
    
    public Vector2 speed = new Vector2(10,10);   // Rychlost Ojocha
    private Vector2 movement;                   // Ulozeni pohybu

    void Update () {
        //Axis information
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        // Pohyb 
        movement = new Vector2(speed.x * inputX, speed.y * inputY);

        ///<summary>
        /// Strelba
        ///</summary>

        bool shoot = Input.GetKeyDown(KeyCode.Space);       //Stisknutí mezerníku
        shoot |= Input.GetButtonDown("Fire2");              //Alternativní střelba - defaultní v Unity
        
        //Pokud chce hrac vystrelit, pouzije se skript weapon, který zavolá svou fci Attack
        if (shoot) {
            WeaponScript weapon = GetComponent<WeaponScript>();
            if (weapon != null) {
                weapon.Attack(false);               //atribut false -> jedna se o nepritele, kdo strili?
            }

        }
    }

    void FixedUpdate() {
       GetComponent<Rigidbody2D>().velocity = movement; //Aplikace pohybu na objekt
    }

    void OnCollisionEnter2D(Collision2D collision) {
        bool damagePlayer = false;

        EnemyScript enemy = 
    }
}
