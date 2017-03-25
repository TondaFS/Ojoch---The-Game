using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PowerUpScript : MonoBehaviour {
    public int powerUps = 0;        //sebrano powerUpu
    public int powerUpCombo = 0;    //jake combo bude

    private OjochScript ojoch;
    public GameObject socha;
    public GameObject sessionController;
    public ShowingEffects effects;
    public GameObject powerUpImage;
    public GameObject ghostPrefab;   

    //promenne na panelText
    public float odpocet = 0;                       //jak dlouho tam bude text
    public Text panelText;                          //text    

    //Ultrakejch
    public float duseniTime;

    //Akacko
    public float akTime = 0;

    //Contra Bubbles
    public float contraTime = 0;

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

        //Kontrola Dušení
        if (duseniTime > 0)
        {
            duseniTime -= Time.deltaTime;
            effects.dText.text = "" + (int)(duseniTime + 1);
            if(duseniTime <= 0)
            {
                ojoch.kejch = false;
                ojoch.ultraKejch = new Vector2(0, 0);
                effects.duseni.SetActive(false);
                ojoch.animator.SetBool("duseni", false);
            }
        }

        //Kontrola Rambouch
        if (akTime > 0)
        {
            akTime -= Time.deltaTime;
            effects.rText.text = "" + (int)(akTime + 1);
            if (akTime <= 0)
            {
                ojoch.isAkacko = false;
                ojoch.animator.SetBool("isAk47", false);
                effects.rambouch.SetActive(false);
            }
        }

        
        //Kontrola contra strelby / prdak
        if (contraTime > 0)
        {
            contraTime -= Time.deltaTime;
            if (contraTime <= 0)
            {
                ojoch.contraBubles = false;
            }
        }
        
    }

    //provedení komba po sebrání 2 powerupů
    public void PowerCombo(int combo) {
        sessionController.GetComponent<EndGameScript>().powersInSession += 1;

        switch (combo)
        {            
            //Mýdlo + Mýdlo  
            case 2:
                Zmatek();
                break;

            //Mýdlo + Ponožka           
            case 9:
                Nitro();
                break;

            //Ponozka + Ponozka
            case 16:
                Duseni();
                break;

            //Mýdlo + Koření
            case 21:
                Bublinace();
                break;

            //Ponozka + Koreni
            case 28:
                Rambouch();
                break;

            //Koření + Koření
            case 40:
                NapojLasky();            
                break;
        }
    }

    //Zobrazi text powerupu a prehraje dobry/spatny zvuk
    /// <summary>
    /// Zobrazi text powerUpu a přehraje zvuk
    /// </summary>
    /// <param name="powerUp">Jméno PowerUpu</param>
    /// <param name="type">Dobrý (true) / Špatný (false) zvuk</param>
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

    /// <summary>
    /// Iverzní ovládání + zrušení modif. skóre
    /// </summary>
    void Zmatek()
    {
        ShowPowerUpText(GameManager.instance.languageManager.GetTextValue("PowerUp.Confuse"), false);
        ojoch.session.modifikatorScore = 1;

        ojoch.invertTime = 5;
        if (!ojoch.isInverted)
        {
            ojoch.InversionControlling();
            effects.zmatek.SetActive(true);
        }
        
    }

    /// <summary>
    /// Obnovi Ojochovi životy
    /// </summary>
    void Bublinace()
    {
        string text = GameManager.instance.languageManager.GetTextValue("PowerUp.Bubbles");
        ShowPowerUpText(text, true);
        ojoch.playerHealth.Damage(-1);
    }

    /// <summary>
    /// 30% sance, ze ojoch dosatne kontra strelbu, ak47, anebo oboji  
    /// </summary>
    void Rambouch()
    {
        panelText.text = GameManager.instance.languageManager.GetTextValue("PowerUp.Rambouch");
        odpocet = 3;
        powerUpImage.SetActive(true);

        int chance = Random.Range(0, 100);

        if (chance <= 33)
        {
            ojoch.contraBubles = true;
            contraTime = 10;
        }
        else if (chance > 33 && chance <= 66)
        {
            ojoch.isAkacko = true;
            akTime = 10;
            ojoch.animator.SetBool("isAk47", true);
        }
        else
        {
            ojoch.contraBubles = true;
            contraTime = 10;

            ojoch.isAkacko = true;
            akTime = 10;
            ojoch.animator.SetBool("isAk47", true);
        }

        ojoch.managerSound.PlaySound(ojoch.managerSound.clipGood);        
        //ShowPowerUpText(GameManager.instance.languageManager.GetTextValue("PowerUp.Rambouch"), true);
        akTime = 10;
        effects.rambouch.SetActive(true);
        SpeedUpEnemeyCooldown();
    }

    /// <summary>
    /// Zvětší rychlost střelby nepřátel
    /// </summary>
    void SpeedUpEnemeyCooldown()
    {
        foreach(GameObject sputnik in SessionController.instance.sputniksInScene)
        {
            sputnik.GetComponent<ShooterAI>().ChangeMissileCooldown(-0.5f);
        }
        foreach (GameObject squirrel in SessionController.instance.squirrelsInScene)
        {
            squirrel.GetComponent<ShooterAI>().ChangeMissileCooldown(-0.5f);
        }
        foreach (GameObject pig in SessionController.instance.pigsInScene)
        {
            pig.GetComponent<ShooterAI>().ChangeMissileCooldown(-0.5f);
        }
    }

    /// <summary>
    ///  Zrychli pohyb vseho a zaroven o neco zrychli celkovy posun zrychleni + Nápoj lásky(socha vystreli sip) 
    /// </summary>
    void NapojLasky()
    {
        ShowPowerUpText(GameManager.instance.languageManager.GetTextValue("PowerUp.Love"), false);
        socha.GetComponent<StatueAttackScript>().heartAttack = true;
        GameObject.Find("Statue Sprite").GetComponent<Animator>().SetTrigger("shoot");

        sessionController.GetComponent<SessionController>().speedUpTime -= 5;
        sessionController.GetComponent<SessionController>().gameSpeed += 0.5f;
        socha.GetComponent<StatueControler>().howMuchForward += 1.5f;
        socha.GetComponent<StatueControler>().howMuchBack = 0;
    }

    /// <summary>
    /// S ojochem to na nekolik vterin hazi a objeví se duch  
    /// </summary>
    void Duseni()
    {
        ShowPowerUpText(GameManager.instance.languageManager.GetTextValue("PowerUp.Ghost"), false);
        ojoch.kejch = true;
        duseniTime = 5;
        effects.duseni.SetActive(true);
        ojoch.playerHealth.LooseSanity(1);

        var fuckingGhost = Instantiate(ghostPrefab) as GameObject;
        fuckingGhost.GetComponent<Transform>().position = transform.position + new Vector3(0.2f, -.5f, 0);
        GameManager.instance.GetComponent<SoundManager>().PlaySound(GameManager.instance.GetComponent<SoundManager>().ghost);
        ojoch.animator.SetBool("duseni", true);
    }

    /// <summary>
    /// Zpomali pohyb vseho a zaroven zpomali celkovou uroven zrychleni + 5s nesmrtelnsot
    /// </summary>
    void Nitro()
    {
        ShowPowerUpText(GameManager.instance.languageManager.GetTextValue("PowerUp.Nitro"), true);

        ojoch.godMode = 5;
        effects.smradostit.SetActive(true);
        OjochManager.instance.ojochScript.sprite.active = true;
        OjochManager.instance.ojochScript.sockPivot.enabled = true;
        //GameObject.Find("sprite").GetComponent<ColorChanger>().active = true;
        //GameObject.Find("sock pivot").GetComponent<SpinSocks>().enabled = true;

        sessionController.GetComponent<SessionController>().speedUpTime += 4;
        sessionController.GetComponent<SessionController>().gameSpeed -= 1f;
        socha.GetComponent<StatueControler>().howMuchForward = 0;
        socha.GetComponent<StatueControler>().howMuchBack = 2f;
    }
}