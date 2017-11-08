using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PowerUpScript : MonoBehaviour {
    public int powerUps = 0;        //sebrano powerUpu
    public int powerUpCombo = 0;    //jake combo bude

    /// <summary>
    /// Jak dlouho se bude zobrazovat text aktivovaneho PowerUpu
    /// </summary>
    public float showTextLength = 3;
    
    public GameObject socha;
    public GameObject sessionController;
    public ShowingEffects effects;
    public GameObject powerUpImage;
    public GameObject ghostPrefab;   

    //promenne na panelText
    public Text panelText;                          //text    

    //Ultrakejch
    public float duseniTime;

   /// <summary>
   /// Doba po kterou  bude rambouch aktivni
   /// </summary>
    public float rambouchTime = 10;
    /// <summary>
    /// Doba, po kterou bude aktivni kontra strelba
    /// </summary>
    public float contraTime = 10;

    void Start() {
        socha = GameObject.Find("statue");
        sessionController = GameObject.Find("Session Controller");
        effects = sessionController.GetComponent<ShowingEffects>();
        panelText = GameObject.Find("PanelText").GetComponent<Text>();
        powerUpImage = GameObject.Find("PowerUpImage");
        powerUpImage.SetActive(false);
        duseniTime = 0;
    }

    /// <summary>
    /// Zobrazí text právě aktivovaného powerUpu
    /// </summary>
    /// <returns></returns>
    public IEnumerator ShowPowerUpText()
    {
        float duration = Time.time + showTextLength;
        while (Time.time < duration)
        {
            yield return null;
        }
        panelText.text = "";
        powerUpImage.SetActive(false);
    }

    /// <summary>
    /// Po určitou dobu be aktivní powerUp Rambouch
    /// </summary>
    /// <returns></returns>
    public IEnumerator RambouchCoroutine()
    {
        float duration = Time.time + rambouchTime;
        while (Time.time < duration)
        {
            effects.rText.text = "" + (int)(duration - Time.time + 1);
            yield return null;
        }
        OjochManager.instance.ojochScript.isAkacko = false;
        OjochManager.instance.ojochScript.animator.SetBool("isAk47", false);
        effects.rambouch.SetActive(false);
    }

    /// <summary>
    /// Po určitou dobu je aktivní kontra střelba
    /// </summary>
    /// <returns></returns>
    public IEnumerator ContraShoot()
    {
        float duration = Time.time + contraTime;
        while (Time.time < duration)
        {
            yield return null;
        }
        OjochManager.instance.ojochScript.contraBubles = false;
    }

    void Update() {
        //Kontrola Dušení
        if (duseniTime > 0)
        {
            duseniTime -= Time.deltaTime;
            effects.dText.text = "" + (int)(duseniTime + 1);
            if(duseniTime <= 0)
            {
                OjochManager.instance.ojochScript.kejch = false;
                OjochManager.instance.ojochScript.ultraKejch = new Vector2(0, 0);
                effects.duseni.SetActive(false);
                OjochManager.instance.ojochScript.animator.SetBool("duseni", false);
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
            OjochManager.instance.ojochScript.managerSound.PlaySound(OjochManager.instance.ojochScript.managerSound.clipGood);
            OjochManager.instance.ojochScript.animator.SetTrigger("good");
        }
        else
        {
            OjochManager.instance.ojochScript.managerSound.PlaySound(OjochManager.instance.ojochScript.managerSound.clipBad);
            OjochManager.instance.ojochScript.animator.SetTrigger("bad");
        }
        panelText.text = powerUp;
        StartCoroutine(ShowPowerUpText());
        powerUpImage.SetActive(true);
    }

    /// <summary>
    /// Iverzní ovládání + zrušení modif. skóre
    /// </summary>
    void Zmatek()
    {
        ShowPowerUpText(GameManager.instance.languageManager.GetTextValue("PowerUp.Confuse"), false);
        ScoreScript.instance.modifikatorScore = 1;

        OjochManager.instance.ojochScript.invertTime = 5;
        if (!OjochManager.instance.ojochScript.isInverted)
        {
            OjochManager.instance.ojochScript.InversionControlling();
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
        OjochManager.instance.ojochHealth.Damage(-1);
    }

    /// <summary>
    /// 30% sance, ze ojoch dosatne kontra strelbu, ak47, anebo oboji  
    /// </summary>
    void Rambouch()
    {
        panelText.text = GameManager.instance.languageManager.GetTextValue("PowerUp.Rambouch");;
        powerUpImage.SetActive(true);

        int chance = Random.Range(0, 100);

        if (chance <= 33)
        {
            OjochManager.instance.ojochScript.contraBubles = true;
            StartCoroutine(ContraShoot());
        }
        else if (chance > 33 && chance <= 66)
        {
            OjochManager.instance.ojochScript.isAkacko = true;
            OjochManager.instance.ojochScript.animator.SetBool("isAk47", true);
        }
        else
        {
            OjochManager.instance.ojochScript.contraBubles = true;            
            OjochManager.instance.ojochScript.isAkacko = true;
            OjochManager.instance.ojochScript.animator.SetBool("isAk47", true);
            StartCoroutine(ContraShoot());
        }

        OjochManager.instance.ojochScript.managerSound.PlaySound(OjochManager.instance.ojochScript.managerSound.clipGood);        
        ShowPowerUpText(GameManager.instance.languageManager.GetTextValue("PowerUp.Rambouch"), true);
        StartCoroutine(RambouchCoroutine());
        effects.rambouch.SetActive(true);
        CallEnemeyAK47();
        
    }
    
    /// <summary>
    /// Zvětší rychlost střelby nepřátel: veverkám, sputnikovi i prasatům
    /// </summary>
    void CallEnemeyAK47()
    {
        foreach(GameObject sputnik in SessionController.instance.sputniksInScene)
        {
            sputnik.GetComponent<CommonAI>().AK47();
        }
        foreach (GameObject squirrel in SessionController.instance.squirrelsInScene)
        {
            squirrel.GetComponent<CommonAI>().AK47();
        }
        foreach (GameObject pig in SessionController.instance.pigsInScene)
        {
            pig.GetComponent<CommonAI>().AK47();
        }
        foreach (GameObject bird in SessionController.instance.birdsInScene)
        {
            bird.GetComponent<CommonAI>().AK47();
        }
        foreach (GameObject rat in SessionController.instance.ratsInScene)
        {
            rat.GetComponent<CommonAI>().AK47();
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
        OjochManager.instance.ojochScript.kejch = true;
        duseniTime = 5;
        effects.duseni.SetActive(true);
        OjochManager.instance.ojochHealth.LooseSanity(1);

        var fuckingGhost = Instantiate(ghostPrefab) as GameObject;
        fuckingGhost.GetComponent<Transform>().position = transform.position + new Vector3(0.2f, -.5f, 0);
        GameManager.instance.GetComponent<SoundManager>().PlaySound(GameManager.instance.GetComponent<SoundManager>().ghost);
        OjochManager.instance.ojochScript.animator.SetBool("duseni", true);
    }

    /// <summary>
    /// Zpomali pohyb vseho a zaroven zpomali celkovou uroven zrychleni + 5s nesmrtelnsot
    /// </summary>
    void Nitro()
    {
        ShowPowerUpText(GameManager.instance.languageManager.GetTextValue("PowerUp.Nitro"), true);

        OjochManager.instance.ojochScript.godMode = 5;
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