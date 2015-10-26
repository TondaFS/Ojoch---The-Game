using UnityEngine;
using UnityEngine.UI;
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
    public PowerUpScript powerCombo;

    //promenne na zivoty/palivo Ojocha
    public Slider healthSlider;                 //Ukazatel zdravi   
    public HealthScript playerHealth;

    //promenne na score
    public Text scoreText;
    public int modifikatorScore = 1;                //Modfifikator
    public int tmpscore;                            //hracovo skore      


    void Start() {
        ojoch = GetComponent<Rigidbody2D>();
        powerCombo = GetComponent<PowerUpScript>();
        playerHealth = GetComponent<HealthScript>();
        tmpscore = 0;
    }

    void Update () {
        //Axis information
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        //Skore
        this.scoreText.text = "Skore: " + tmpscore;        

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

        //Ocekuje, jestli je natoceni  na 0, pokud ne, zacne aplikovat rotaci v danem smeru
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
                CollisionDisable(true);
            } 
        }

        ///<summary>
        /// Strelba
        ///</summary>

        bool shoot = Input.GetKeyDown(KeyCode.Space);       //Stisknutí mezerníku
        shoot |= Input.GetButtonDown("Fire2");              //Alternativní střelba - defaultní v Unity
        
        //Pokud chce hrac vystrelit, pouzije se skript weapon, který zavolá svou fci Attack a ubere mu to 1 život
        if (shoot) {
            WeaponScript weapon = GetComponent<WeaponScript>();
            if (weapon != null) {
                weapon.Attack(false);                       //atribut false -> jedna se o nepritele, kdo strili? 
                playerHealth.Damage(1);
                healthSlider.value = playerHealth.hp;
            }

        }
    }

    void FixedUpdate() {
       GetComponent<Rigidbody2D>().velocity = movement; //Aplikace pohybu na objekt
    }

    //Kolize 
    void OnCollisionEnter2D(Collision2D collision) {

        //S nepritelem -> ubere 5 zivotu a nepritele znici
        if (collision.gameObject.tag == "Enemy") {
            Destroy(collision.gameObject);
            if (playerHealth != null) {
                playerHealth.Damage(5);
                healthSlider.value = playerHealth.hp;
            }

            //orotuje ojocha
            if (transform.rotation.z < 0)
                transform.Rotate(0, 0, Random.Range(-25, -15));
            else
                transform.Rotate(0, 0, Random.Range(15, 25));

        }

        //S prekazkou -> ubere 10 zivotu a ucini na 5 vterin Ojocha nesmrtelnym
        if (collision.gameObject.tag == "Obstacle")
        {
            if (playerHealth != null)
            {
                playerHealth.Damage(10);
                healthSlider.value = playerHealth.hp;
            }

            //orotuje ojocha
            if (transform.rotation.z < 0)
                transform.Rotate(0, 0, Random.Range(-40, -30));
            else
                transform.Rotate(0, 0, Random.Range(30, 40));
            
            //nesmrtelnost
            CollisionDisable(false);
            godMode = 5;                                  
        }

        //S powerUpem -> Zvysi pocet powerupu, provede efekt powerUpu, a pokud je sbran jiz druhy power up
        //provede se kombo
        if (collision.gameObject.tag == "PowerUp") {
            tmpscore += 5 * modifikatorScore;                                                       //Zapocitani skore 
            powerCombo.powerUps += 1;                                                               //zvyseni powerUpu
            powerCombo.powerUpCombo += collision.gameObject.GetComponent<PowerUpID>().powerUpID;    //pridani ID
            powerCombo.PowerEffect(collision.gameObject.GetComponent<PowerUpID>().powerUpID);       //efekt powerUpu

            //pokud je sebran jiz druhy powerUp -> provede se kombo
            if (powerCombo.powerUps == 2) {
                powerCombo.PowerCombo(powerCombo.powerUpCombo);                                     //provedeni komba
                powerCombo.powerUps = 0;                                                            //nastaveni poctu powerupu na nula
                powerCombo.powerUpCombo = 0;                                                        //vymazani komba a priprava na dalsi
            }
            Destroy(collision.gameObject);
        }
    }

    //Metoda pro zapnuti/vynuti BoxCollideru - nesmrtelnost
    void CollisionDisable(bool enableGod) {
        this.GetComponent<BoxCollider2D>().enabled = enableGod;
    }      
}