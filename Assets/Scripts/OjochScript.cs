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
    public Collider2D obstacle;

    void Start() {
        ojoch = GetComponent<Rigidbody2D>();
    }

    void Update () {
        //Axis information
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        god -= Time.deltaTime;

        // Pohyb 
        movement = new Vector2(speed.x * inputX, speed.y * inputY);

        bool rotateLeft = Input.GetKey(KeyCode.E);
        bool rotateRight = Input.GetKey(KeyCode.Q);
        if (rotateLeft) {
            transform.Rotate(0, 0, -1);
        }

        if (rotateRight) {
            transform.Rotate(0, 0, 1);
        }
        

        ///<summary>
        /// Strelba
        ///</summary>

        bool shoot = Input.GetKey(KeyCode.Space);           //Stisknutí mezerníku
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
        ojoch.AddForce(ojoch.velocity * -1);
    }

    //Kolize 
    void OnCollisionEnter2D(Collision2D collision) {
        //S nepritelem -> ubere 5 zivotu a nepritele znici
        if (collision.gameObject.tag == "Enemy") {
            Destroy(collision.gameObject);
            HealthScript playerHealth = this.GetComponent<HealthScript>();
            if (playerHealth != null) {
                playerHealth.Damage(5);
            }

        }

        //S prekazkou -> ubere 10 zivotu a ucini na 5 vterin Ojocha nesmrtelnym
        if (collision.gameObject.tag == "Obstacle")
        {
            HealthScript playerHealth = this.GetComponent<HealthScript>();
            if (playerHealth != null)
            {
                playerHealth.Damage(10);
            }

            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                        

                       
        }
    }

      
}
