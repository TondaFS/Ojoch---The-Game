using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PowerUpScript : MonoBehaviour {
    public int powerUps = 0;        //sebrano powerUpu
    public int powerUpCombo = 0;    //jake combo bude
    private OjochScript ojoch;
    public Transform sockClean;
    public Transform smetacek;
    public GameObject socha;
    public GameObject sessionController;
    public ShowingEffects effects;
    public GameObject powerUpImage;
    public GameObject ghostPrefab;
        
    public AudioClip good;
    public AudioClip bad;

    //promenne na panelText
    public float odpocet = 0;                       //jak dlouho tam bude text
    public Text panelText;                          //text

    //Slowtime
    public float timeSlow = 0;                      //jak dlouho bude zpomaleny cas

    //Ultrakejch
    public float duseniTime;

    //Akacko
    public float akTime = 0;

    //Contra Bubbles
    public float contraTime = 0;

    //Zakaleni
    public GameObject souflEffect;
    public float zakaleniTime = 0;
    private float zakaleniFade = 0;


    void Start() {
        ojoch = this.GetComponent<OjochScript>();
        socha = GameObject.Find("statue");
        sessionController = GameObject.Find("Session Controller");
        effects = sessionController.GetComponent<ShowingEffects>();
        panelText = GameObject.Find("PanelText").GetComponent<Text>();
        powerUpImage = GameObject.Find("PowerUpImage");
        powerUpImage.SetActive(false);
        duseniTime = 0;
    }

    void Update() {

        //Odpocet zobrazeni textu
        if (odpocet != 0)
        {
            odpocet -= Time.deltaTime;
            if (odpocet <= 0)
            {
                panelText.text = "";
                odpocet = 0;
                powerUpImage.SetActive(false);
            }
        }

        //Kontrola zpomaleni casu
        if (timeSlow > 0)
        {            
            timeSlow -= Time.deltaTime;
            effects.slowtime.GetComponent<Text>().text = "SLOWTIME: " + (int)(timeSlow * 1.75f);
            if (timeSlow <= 0)
            {
                SlowTime(false);
                effects.slowtime.SetActive(false);
            }
        }

        //Kontrola Dušení
        if (duseniTime > 0)
        {
            duseniTime -= Time.deltaTime;
            effects.duseni.GetComponent<Text>().text = "Dušení: " + (int)duseniTime;
            if(duseniTime <= 0)
            {
                ojoch.kejch = false;
                ojoch.ultraKejch = new Vector2(0, 0);
                effects.duseni.SetActive(false);
                ojoch.animator.SetBool("duseni", false);
            }
        }

        //Kontrola Akacka
        if (akTime > 0)
        {
            akTime -= Time.deltaTime;
            effects.ak47.GetComponent<Text>().text = "AK-47: " + (int)akTime;
            if (akTime <= 0)
            {
                ojoch.isAkacko = false;
                ojoch.animator.SetBool("isAk47", false);
                effects.ak47.SetActive(false);
            }
        }

        //Kontrola contra strelby / prdak
        if (contraTime > 0)
        {
            contraTime -= Time.deltaTime;
            effects.prdak.GetComponent<Text>().text = "Prďák: " + (int)contraTime;
            if (contraTime <= 0)
            {
                ojoch.contraBubles = false;
                effects.prdak.SetActive(false);
            }
        }

        //Kontrola zakaleni
        if (zakaleniTime > 0)
        {
            zakaleniTime -= Time.deltaTime;
            effects.soufl.GetComponent<Text>().text = "Šoufl: " + (int)zakaleniTime;
            zakaleniFade = Mathf.Clamp(zakaleniFade + (Time.deltaTime * 2), 0, 1);
            SpriteRenderer[] souflSR = souflEffect.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer sr in souflSR)
            {
                sr.color = Color.Lerp(Color.clear, Color.white, zakaleniFade);
            }
        }
        else
        {
            zakaleniTime = 0;
            SpriteRenderer[] souflSR = souflEffect.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer sr in souflSR)
            {
                if (sr.color != Color.clear)
                {
                    zakaleniFade = Mathf.Clamp(zakaleniFade - (Time.deltaTime * 2), 0, 1);
                    sr.color = Color.Lerp(Color.clear, Color.white, zakaleniFade);
                    effects.soufl.SetActive(false);
                }                
            }            
        }
    }

    //provedení komba po sebrání 2 powerupů
    public void PowerCombo(int combo) {
        for (int i = 0; i < 3; i++)
        {
            if ((GameManager.instance.GetComponent<TaskManager>().activeTasks[i].type == "grab") && (GameManager.instance.GetComponent<TaskManager>().activeTasks[i].completed != true))
            {
                GameManager.instance.GetComponent<TaskManager>().CheckCountingTask(i);
            } else if (GameManager.instance.GetComponent<TaskManager>().activeTasks[i].type == "grabRound")
            {
                GameManager.instance.GetComponent<TaskManager>().grabsPerGame += 1;
            }
        }
        switch (combo)
        {
            /// <summary>
            /// BUBLINACE: Mýdlo + Mýdlo
            /// Přidá Ojochovi životy
            /// </summary>            
            
            case 2:
                ShowPowerUpText("Bublinace", true);                
                ojoch.playerHealth.Damage(-30);
                ojoch.healthSlider.value = ojoch.playerHealth.hp;
                break;

            /// <summary>
            /// ZMATEK: LP + LP
            /// Vynuluje modifikátor skóre a nastaví inverzní ovládání
            /// </summary>
            
            case 6:
                ShowPowerUpText("Zmatek", true);
                ojoch.session.modifikatorScore = 1;

                ojoch.invertTime = 10;
                if (!ojoch.isInverted)
                {
                    ojoch.InversionControlling();
                    effects.zmatek.SetActive(true);
                }                
                break;

            /// <summary>
            /// SMRADOŠTÍT: Mýdlo + Ponožka
            /// Nesmrtelnost (shield)
            /// </summary>  
            
            case 9:
                ShowPowerUpText("Smradoštít", true);
                ojoch.godMode = 5;
                effects.smradostit.SetActive(true);
                GameObject.Find("sprite").GetComponent<OpacityChanger>().active = true;                
                break;


            /// <summary>
            /// PRĎÁK: Mýdlo + Koření
            /// Zapnutí Contra střelby
            /// </summary>  
                    
            case 21:
                ShowPowerUpText("Prďák", true);
                ojoch.contraBubles = true;
                contraTime = 10;                
                effects.prdak.SetActive(true);
                break;


            /// <summary>
            /// NITRO: LP + Koreni
            /// Zpomali pohyb vseho a zaroven zpomali celkovou uroven zrychleni           
            /// </summary>

            case 23:
                ShowPowerUpText("NITRO", true);               

                sessionController.GetComponent<SessionController>().speedUpTime += 4;
                sessionController.GetComponent<SessionController>().gameSpeed -= 1f;
                socha.GetComponent<StatueControler>().howMuchForward = 0;
                socha.GetComponent<StatueControler>().howMuchBack = 2f;                
                break;

            /// <summary>
            /// AK47: Ponozka + Koreni
            /// AK47 na nejakou dobu
            /// </summary>  

            case 28:
                ShowPowerUpText("AK-47", true);

                ojoch.isAkacko = true;
                akTime = 10;
                ojoch.animator.SetTrigger("good");
                ojoch.animator.SetBool("isAk47", true);
                effects.ak47.SetActive(true);
                break;

            /// <summary>
            /// NÁPOJ LÁSKY: Mýdlo + LP
            /// Zrychli pohyb vseho a zaroven o neco zrychli celkovy posun zrychleni + Nápoj lásky
            /// </summary> 

            case 4:
                ShowPowerUpText("Nápoj lásky", false);
                socha.GetComponent<StatueAttackScript>().heartAttack = true;

                sessionController.GetComponent<SessionController>().speedUpTime -= 5;
                sessionController.GetComponent<SessionController>().gameSpeed += 0.5f;
                socha.GetComponent<StatueControler>().howMuchForward += 1.5f;
                socha.GetComponent<StatueControler>().howMuchBack = 0;
                break;


            /// <summary>
            /// PÁN ČASU: LP + Ponozka
            /// Zpomalení času
            /// </summary>  
            
            case 11:
                ShowPowerUpText("Pán času", true);
                odpocet = 1;
                SlowTime(true);                
                effects.slowtime.SetActive(true);                
                break;

            /// <summary>
            /// ŠOUFL: Ponozka + Ponozka
            /// Zakali Ojochovi na nejakou dobu pohled
            /// </summary> 

            case 16:
                ShowPowerUpText("Šoufl", false);
                zakaleniTime = 5;
                effects.soufl.SetActive(true);
                break;

            /// <summary>
            /// DUŠENÍ: Koření + Koření
            ///  S ojochem to na nekolik vterin hazi a objeví se duch           
            /// </summary> 
            
            case 40:
                ShowPowerUpText("Dušení", false);
                ojoch.kejch = true;
                duseniTime = 5;
                effects.duseni.SetActive(true);                
                ojoch.playerHealth.LooseSanity(5);                

                var fuckingGhost = Instantiate(ghostPrefab) as GameObject;                              
                fuckingGhost.GetComponent<Transform>().position = transform.position + new Vector3(1f, 0, 0);
                GameManager.instance.GetComponent<SoundManager>().PlaySound(GameManager.instance.GetComponent<SoundManager>().ghost);
                ojoch.animator.SetBool("duseni", true);
                break;
        }
    }

    //Zpomali/vrati cas
    public void SlowTime(bool slow)
    {
        if (slow)
        {
            ojoch.managerSound.setPitch(ojoch.managerSound.soundAudioSource, 0.5f);
            Time.timeScale = 0.5f;
            timeSlow = 3;            
        }
        else
        {
            ojoch.managerSound.setPitch(ojoch.managerSound.soundAudioSource, 1f);
            Time.timeScale = 1;            
        }
    }

    //Zobrazi text powerupu
    public void ShowPowerUpText(string powerUp, bool type) {
        if (type)
        {
            ojoch.managerSound.PlaySound(ojoch.managerSound.clipGood);
            ojoch.animator.SetTrigger("good");
        }
        else
        {
            ojoch.managerSound.PlaySound(ojoch.managerSound.clipBad);
            ojoch.animator.SetTrigger("bad");
        }
        panelText.text = powerUp;
        odpocet = 3;
        powerUpImage.SetActive(true);
    }
}