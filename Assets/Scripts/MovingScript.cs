using UnityEngine;
using System.Collections;

/// <summary>
/// Skript pohybující objekty ve hře
/// </summary>

public class MovingScript : MonoBehaviour {
    
    //Proměnné
    public Vector2 speed = new Vector2(10, 10);     //rychlost
    public Vector2 direction = new Vector2(-1, 0);  //smer
    public float modifier = 0f;                     //modifikator gravitace
    public float countdown = 5f;                    //odpocet
    private Vector2 movement;                       //pohyb

    void Update() {
        countdown -= Time.deltaTime;             
        movement = new Vector2(speed.x * direction.x, speed.y * direction.y);   //samotny pohyb

        //pokud uplyne doba countdown, zacne se na objekt aplikovat gravitace s modifikatorem
        if (countdown <= 0.0f) {
            gameObject.GetComponent<Rigidbody2D>().gravityScale -= (float)modifier * 2;
        }            
        
    }

    void FixedUpdate() {
        GetComponent<Rigidbody2D>().velocity = movement; //Aplikace pohybu na objekt
    }

	
}
