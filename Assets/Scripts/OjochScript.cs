using UnityEngine;
using System.Collections;

/// <summary>
/// Ovládání a chování Ojocha = Hráče
/// </summary>

public class OjochScript : MonoBehaviour {

    /// <summary>
    /// Proměnné
    /// </summary>
    
    public Vector2 speed = new Vector2(10,10);  // Rychlost Ojocha
    private Vector2 movement;                   // Ulozeni pohybu
    public Rigidbody2D ojoch;
    public Collider2D obstacle;
    public float godMode = 0;                   //nesmrtelnost
    public float rotace = 10;                   //rychlost nezavisle rotace
    public float modifikator = 1;               //Modfifikator

    void Start() {
        ojoch = GetComponent<Rigidbody2D>();
    }

    void Update () {
        //Axis information
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        

        // Pohyb 
        movement = new Vector2(speed.x * inputX, speed.y * inputY);

        //Balancovani
        bool rotateLeft = Input.GetKey(KeyCode.E);
        bool rotateRight = Input.GetKey(KeyCode.Q);
        if (rotateLeft) {
            transform.Rotate(0, 0, -1);
        }
        if (rotateRight) {
            transform.Rotate(0, 0, 1);
        }

        //Ocekuje, jestli je natoceni  na 0, pokud ne, yacne aplikovat rotaci v danem smeru
        if (transform.rotation.z <= 0)
        {
            transform.Rotate(0, 0, -10 * Time.deltaTime);
        }
        else if (transform.rotation.z > 0) {
            transform.Rotate(0, 0, 10 * Time.deltaTime);
        }

        //Kontrola, zda neni Ojoch zrovna nesmrtelny, pokud je, odecte cas z godMode a pokud dojde na nulu, okamzite mu opet zapne BoxCollider
        if (godMode != 0) {
            godMode -= Time.deltaTime ;
            if (godMode == 0 || godMode < 0) {
                godMode = 0;
                CollisionDisable(false);
            } 
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

            if (transform.rotation.z < 0)
                transform.Rotate(0, 0, Random.Range(-25, -15));
            else
                transform.Rotate(0, 0, Random.Range(15, 25));

        }

        //S prekazkou -> ubere 10 zivotu a ucini na 5 vterin Ojocha nesmrtelnym
        if (collision.gameObject.tag == "Obstacle")
        {
            HealthScript playerHealth = this.GetComponent<HealthScript>();
            if (playerHealth != null)
            {
                playerHealth.Damage(10);
            }

            if (transform.rotation.z < 0)
                transform.Rotate(0, 0, Random.Range(-40, -30));
            else
                transform.Rotate(0, 0, Random.Range(30, 40));
            
            CollisionDisable(true);
            godMode = 5;                                  
        }
    }

    //Metoda pro zapnuti/vynuti BoxCollideru
    void CollisionDisable(bool enableGod) {
        this.GetComponent<BoxCollider2D>().isTrigger = enableGod;
    }      
}