using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OjochScript : MonoBehaviour {

    /// <summary>
    /// Proměnné
    /// </summary>
    
    public Vector2 speed = new Vector2(10,10);      // Rychlost Ojocha
    private Vector2 movement;                       // Ulozeni pohybu
    public Rigidbody2D ojoch;
    public Collider2D obstacle;
    public PowerUpScript powerCombo;
    private WeaponScript[] weapons;
    public Animator animator;
    public CollectingScript collect;
    public GameObject socha;
    public HealthScript playerHealth;
    public SoundManager managerSound;
    public ScoreScript session;   
    
    //Pro ultrakejch 
    public bool kejch = false;
    public Vector2 ultraKejch = new Vector2(0,0);

    //promenne na zivoty a sanity Ojocha
    public GameObject sanityBar;   

    //Kontra strelba
    public bool contraBubles = false;           //Rozptyl bublinek   

    //AK-47
    public bool isAkacko = false;               //ke Ak-47 aktivni?

    //Inverze
    public bool isInverted = false;             //Inverzni ovladani?
    public float invertTime = 0;

    //nesmrtelnost
    public float godMode = 0;                   //nesmrtelnost 

    //Push od sochy
    public float push = 0;
          

    void Start() {
        session = GameObject.Find("Session Controller").GetComponent<ScoreScript>();
        ojoch = GetComponent<Rigidbody2D>();
        powerCombo = GetComponent<PowerUpScript>();
        playerHealth = GetComponent<HealthScript>();        
        animator = transform.Find("sprite").gameObject.GetComponent<Animator>();
        weapons = GetComponentsInChildren<WeaponScript>();
        collect = GetComponent<CollectingScript>();
        managerSound = GameManager.instance.GetComponent<SoundManager>();
        socha = GameObject.Find("statue");
        GetComponent<AudioSource>().volume = 0.3f * managerSound.soundsVolume;
        sanityBar = GameObject.Find("Brain");
        sanityBar.SetActive(false);        
}

    void Update () {

        //Odražení od Sochy
        if (push > 0)
        {
            transform.Translate(0.25f, 0, 0);
            push -= Time.deltaTime;
        }        

        //Ultrakejch
        if (kejch)
        {
            ultraKejch = new Vector2(Random.Range(-0.15f, 0.15f), Random.Range(-0.15f, 0.15f));
        }        

        //Axis information
        float inputX = Input.GetAxis("Horizontal") * (isInverted ? -1 : 1) ;
        float inputY = Input.GetAxis("Vertical") * (isInverted ? -1 : 1) ;

        // Pohyb 
        movement = new Vector2(speed.x * inputX * Time.deltaTime + ultraKejch.x, speed.y * inputY * Time.deltaTime + ultraKejch.y);
        transform.Translate(movement, 0);

        //Kontrola inverzniho ovladani
        if (invertTime != 0)
        {
            invertTime -= Time.deltaTime;
            powerCombo.effects.zmatek.GetComponent<Text>().text = "Zmatek: " + (int)invertTime;
            if (invertTime == 0 || invertTime < 0)
            {
                this.InversionControlling();
                invertTime = 0;
                powerCombo.effects.zmatek.SetActive(false);
            }
        }   

        //Kontrola, zda neni Ojoch zrovna nesmrtelny, pokud je, odecte cas z godMode a pokud dojde na nulu, okamzite mu opet zapne BoxCollider
        if (godMode != 0)
        {
            godMode -= Time.deltaTime;
            powerCombo.effects.smradostit.GetComponent<Text>().text = "Smradoštít: " + (int)godMode;
            if (godMode <= 0)
            {
                godMode = 0;
                powerCombo.effects.smradostit.SetActive(false);
                GameObject.Find("sprite").GetComponent<ColorChanger>().active = false;
            }
        }

        
        // Strelba    
        bool shoot = Input.GetButton("Fire1");          //Stisknutí mezerníku

        //Pokud chce hrac vystrelit, pouzije se skript weapon, který zavolá svou fci Attack a ubere mu to 1 život
        if (shoot)
        {
            if (weapons != null && weapons[0].CanAttack)
            {

                if (!isAkacko)
                {
                    weapons[0].Attack(false, new Vector2(1, 0));                       //atribut false -> jedna se o nepritele, kdo strili? 
                    animator.SetTrigger("fire");
                    managerSound.PlaySound(managerSound.clipShoot);                        //Zvuk vystrelu

                    if (contraBubles)
                    {
                        weapons[1].Attack(false, new Vector2(1, 0.7f));
                        weapons[2].Attack(false, new Vector2(1, -0.7f));                       

                    }
                }
                else
                {
                    weapons[0].Ak47Attack(false, new Vector2(1, 0));
                    animator.SetTrigger("akFire");
                    managerSound.PlaySound(managerSound.clipAk47);
                    if (contraBubles)
                    {
                        weapons[1].Ak47Attack(false, new Vector2(1, 0.7f));
                        weapons[2].Ak47Attack(false, new Vector2(1, -0.7f));
                    }

                }
            }
        } 
    }
    

    //Kolize 
    void OnCollisionEnter2D(Collision2D collision) {

        //S nepritelem -> ubere 2 zivotu a nepritele znici
        if (collision.gameObject.tag == "Enemy" && (godMode <= 0)) {
            socha.GetComponent<StatueControler>().howMuchForward += 0.75f;
            socha.GetComponent<StatueControler>().howMuchBack = 0;
            session.modifikatorScore -= 1;
            managerSound.PlayRandom(managerSound.clipDamage1, managerSound.clipDamage2);
            animator.SetTrigger("hit");

            collision.gameObject.GetComponent<Collider2D>().enabled = false;
            collision.gameObject.GetComponent<Animator>().SetTrigger("bDeath");
            GameManager.instance.GetComponent<SoundManager>().PlaySound(GameManager.instance.GetComponent<SoundManager>().clipEnemyHit);
            Destroy(collision.gameObject, 0.5f);
            if (playerHealth != null) {
                playerHealth.Damage(1);
                //healthSlider.value = playerHealth.hp;
            }
        }

        else if (collision.gameObject.tag == "Enemy" && ( godMode != 0)) 
        {
            GameManager.instance.GetComponent<SoundManager>().PlaySound(GameManager.instance.GetComponent<SoundManager>().clipEnemyHit);
            session.AdjustScore(10);
            session.killedEnemies += 1;
            session.FiveSecondsTimer();
            Destroy(collision.gameObject);
        }

        //S prekazkou -> ubere 5 zivotu a ucini na 3 vterin Ojocha nesmrtelnym
        if (collision.gameObject.tag == "Obstacle")
        {
            socha.GetComponent<StatueControler>().howMuchForward += 1;
            socha.GetComponent<StatueControler>().howMuchBack = 0;
            session.modifikatorScore -= 2;
            if (playerHealth != null || godMode == 0)
            {
                playerHealth.Damage(1);
                managerSound.PlayRandom(managerSound.clipDamage1, managerSound.clipDamage2);
                //healthSlider.value = playerHealth.hp;
            }
            collision.gameObject.GetComponent<ObstacleDestruction>().Destruction();
            godMode = 3;
        }

        //Se sochou
        if (collision.gameObject.tag == "Socha")
        {
            playerHealth.Damage(1);
            managerSound.PlayRandom(managerSound.clipDamage1, managerSound.clipDamage2);
            //healthSlider.value = playerHealth.hp;
            if (playerHealth.hp > 0)
            {
                animator.SetTrigger("isBack");                
            }     
                   
            session.modifikatorScore -= 3;     
            push = 0.25f;
        }

        //S powerUpem -> Zvysi pocet powerupu, provede efekt powerUpu, a pokud je sbran jiz druhy power up
        //provede se kombo
        if (collision.gameObject.tag == "PowerUp") {
            session.FiveSecondsTimer();            
            managerSound.PlaySound(managerSound.clipGrab);
            session.AdjustScore(5);                                                                 //Zapocitani skore 
            powerCombo.powerUps += 1;                                                               //zvyseni powerUpu
            powerCombo.powerUpCombo += collision.gameObject.GetComponent<PowerUpID>().powerUpID;    //pridani ID            
            collect.showObject(collision.gameObject.GetComponent<PowerUpID>().powerUpID, powerCombo.powerUps, powerCombo.powerUpCombo);
            

            //pokud je sebran jiz druhy powerUp -> provede se kombo
            if (powerCombo.powerUps == 2) {
                session.modifikatorScore += 1;
                powerCombo.PowerCombo(powerCombo.powerUpCombo);                                     //provedeni komba
                powerCombo.powerUps = 0;                                                            //nastaveni poctu powerupu na nula
                powerCombo.powerUpCombo = 0;                                                        //vymazani komba a priprava na dalsi                
            }
            
            Destroy(collision.gameObject);
        }
    }    
    
    //Inverze ovladani
    public void InversionControlling() {
        this.isInverted = !(isInverted);
    }     
}