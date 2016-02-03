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

    
    //Zakaleni obrayovky, kdyz ojoch prijde o veskerou pricetnost
    public GameObject souflEffect;
    public float zakaleniTime = 0;
    private float zakaleniFade = 0;
    


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
        
        //Kontrola zakaleni obrazovkz
        if (zakaleniTime > 0)
        {
            zakaleniFade = Mathf.Clamp(zakaleniFade + (Time.deltaTime * 2), 0, 1);
            SpriteRenderer[] souflSR = souflEffect.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer sr in souflSR)
            {
                sr.color = Color.Lerp(Color.clear, Color.white, zakaleniFade);
            }
        }

        //Odražení od Sochy
        if (push > 0)
        {
            transform.Translate(0.25f, 0, 0);
            push -= Time.deltaTime;
        }        

        //Ultrakejch = pri duseni se Ojoch trese
        if (kejch)
        {
            ultraKejch = new Vector2(Random.Range(-0.15f, 0.15f), Random.Range(-0.15f, 0.15f));
        }        

        //Axis information
        float inputX = Input.GetAxisRaw("Horizontal") * (isInverted ? -1 : 1) ;
        float inputY = Input.GetAxisRaw("Vertical") * (isInverted ? -1 : 1) ;

        // Pohyb 
        movement = new Vector2(speed.x * inputX * Time.deltaTime + ultraKejch.x, speed.y * inputY * Time.deltaTime + ultraKejch.y);
        transform.Translate(movement, 0);

        //Kontrola inverzniho ovladani
        if (invertTime != 0)
        {
            invertTime -= Time.deltaTime;
            powerCombo.effects.zText.text = "" + (int)(invertTime + 1);
            if (invertTime == 0 || invertTime < 0)
            {
                this.InversionControlling();
                invertTime = 0;
                powerCombo.effects.zmatek.SetActive(false);
            }
        }   

        //Kontrola, zda neni Ojoch zrovna nesmrtelny, pokud je, odecte cas z godMode
        if (godMode != 0)
        {
            godMode -= Time.deltaTime;
            powerCombo.effects.sText.text = "" + (int)(godMode + 1);
            if (godMode <= 0)
            {
                godMode = 0;
                powerCombo.effects.smradostit.SetActive(false);
                GameObject.Find("sprite").GetComponent<ColorChanger>().active = false;
                GameObject.Find("sock pivot").GetComponent<SpinSocks>().enabled = false;
            }
        }

        
        // Strelba    
        bool shoot = Input.GetButton("Fire1");          //Stisknutí mezerníku

        //Pokud chce hrac vystrelit, pouzije se skript weapon, který zavolá svou fci Attack 
        if (shoot)
        {
            if (weapons != null && weapons[0].CanAttack)
            {

                if (!isAkacko)
                {
                    weapons[0].Attack(false, new Vector2(1, 0));                       //atribut false -> jedna se o nepritele, kdo strili? 
                    animator.SetTrigger("fire");
                    managerSound.PlaySound(managerSound.clipShoot);                        //Zvuk vystrelu

                    //Pokud je aktivovany prdak: Contra strelba
                    if (contraBubles)
                    {
                        weapons[1].Attack(false, new Vector2(1, 0.7f));
                        weapons[2].Attack(false, new Vector2(1, -0.7f));                       

                    }
                }

                //pokud je aktivovany prdak: Ak47
                else
                {
                    weapons[0].Ak47Attack(false, new Vector2(1, 0));
                    animator.SetTrigger("akFire");
                    managerSound.PlaySound(managerSound.clipAk47);

                    //Ak47 + contra
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

        //S nepritelem -> ubere zivot a nepritele znici
        if ((collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Boss") && (godMode <= 0)) {

            //Posunuti sochy kupredu
            socha.GetComponent<StatueControler>().howMuchForward += 0.75f;
            socha.GetComponent<StatueControler>().howMuchBack = 0;

            session.modifikatorScore -= 1;
            managerSound.PlayRandom(managerSound.clipDamage1, managerSound.clipDamage2);
            animator.SetTrigger("hit");

            //Pokud se nejedna o Bosse, je nepritel znicen, prehrana animace, a zvuk narazeni do nepritele
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<Collider2D>().enabled = false;
                collision.gameObject.GetComponent<Animator>().SetTrigger("bDeath");
                GameManager.instance.GetComponent<SoundManager>().PlaySound(GameManager.instance.GetComponent<SoundManager>().clipEnemyHit);
                Destroy(collision.gameObject, 0.5f);
            }

            //Ojochovi da zraneni
            if (playerHealth != null) {
                playerHealth.Damage(1);
            }
        }

        //Pokud je srazka a Ojoch je nesmrtelny, zvuk srazky a upravi se skore a podobne
        else if ((collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Boss") && ( godMode != 0)) 
        {
            collision.gameObject.GetComponent<Collider2D>().enabled = false;
            collision.gameObject.GetComponent<Animator>().SetTrigger("bDeath");
            GameManager.instance.GetComponent<SoundManager>().PlaySound(GameManager.instance.GetComponent<SoundManager>().clipEnemyHit);
            Destroy(collision.gameObject, 0.5f);

            session.AdjustScore(10);
            session.killedEnemies += 1;
            session.FiveSecondsTimer();            
        }

        //S prekazkou -> ubere zivot a ucini na 3 vterin Ojocha nesmrtelnym
        if (collision.gameObject.tag == "Obstacle")
        {
            //Posunuti sochy
            socha.GetComponent<StatueControler>().howMuchForward += 1;
            socha.GetComponent<StatueControler>().howMuchBack = 0;
            session.modifikatorScore -= 2;

            //Ojoch neni mrty a neni nesmrtelny -> dostane zraneni a prehraje zvuk zraneni
            if (playerHealth != null || godMode == 0)
            {
                playerHealth.Damage(1);
                managerSound.PlayRandom(managerSound.clipDamage1, managerSound.clipDamage2);
            }

            collision.gameObject.GetComponent<ObstacleDestruction>().Destruction();
            godMode = 3;
        }

        //Se sochou -> Da zraneni, prehraje se animace a zvuk, Ojocha to odhcodi kupredu
        if (collision.gameObject.tag == "Socha")
        {
            playerHealth.Damage(1);
            managerSound.PlayRandom(managerSound.clipDamage1, managerSound.clipDamage2);
            GameObject.Find("Statue Sprite").GetComponent<Animator>().SetTrigger("hit");
            if (playerHealth.hp > 0)
            {
                animator.SetTrigger("isBack");                
            }     
                   
            session.modifikatorScore -= 3;     
            push = 0.25f;
        }

        //S powerUpem -> Zvysi pocet sebranych predmety a pokud je sbran jiz druhy , provede kombo }powerUp/Down]
        if (collision.gameObject.tag == "PowerUp") {
            session.FiveSecondsTimer();                            
            managerSound.PlaySound(managerSound.clipGrab);      //zvuk sebrani
            session.AdjustScore(5);                                                                 //Zapocitani skore 
            powerCombo.powerUps += 1;                                                               //zvyseni powerUpu
            powerCombo.powerUpCombo += collision.gameObject.GetComponent<PowerUpID>().powerUpID;    //pridani ID   
            
            //zobrazeni sebraneho predmetu         
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