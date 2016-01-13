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


    void Start() {
        ojoch = this.GetComponent<OjochScript>();
        socha = GameObject.Find("statue");
        kejchTime = 0;

    }

    void Update() {

        //Odpocet zobrazeni textu
        if (odpocet != 0)
        {
            odpocet -= Time.deltaTime;
            if (odpocet <= 0)
            {
                this.panelText.text = "";
                odpocet = 0;
            }
        }

        //Kontrola zpomaleni casu
        if (timeSlow > 0)
        {
            timeSlow -= Time.deltaTime;
            if (timeSlow <= 0)
            {
                SlowTime(false);
            }
        }

        //Kontrola Ultrakejchu
        if (kejchTime > 0)
        {
            kejchTime -= Time.deltaTime;
            if(kejchTime <= 0)
            {
                ojoch.kejch = false;
                ojoch.ultraKejch = new Vector2(0, 0);
            }
        }

        //Kontrola Akacka
        if (akTime > 0)
        {
            akTime -= Time.deltaTime;
            if (akTime <= 0)
            {
                ojoch.isAkacko = false;
                ojoch.animator.SetBool("isAk47", false);
            }
        }

        //Kontrola contra strelby
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
        switch (combo)
        {
            // Bublinky + Bublinky 
            // Prida Ojochovi palivo
            case 2:
                SoundScript.instance.PlaySingle(good);
                ojoch.playerHealth.Damage(-30);
                ojoch.healthSlider.value = ojoch.playerHealth.hp;
                panelText.text = "Bublinace!";
                odpocet = 3;
                ojoch.animator.SetTrigger("good");
                break;


            //bublinky + LP
            //Nulak
            //Vynuluje modifikator skore
            case 4:
                panelText.text = "Nulák!";
                ojoch.modifikatorScore = 1;
                odpocet = 3;
                break;


            //bublinky + ponozky
            // *** Cista ponozka ***
            //Rotujici ponozka kolem ojocha neguje 1 zraneni
            case 9:
                SoundScript.instance.PlaySingle(good);
                panelText.text = "Smradoštít";
                odpocet = 3;
                ojoch.cleanSock = true;
                var sock = Instantiate(sockClean) as Transform;                
                sock.position = transform.position + new Vector3(0.1f, -0.2f, 0);
                sock.parent = ojoch.transform;
                ojoch.animator.SetTrigger("good");
                break;
            

            //Bublinky + smetak  
            //Ojoch strili 3 bublinky naraz! Ve trech smerech                     
            case 12:
                SoundScript.instance.PlaySingle(good);
                panelText.text = "Prďák";                
                odpocet = 3;
                ojoch.contraBubles = true;
                contraTime = 10;
                ojoch.animator.SetTrigger("good");
                break;


            //bublinky + koreni
            // NITRO
            //Zpomali pohyb vseho a zaroven zpomali celkovou uroven zrychleni
            case 21:
                panelText.text = "NITRO";
                GameObject.Find("Session Controller").gameObject.GetComponent<SessionController>().speedUpTime += 4;
                GameObject.Find("Session Controller").gameObject.GetComponent<SessionController>().gameSpeed -= 1f;
                odpocet = 3;
                break;


            //lp + lp 
            // *** Zpomaleni casu ***
            case 6:
                SoundScript.instance.PlaySingle(good);
                panelText.text = "SLOWTIME!";
                odpocet = 1;
                SlowTime(true);
                ojoch.animator.SetTrigger("good");
                break;


            //lp + ponozky
            //Kovadleni
            case 11:
                panelText.text = "Kovadleni";
                odpocet = 3;
                break;


            //lp + smetak
            //DENItRO
            //Zrychli pohyb vseho a zaroven o neco zrychli celkovy posun zrychleni
            case 14:
                panelText.text = "DENItRO";
                GameObject.Find("Session Controller").gameObject.GetComponent<SessionController>().speedUpTime -= 5;
                GameObject.Find("Session Controller").gameObject.GetComponent<SessionController>().gameSpeed += 0.5f;
                odpocet = 3;
                break;


            //lp + koreni
            //Napoj lasky
            //Socha vysterli na Ojocha srdicka
            case 23:
                socha.GetComponent<StatueAttackScript>().heartAttack = true;
                SoundScript.instance.PlaySingle(bad);
                panelText.text = "Nápoj lásky";
                odpocet = 3;                
                break;


            //ponozky + ponozky
            //Dušení
            //Duch Prepere Smradinoha
            case 16:
                panelText.text = "Dušení!";
                odpocet = 3;
                break;


            //ponozky + smetak
            // Soufl
            // Zakali Ojochovi na nejakou dobu pohled
            case 19:
                panelText.text = "Šoufl!";
                odpocet = 3;
                break;


            //ponozky + koreni 
            //Inverzni ovladani
            case 28:
                SoundScript.instance.PlaySingle(bad);
                panelText.text = "Zmatek";
                odpocet = 3;
                ojoch.animator.SetTrigger("bad");
                //Inverze
                ojoch.InversionControlling();
                ojoch.invertTime = 10;
                break;


            //smetak + smetak 
            //Nesmrtelnost po určitou dobu - kolem ojocha budou rotovat 2 smetáky
            case 22:
                SoundScript.instance.PlaySingle(good);
                panelText.text = "Koštění";
                odpocet = 3;
                ojoch.godMode = 5;
                ojoch.animator.SetTrigger("good");

                var smet = Instantiate(smetacek) as Transform;
                smet.position = transform.position + new Vector3(0.2f, 0.2f, 0);
                smet.parent = ojoch.transform;
                break;


            //smetak + koreni
            //AK - na nejakou dobu - vyssi ucinnost zbrane
            case 31:
                panelText.text = "AK-47";
                odpocet = 3;

                ojoch.isAkacko = true;
                akTime = 10;
                ojoch.animator.SetTrigger("good");
                ojoch.animator.SetBool("isAk47", true);
                break;


            //koreni + koreni
            //ULTRAKEJCH
            //S ojochem to na nekolik vterin hazi
            case 40:
                panelText.text = "Ultrakejch!";
                ojoch.kejch = true;
                kejchTime = 5;
                odpocet = 3;
                break;               

        }
    }

    //Zpomali/vrati cas
    public void SlowTime(bool slow)
    {
        if (slow)
        {
            Time.timeScale = 0.5f;
            timeSlow = 3;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}