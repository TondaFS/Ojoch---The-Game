using UnityEngine;
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
    public Rigidbody2D ojoch;

    void Start() {
        ojoch = GetComponent<Rigidbody2D>();
    }

    void Update () {
        //Axis information
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        // Pohyb 
        movement = new Vector2(speed.x * inputX, speed.y * inputY);

        
        bool rotateLeft = Input.GetKey(KeyCode.E);
        bool rotateRight = Input.GetKey(KeyCode.Q);
        if (rotateLeft) {
           ojoch.MoveRotation(ojoch.rotation - 1);
        }

        if (rotateRight) {
            ojoch.MoveRotation(ojoch.rotation + 1);
        }
        


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
        if (collision.gameObject.tag == "Enemy") {
            Destroy(collision.gameObject);
            HealthScript playerHealth = this.GetComponent<HealthScript>();
            if (playerHealth != null) {
                playerHealth.Damage(5);
            }

        }
    }
    
}
