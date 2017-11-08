using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OjochScript : MonoBehaviour {
    /// <summary>
    /// Ojochova rychlost
    /// </summary>
    public Vector2 speed = new Vector2(10,10);
    private Vector2 movement;                       // Ulozeni pohybu  
    public PowerUpScript powerCombo;
    private WeaponScript[] weapons;
    public Animator animator;    
    public SoundManager managerSound;

    //Pro ultrakejch 
    public bool kejch = false;
    public Vector2 ultraKejch = new Vector2(0,0);
 
    //Kontra strelba
    public bool contraBubles = false;           //Rozptyl bublinek   

    //AK-47
    public bool isAkacko = false;               //ke Ak-47 aktivni?

    //Inverze
    public bool isInverted = false;             //Inverzni ovladani?
    public float invertTime = 0;

    //nesmrtelnost
    public float godMode = 0;                   //nesmrtelnost 
    
    //Zakaleni obrayovky, kdyz ojoch prijde o veskerou pricetnost
    public GameObject souflEffect;
    public float zakaleniTime = 0;
    private float zakaleniFade = 0;

    //Dalsi obekty
    public ColorChanger sprite;
    public SpinSocks sockPivot;
     
  
    void Start()
    {
        if (GameManager.instance.GetComponent<BonusManager>().ojochHasHat)
        {
            GameObject.Find("sprite").SetActive(false);           
        }
        else
        {
            GameObject.Find("hatSprite").SetActive(false);
        }
        
        powerCombo = GetComponent<PowerUpScript>();           
        weapons = GetComponentsInChildren<WeaponScript>();        
        managerSound = GameManager.instance.GetComponent<SoundManager>();        
        GetComponent<AudioSource>().volume = 0.3f * managerSound.soundsVolume;
        animator = GetComponentInChildren<Animator>();        
        sprite = GetComponentInChildren<ColorChanger>();
        sockPivot = GameObject.Find("rotatingSocks").GetComponent<SpinSocks>();
        sockPivot.enabled = false;
    }

    /// <summary>
    /// Po určitou dobu ude Ojocha pushovat kupředu
    /// </summary>
    /// <param name="length">Doba pushe</param>
    /// <returns></returns>
    public IEnumerator Push(float length)
    {
        float duration = Time.time + length;
        while (Time.time < duration)
        {
            transform.Translate(0.25f, 0, 0);
            yield return null;
        }
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
                sprite.active = false;               
                sockPivot.enabled = false;

                //GameObject.Find("sprite").GetComponent<ColorChanger>().active = false;
                //GameObject.Find("sock pivot").GetComponent<SpinSocks>().enabled = false;
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
                    managerSound.PlaySoundPitchShift(managerSound.clipShoot);                        //Zvuk vystrelu
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
                    managerSound.PlaySoundPitchShift(managerSound.clipAk47);
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
 
    /// <summary>
    /// Přepíná inverzní ovládání
    /// </summary>
    public void InversionControlling() {
        this.isInverted = !(isInverted);
    }    
    
    /// <summary>
    /// Přehraje jeden z Ojochových zranění.
    /// </summary>
    public void PlayDamageSound()
    {
        managerSound.PlayRandom(managerSound.clipDamage1, managerSound.clipDamage2);
    } 
}