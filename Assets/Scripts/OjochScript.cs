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
    public Text panelText;                          //text
    public bool cleanSock = false;                  //je powerUp Ciste ponozky aktivni?
    public CollectingScript collect;
    public GameObject socha;

    //Pro ultrakejch 
    public bool kejch;
    public Vector2 ultraKejch = new Vector2(0,0);

    //promenne na zivoty/palivo Ojocha
    public Slider healthSlider;                 //Ukazatel zdravi   
    public HealthScript playerHealth;

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

    /***
        ZVUKY
    ***/
    public AudioClip shootSound;
    public AudioClip damage1;
    public AudioClip damage2;
    public AudioClip grab;
    public AudioClip ak47;

    /// <summary>
    /// Co by mohlo z Ojocha pryc do jinych skriptu
    /// </summary> 
    /// 
    

    //promenne na score
    public Text scoreText;
    public Text modi;                   
    public float modifikatorScore = 1;              //Modfifikator
    public float tmpscore;                          //hracovo skore
    public float scorePerSecond = 0;                //pro zvyseni skore za kazdou vterinu  
    public int killedEnemies;                       //pocet zabitych nepratel
    public float tenSecondsTimer = 0;               //Timer na vynulovani modifikatoru skore
    public Slider tenSecondsSlider;
    public GameObject tenSecondsObject;
            

    void Start() {
        ojoch = GetComponent<Rigidbody2D>();
        powerCombo = GetComponent<PowerUpScript>();
        playerHealth = GetComponent<HealthScript>();        
        animator = transform.Find("sprite").gameObject.GetComponent<Animator>();
        weapons = GetComponentsInChildren<WeaponScript>();
        collect = GetComponent<CollectingScript>();
        kejch = false;
        socha = GameObject.Find("statue");
        


        //Co muze pryc
        scorePerSecond = 1;
        tmpscore = 0;
        killedEnemies = 0;
        tenSecondsObject.SetActive(false);
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
            powerCombo.effects.zmatekText.text = "Zmatek: " + (int)invertTime;
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
            powerCombo.effects.kosteniText.text = "Kosteni: " + (int)godMode;
            if (godMode <= 0)
            {
                godMode = 0;
                Destroy(transform.Find("smetacek(Clone)").gameObject);
                powerCombo.effects.kosteni.SetActive(false);
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
                    SoundScript.instance.PlaySingle(shootSound);                        //Zvuk vystrelu

                    if (contraBubles)
                    {
                        weapons[1].Attack(false, new Vector2(1, 0.7f));
                        weapons[2].Attack(false, new Vector2(1, -0.7f));                       

                    }
                }
                else
                {
                    weapons[0].Ak47Attack(false, new Vector2(1, 0));
                    SoundScript.instance.PlaySingle(ak47);
                    if (contraBubles)
                    {
                        weapons[1].Ak47Attack(false, new Vector2(1, 0.7f));
                        weapons[2].Ak47Attack(false, new Vector2(1, -0.7f));
                    }

                }
            }
        }        

        /// <summary>
        /// Co by mohlo z Ojocha pryc
        /// </summary> 

        //Skore
        this.scoreText.text = "Skóre: " + tmpscore;
        if (scorePerSecond <= 0) {
            tmpscore += 1 * modifikatorScore;
            scorePerSecond = 1;
        }
        scorePerSecond -= Time.deltaTime;  

        if(modifikatorScore < 1)
        {
            modifikatorScore = 1;
        }
        this.modi.text = "Modifikátor: " + modifikatorScore + "x";

        if (killedEnemies == 3)
        {
            modifikatorScore += 1;
            killedEnemies = 0;
        }

        if (modifikatorScore > 9)
        {
            modifikatorScore = 9;
        }

        if (tenSecondsTimer > 0)
        {
            
            tenSecondsTimer -= Time.deltaTime;
            tenSecondsSlider.value = tenSecondsTimer;
            if(tenSecondsTimer <= 0)
            {
                tenSecondsObject.SetActive(false);
                modifikatorScore = 0;
                killedEnemies = 0;

            }
        }
        
    }
    

    //Kolize 
    void OnCollisionEnter2D(Collision2D collision) {

        //S nepritelem -> ubere 2 zivotu a nepritele znici
        if (collision.gameObject.tag == "Enemy" && (!cleanSock || godMode <= 0)) {
            socha.GetComponent<StatueControler>().howMuchForward += 0.75f;
            socha.GetComponent<StatueControler>().howMuchBack = 0;
            modifikatorScore -= 1;
            SoundScript.instance.RandomSFX(damage1, damage2);
            animator.SetTrigger("hit");
            Destroy(collision.gameObject);
            if (playerHealth != null) {
                playerHealth.Damage(2);
                healthSlider.value = playerHealth.hp;
            }
        }

        else if (collision.gameObject.tag == "Enemy" && (cleanSock || godMode != 0)) 
        {
            Destroy(collision.gameObject);
            Destroy(this.transform.Find("sockClean(Clone)").gameObject);
            cleanSock = false;
        }

        //S prekazkou -> ubere 5 zivotu a ucini na 3 vterin Ojocha nesmrtelnym
        if (collision.gameObject.tag == "Obstacle")
        {
            socha.GetComponent<StatueControler>().howMuchForward += 1;
            socha.GetComponent<StatueControler>().howMuchBack = 0;
            modifikatorScore -= 2;
            if (playerHealth != null || godMode == 0)
            {
                playerHealth.Damage(5);
                SoundScript.instance.RandomSFX(damage1, damage2);
                healthSlider.value = playerHealth.hp;
            }
            collision.gameObject.GetComponent<ObstacleDestruction>().Destruction();
            godMode = 3;
        }

        //Se sochou
        if (collision.gameObject.tag == "Socha")
        {
            animator.SetTrigger("isBack");
            modifikatorScore -= 3;
            if (playerHealth != null)
            {
                playerHealth.Damage(30);
                SoundScript.instance.RandomSFX(damage1, damage2);
                healthSlider.value = playerHealth.hp;
            }
            godMode = 3;
            push = 0.25f;
        }


        //S powerUpem -> Zvysi pocet powerupu, provede efekt powerUpu, a pokud je sbran jiz druhy power up
        //provede se kombo
        if (collision.gameObject.tag == "PowerUp") {
            tenSecondsTimer = 5;
            tenSecondsObject.SetActive(true);
            tenSecondsSlider.value = tenSecondsTimer;
            SoundScript.instance.PlaySingle(grab);
            tmpscore += 5 * modifikatorScore;                                                       //Zapocitani skore 
            powerCombo.powerUps += 1;                                                               //zvyseni powerUpu
            powerCombo.powerUpCombo += collision.gameObject.GetComponent<PowerUpID>().powerUpID;    //pridani ID   
            collect.showObject(collision.gameObject.GetComponent<PowerUpID>().powerUpID, powerCombo.powerUps);

            //pokud je sebran jiz druhy powerUp -> provede se kombo
            if (powerCombo.powerUps == 2) {
                modifikatorScore += 1;
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