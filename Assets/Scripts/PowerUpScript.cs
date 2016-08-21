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
            //POWERUPS

            /// <summary>
            /// BUBLINACE: Mýdlo + Mýdlo
            /// Přidá Ojochovi životy
            /// </summary>    
            case 2:
                string text = GameManager.instance.languageManager.GetTextValue("PowerUp.Bubbles");
                ShowPowerUpText(text, true);                
                ojoch.playerHealth.Damage(-1);                
                break;

            /// <summary>
            /// NITRO: Ponozka + Ponozka
            /// Zpomali pohyb vseho a zaroven zpomali celkovou uroven zrychleni + 5s nesmrtelnsot    
            /// </summary> 
            case 16:
                ShowPowerUpText("NITRO", true);

                ojoch.godMode = 5;
                effects.smradostit.SetActive(true);
                GameObject.Find("sprite").GetComponent<ColorChanger>().active = true;
                GameObject.Find("sock pivot").GetComponent<SpinSocks>().enabled = true;

                sessionController.GetComponent<SessionController>().speedUpTime += 4;
                sessionController.GetComponent<SessionController>().gameSpeed -= 1f;
                socha.GetComponent<StatueControler>().howMuchForward = 0;
                socha.GetComponent<StatueControler>().howMuchBack = 2f;
                break;

            /// <summary>
            /// AK47: Koření + Koření
            ///  30% sance, ze ojoch dosatne kontra strelbu, ak47, anebo oboji        
            /// </summary> 
            case 40:
                ShowPowerUpText("Rambouch", true);
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

                akTime = 10;
                effects.rambouch.SetActive(true);
                break;


           //POWERDOWNS

            /// <summary>
            /// ZMATEK: Mýdlo + Koření
            /// Iverzní ovládání + zrušení modif. skóre
            /// </summary>  
            case 21:
                ShowPowerUpText("Zmatek", false);
                ojoch.session.modifikatorScore = 1;

                ojoch.invertTime = 5;
                if (!ojoch.isInverted)
                {
                    ojoch.InversionControlling();
                    effects.zmatek.SetActive(true);
                }
                break;

            /// <summary>
            /// NÁPOJ LÁSKY: Mýdlo + Ponožka
            /// Zrychli pohyb vseho a zaroven o neco zrychli celkovy posun zrychleni + Nápoj lásky(socha vystreli srdicka) 
            /// </summary>  
            case 9:
                ShowPowerUpText("Nápoj lásky", false);
                socha.GetComponent<StatueAttackScript>().heartAttack = true;
                GameObject.Find("Statue Sprite").GetComponent<Animator>().SetTrigger("shoot");

                sessionController.GetComponent<SessionController>().speedUpTime -= 5;
                sessionController.GetComponent<SessionController>().gameSpeed += 0.5f;
                socha.GetComponent<StatueControler>().howMuchForward += 1.5f;
                socha.GetComponent<StatueControler>().howMuchBack = 0;
                break;

            /// <summary>
            /// DUŠENÍ: Ponozka + Koreni
            /// S ojochem to na nekolik vterin hazi a objeví se duch  
            /// </summary>  
            case 28:
                ShowPowerUpText("Dušení", false);
                ojoch.kejch = true;
                duseniTime = 5;
                effects.duseni.SetActive(true);
                ojoch.playerHealth.LooseSanity(1);

                var fuckingGhost = Instantiate(ghostPrefab) as GameObject;
                fuckingGhost.GetComponent<Transform>().position = transform.position + new Vector3(0.2f, -.5f, 0);
                GameManager.instance.GetComponent<SoundManager>().PlaySound(GameManager.instance.GetComponent<SoundManager>().ghost);
                ojoch.animator.SetBool("duseni", true);
                break;  
        }
    }

    //Zobrazi text powerupu a prehraje dobry/spatny zvuk
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