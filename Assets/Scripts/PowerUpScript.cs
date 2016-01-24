﻿using UnityEngine;
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
        
    public AudioClip good;
    public AudioClip bad;

    //promenne na panelText
    public float odpocet = 0;                       //jak dlouho tam bude text
    public Text panelText;                          //text

    //Slowtime
    public float timeSlow = 0;                      //jak dlouho bude zpomaleny cas

    //Ultrakejch
    public float kejchTime;

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
        kejchTime = 0;
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
            effects.slowtimeText.text = "SLOWTIME: " + (int)timeSlow;
            if (timeSlow <= 0)
            {
                SlowTime(false);
                effects.slowtime.SetActive(false);
            }
        }

        //Kontrola Ultrakejchu
        if (kejchTime > 0)
        {
            kejchTime -= Time.deltaTime;
            effects.ultrakejchText.text = "Ultrakejch: " + (int)kejchTime;
            if(kejchTime <= 0)
            {
                ojoch.kejch = false;
                ojoch.ultraKejch = new Vector2(0, 0);
                effects.ultrakejch.SetActive(false);
            }
        }

        //Kontrola Akacka
        if (akTime > 0)
        {
            akTime -= Time.deltaTime;
            effects.ak47Text.text = "AK-47: " + (int)akTime;
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
            effects.prdakText.text = "Prďák: " + (int)contraTime;
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
            effects.souflText.text = "Šoufl: " + (int)zakaleniTime;
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
        if ((GameManager.instance.GetComponent<TaskManager>().activeTask.type == "grab") && (GameManager.instance.GetComponent<TaskManager>().activeTask.completed != true))
        {
            GameManager.instance.GetComponent<TaskManager>().CheckCountingTask();
        }
        switch (combo)
        {
            // Bublinky + Bublinky 
            // Prida Ojochovi palivo
            case 2:
                ShowPowerUpText("Bublinace", true);                
                ojoch.playerHealth.Damage(-30);
                ojoch.healthSlider.value = ojoch.playerHealth.hp;
                break;


            //bublinky + LP
            //Nulak
            //Vynuluje modifikator skore
            case 4:
                ShowPowerUpText("Nulák", true);
                ojoch.session.modifikatorScore = 1;
                break;


            //bublinky + ponozky
            // *** Cista ponozka ***
            //Rotujici ponozka kolem ojocha neguje 1 zraneni
            case 9:
                ShowPowerUpText("Smradoštít", true);
                ojoch.cleanSock = true;
                var sock = Instantiate(sockClean) as Transform;                
                sock.position = transform.position + new Vector3(0.1f, -0.2f, 0);
                sock.parent = ojoch.transform;                
                break;
            

            //Bublinky + smetak  
            //Ojoch strili 3 bublinky naraz! Ve trech smerech                     
            case 12:
                ShowPowerUpText("Prďák", true);
                ojoch.contraBubles = true;
                contraTime = 10;                
                effects.prdak.SetActive(true);
                break;


            //bublinky + koreni
            // NITRO
            //Zpomali pohyb vseho a zaroven zpomali celkovou uroven zrychleni
            case 21:
                ShowPowerUpText("NITRO", true);
                sessionController.GetComponent<SessionController>().speedUpTime += 4;
                sessionController.GetComponent<SessionController>().gameSpeed -= 1f;
                socha.GetComponent<StatueControler>().howMuchForward = 0;
                socha.GetComponent<StatueControler>().howMuchBack = 2f;
                
                break;


            //lp + lp 
            // *** Zpomaleni casu ***
            case 6:
                ShowPowerUpText("SLOWTIME", true);
                odpocet = 1;
                SlowTime(true);                
                effects.slowtime.SetActive(true);                
                break;


            //lp + ponozky
            //Kovadleni
            case 11:
                ShowPowerUpText("Kovadlení", false);
                break;


            //lp + smetak
            //DENItRO
            //Zrychli pohyb vseho a zaroven o neco zrychli celkovy posun zrychleni
            case 14:
                ShowPowerUpText("DENItRO", false);
                sessionController.GetComponent<SessionController>().speedUpTime -= 5;
                sessionController.GetComponent<SessionController>().gameSpeed += 0.5f;
                socha.GetComponent<StatueControler>().howMuchForward += 1.5f;
                socha.GetComponent<StatueControler>().howMuchBack = 0;
                
                break;


            //lp + koreni
            //Napoj lasky
            //Socha vysterli na Ojocha srdicka
            case 23:
                ShowPowerUpText("Nápoj lásky", false);
                socha.GetComponent<StatueAttackScript>().heartAttack = true;
                break;


            //ponozky + ponozky
            //Dušení
            //Duch Prepere Smradinoha
            case 16:
                ShowPowerUpText("Dušení", false);
                break;


            //ponozky + smetak
            // Soufl
            // Zakali Ojochovi na nejakou dobu pohled
            case 19:
                ShowPowerUpText("Šoufl", false);
                zakaleniTime = 5;
                effects.soufl.SetActive(true);
                break;


            //ponozky + koreni 
            //Inverzni ovladani
            case 28:
                ShowPowerUpText("Zmatek", false);

                //Inverze
                ojoch.InversionControlling();
                ojoch.invertTime = 10;
                effects.zmatek.SetActive(true);
                break;


            //smetak + smetak 
            //Nesmrtelnost po určitou dobu - kolem ojocha budou rotovat 2 smetáky
            case 22:
                ShowPowerUpText("Koštění", true);

                ojoch.godMode = 5;
                var smet = Instantiate(smetacek) as Transform;
                smet.position = transform.position + new Vector3(0.2f, 0.2f, 0);
                smet.parent = ojoch.transform;
                effects.kosteni.SetActive(true);
                break;


            //smetak + koreni
            //AK - na nejakou dobu - vyssi ucinnost zbrane
            case 31:
                ShowPowerUpText("AK-47", true);

                ojoch.isAkacko = true;
                akTime = 10;
                ojoch.animator.SetTrigger("good");
                ojoch.animator.SetBool("isAk47", true);
                effects.ak47.SetActive(true);
                break;


            //koreni + koreni
            //ULTRAKEJCH
            //S ojochem to na nekolik vterin hazi
            case 40:
                ShowPowerUpText("Ultrakejch", false);

                ojoch.kejch = true;
                kejchTime = 5;                
                effects.ultrakejch.SetActive(true);
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