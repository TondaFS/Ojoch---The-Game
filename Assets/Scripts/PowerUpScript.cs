using UnityEngine;
using System.Collections;

public class PowerUpScript : MonoBehaviour {
    public int powerUps = 0;        //sebrano powerUpu
    public int powerUpCombo = 0;    //jake combo bude
    private HealthScript health;
    private OjochScript ojoch;
    public Transform sockClean;
    public Transform smetacek;
    public GameObject socha;

    public AudioClip good;
    public AudioClip bad;


    void Start() {
        health = this.GetComponent<HealthScript>();
        ojoch = this.GetComponent<OjochScript>();
        socha = GameObject.Find("statue");

    }

    //provedení komba po sebrání 2 powerupů
    public void PowerCombo(int combo) {
        switch (combo)
        {
            // Bublinky + Bublinky 
            // Prida Ojochovi palivo
            case 2:
                SoundScript.instance.PlaySingle(good);
                health.Damage(-20);
                ojoch.healthSlider.value = health.hp;
                ojoch.panelText.text = "Bublinace!";
                ojoch.odpocet = 3;
                ojoch.animator.SetTrigger("good");
                break;


            //bublinky + LP
            case 4:
                ojoch.panelText.text = "Velký bratr tě vidí!";
                ojoch.odpocet = 3;
                break;


            //bublinky + ponozky
            // *** Cista ponozka ***
            //Rotujici ponozka kolem ojocha neguje 1 zraneni
            case 9:
                SoundScript.instance.PlaySingle(good);
                ojoch.panelText.text = "Smradoštít";
                ojoch.odpocet = 3;
                ojoch.cleanSock = true;
                var sock = Instantiate(sockClean) as Transform;                
                sock.position = transform.position + new Vector3(0.1f, -0.2f, 0);
                sock.parent = ojoch.transform;
                ojoch.animator.SetTrigger("good");
                break;
            

            //Bublinky + smetak  
            //Ojoch strili 3 bublinky naraz! Ve trech smerech - 10 vystrelu                    
            case 12:
                SoundScript.instance.PlaySingle(good);
                ojoch.panelText.text = "Prďák";                
                ojoch.odpocet = 3;
                ojoch.contraBubles = true;
                ojoch.animator.SetTrigger("good");
                break;


            //bublinky + koreni
            case 21:
                ojoch.panelText.text = "Sbírej jako dobrý kapitalista";
                ojoch.odpocet = 3;
                break;


            //lp + lp 
            // *** Zpomaleni casu ***
            case 6:
                SoundScript.instance.PlaySingle(good);
                ojoch.panelText.text = "SLOWTIME!";
                ojoch.odpocet = 1;
                ojoch.SlowTime(true);
                ojoch.animator.SetTrigger("good");
                break;


            //lp + ponozky
            case 11:
                ojoch.panelText.text = "BREAKDANCE!";
                ojoch.odpocet = 3;
                break;


            //lp + smetak
            case 14:
                ojoch.panelText.text = "'MC Smeták' - Mc Citát";
                ojoch.odpocet = 3;
                break;


            //lp + koreni
            //Napoj lasky
            //Socha vysterli na Ojocha srdicka
            case 23:
                socha.GetComponent<StatueAttackScript>().heartAttack = true;
                SoundScript.instance.PlaySingle(bad);
                ojoch.panelText.text = "Nápoj lásky";
                ojoch.odpocet = 3;                
                break;


            //ponozky + ponozky
            case 16:
                ojoch.panelText.text = "To je smrad!";
                ojoch.odpocet = 3;
                break;


            //ponozky + smetak
            case 19:
                ojoch.panelText.text = "Nothing here!";
                ojoch.odpocet = 3;
                break;


            //ponozky + koreni 
            //Inverzni ovladani
            case 28:
                SoundScript.instance.PlaySingle(bad);
                ojoch.panelText.text = "Zmatek";
                ojoch.odpocet = 3;
                ojoch.animator.SetTrigger("bad");
                //Inverze
                ojoch.InversionControlling();
                ojoch.invertTime = 10;
                break;


            //smetak + smetak 
            //Nesmrtelnost po určitou dobu - kolem ojocha budou rotovat 2 smetáky
            case 22:
                SoundScript.instance.PlaySingle(good);
                ojoch.panelText.text = "Koštění";
                ojoch.odpocet = 3;
                //ojoch.CollisionDisable(false);
                ojoch.godMode = 5;
                ojoch.animator.SetTrigger("good");

                var smet = Instantiate(smetacek) as Transform;
                smet.position = transform.position + new Vector3(0.2f, 0.2f, 0);
                smet.parent = ojoch.transform;
                break;


            //smetak + koreni
            //AK - na nejakou dobu - vyssi ucinnost zbrane
            case 31:
                ojoch.panelText.text = "AK-47";
                ojoch.odpocet = 3;

                ojoch.isAkacko = true;
                ojoch.akacko = 10;
                ojoch.animator.SetTrigger("good");
                ojoch.animator.SetBool("isAk47", true);
                break;


            //koreni + koreni
            case 40:
                ojoch.panelText.text = "Cake is Lie!";
                ojoch.odpocet = 3;
                break;               

        }      

    }

}
///<summary>
/// Věci, které se nevyužijí, ale jen por případ je tu nechávám
/// Opusť ten hejt!
///</summary>
/*
//Privedení efektu po sebrání 1 powerupu
public void PowerEffect(int id)
{
    switch (id)
    {
        //Bublinky
        case 1:
            health.Damage(-5);
            ojoch.healthSlider.value = health.hp;
            break;

        //LP
        case 3:
            break;

        //ponozky
        case 8:
            break;

        //smetak
        case 11:
            break;

        //koreni
        case 20:
            break;
    }

}
*/