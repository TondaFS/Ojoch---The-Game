using UnityEngine;
using System.Collections;

/// <summary>
/// Skript pohybující objekty ve hře
/// </summary>

public class MovingScript : MonoBehaviour {
    
    //Proměnné
    public Vector2 direction = new Vector2(0, 1);     //smer    
    private Vector2 movement;                         //pohyb
    public int speed = 1;

    void Update() {                 
        movement = new Vector2(speed * direction.x * Time.deltaTime, speed * direction.y * Time.deltaTime);   //samotny pohyb
        transform.Translate(movement, 0);       
    }  
}

/*
Pozustatkove kody
******************************
    //public float modifier = 0f;                     //modifikator gravitace
    //public float countdown = 5f;                    //odpocet
    //countdown -= Time.deltaTime;    

 void FixedUpdate() {
        //GetComponent<Rigidbody2D>().velocity = movement; //Aplikace pohybu na objekt
    }
    
    //pokud uplyne doba countdown, zacne se na objekt aplikovat gravitace s modifikatorem
        if (countdown <= 0.0f) {
            gameObject.GetComponent<Rigidbody2D>().gravityScale -= (float)modifier * 2;
        }  	
*/
