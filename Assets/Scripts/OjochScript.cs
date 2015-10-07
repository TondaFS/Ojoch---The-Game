using UnityEngine;
using System.Collections;

/// <summary>
/// Ovládání a chování Ojocha = Hráče
/// </summary>

public class OjochScript : MonoBehaviour {

    /// <summary>
    /// Proměnné
    /// </summary>

    private float rychlost = 10f;
    public Vector2 speed = new Vector2(10,10);   // Rychlost Ojocha
    private Vector2 movement;                   // Ulozeni pohybu

    void Update () {
        //Axis information
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        // Pohyb 
        movement = new Vector2(speed.x * inputX, speed.y * inputY);
	}


    void FixedUpdate() {
       GetComponent<Rigidbody2D>().velocity = movement; //Aplikace pohybu na objekt
    }

}
