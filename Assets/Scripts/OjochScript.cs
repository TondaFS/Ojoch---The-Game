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
    private WeaponScript[] weapons;
    public float vterina = 0;                   //vterina
    public float timeSlow = 0;                  //jak dlouho bude zpomaleny cas

    public bool contraBubles = false;           //Rozptyl bublinek
    public int contraNumber = 10;              //Pocet Kontra Strel
    public bool cleanSock = false;              //je powerUp Ciste ponozky aktivni?

    //Inverze
    public bool isInverted = false;             //Inverzni ovladani?
    public float invertTime = 0;

    //promenne na zivoty/palivo Ojocha
    public Slider healthSlider;                 //Ukazatel zdravi   
    public HealthScript playerHealth;

    //promenne na score
    public Text scoreText;
    public int modifikatorScore = 1;                //Modfifikator
    public int tmpscore;                            //hracovo skore 

    
    //promenne na panelText
    public float odpocet = 0;                       //jak dlouho tam bude text
    public Text panelText;                          //text
    

    void Start() {
        ojoch = GetComponent<Rigidbody2D>();
        powerCombo = GetComponent<PowerUpScript>();
        playerHealth = GetComponent<HealthScript>();
        tmpscore = 0;
        weapons = GetComponentsInChildren<WeaponScript>();
    }

    void Update () {
        //Axis information
        float inputX = Input.GetAxis("Horizontal") * (isInverted ? -1 : 1) ;
        float inputY = Input.GetAxis("Vertical") * (isInverted ? -1 : 1) ;

        //Skore
        this.scoreText.text = "Skore: " + tmpscore;

        // Pohyb 
        
        movement = new Vector2(speed.x * inputX * Time.deltaTime, speed.y * inputY * Time.deltaTime);
        transform.Translate(movement, 0);

        //Balancovani
        float rotation = Input.GetAxis("Rotation");                 //* (isInverted ? -1 : 1)
        if (rotation < 0 && transform.rotation.z >= -0.8) {
            transform.Rotate(0, 0, rotation * 1.5f);
        }
        if (rotation >= 0 && transform.rotation.z <= 0.8) {
            transform.Rotate(0, 0, rotation * 1.5f);
        }

        //Kontrola zpomaleni casu
        if(timeSlow > 0)
        {
            timeSlow -= Time.deltaTime;
            if(timeSlow <= 0)
            {
                SlowTime(false);
            }
        }
        
        //Odpocet zobrazeni textu
        if (odpocet != 0)
        {
            odpocet -= Time.deltaTime;
            if (odpocet == 0 || odpocet < 0)
            {
                this.panelText.text = "";
                odpocet = 0;
            }
        }

        //Kontrola inverzniho ovladani
        if (invertTime != 0)
        {
            invertTime -= Time.deltaTime;
            if(invertTime == 0 || invertTime < 0)
            {
                this.InversionControlling();
                invertTime = 0;
            }
        }


        //Ocekuje, jestli je natoceni  na 0, pokud ne, zacne aplikovat rotaci v danem smeru
        if (transform.rotation.z <= 0 && transform.rotation.z >= -0.8)
        {
            transform.Rotate(0, 0, -rotace * Time.deltaTime + transform.rotation.z*0.8f);
        }
        else if (transform.rotation.z > 0 && transform.rotation.z <= 0.8) {
            transform.Rotate(0, 0, rotace * Time.deltaTime + transform.rotation.z*0.8f);
        }

        //Ocekuje jaka je rotace a pripadne odebere palivo
        if (transform.rotation.z <= -0.5 || transform.rotation.z >= 0.5)
        {
            if (vterina > 0)
            {
                vterina -= Time.deltaTime;
            }
            else
            {
                playerHealth.Damage(2);
                healthSlider.value = playerHealth.hp;
                vterina = 1;
            }
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

        bool shoot = Input.GetButton("Fire1");          //Stisknutí mezerníku
        
        //Pokud chce hrac vystrelit, pouzije se skript weapon, který zavolá svou fci Attack a ubere mu to 1 život
        if (shoot) {            
            if (weapons != null && weapons[0].CanAttack) {
                weapons[0].Attack(false);                       //atribut false -> jedna se o nepritele, kdo strili? 
                playerHealth.Damage(1);
                healthSlider.value = playerHealth.hp;
                
                if (contraBubles)
                {
                    weapons[1].Attack(false);
                    weapons[2].Attack(false);

                    contraNumber -= 1;
                    if (contraNumber == 0)
                    {
                        contraBubles = false;
                    }
                    
                }
            }
        }
    }
    
    //Kolize 
    void OnCollisionEnter2D(Collision2D collision) {

        //S nepritelem -> ubere 5 zivotu a nepritele znici
        if (collision.gameObject.tag == "Enemy" && !cleanSock) {
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
        else
        {
            Destroy(collision.gameObject);
            cleanSock = false;
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
            //powerCombo.PowerEffect(collision.gameObject.GetComponent<PowerUpID>().powerUpID);       //efekt powerUpu

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

    //Inverze ovladani
    public void InversionControlling() {
        this.isInverted = !(isInverted);
    }    

    //Zpomali/vrati cas
    public void SlowTime(bool slow)
    {
        if (slow)
        {
            Time.timeScale = 0.5f;
            //Time.fixedDeltaTime = 0.5f;
            timeSlow = 3;
        }
        else
        {
            Time.timeScale = 1;
            //Time.fixedDeltaTime = 1;
        }
    }
}





/***
***     Pozustatkove kody!

   
    void FixedUpdate() {
       GetComponent<Rigidbody2D>().velocity = movement; //Aplikace pohybu na objekt
    }

*/

