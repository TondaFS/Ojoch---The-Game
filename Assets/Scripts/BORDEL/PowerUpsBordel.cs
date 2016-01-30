using UnityEngine;
using System.Collections;

public class PowerUpsBordel : MonoBehaviour {
    //Slowtime
    //public float timeSlow = 0;                      //jak dlouho bude zpomaleny cas

    /*
    //Zakaleni
    public GameObject souflEffect;
    public float zakaleniTime = 0;
    private float zakaleniFade = 0;*/
	
	void Update () {
        /*
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
            }*/

        /*
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
        }*/
    }
}

//POWER COMBO FUNKCE
/*
            /// <summary>
            /// Prďák: LP + LP
            /// Vynuluje modifikátor skóre a nastaví inverzní ovládání
            /// </summary>

            case 6:
                ShowPowerUpText("Prďák", true);
                ojoch.contraBubles = true;
                contraTime = 10;
                effects.prdak.SetActive(true);
                break;  

            /// <summary>
            /// NITRO: LP + Koreni
            /// Zpomali pohyb vseho a zaroven zpomali celkovou uroven zrychleni + 5s nesmrtelnsot          
            /// </summary>

            case 23:

                ShowPowerUpText("Šoufl", false);
                zakaleniTime = 5;
                //effects.soufl.SetActive(true);
                break;
                /*
                ShowPowerUpText("NITRO", true);

                ojoch.godMode = 5;
                effects.smradostit.SetActive(true);
                GameObject.Find("sprite").GetComponent<ColorChanger>().active = true;

                sessionController.GetComponent<SessionController>().speedUpTime += 4;
                sessionController.GetComponent<SessionController>().gameSpeed -= 1f;
                socha.GetComponent<StatueControler>().howMuchForward = 0;
                socha.GetComponent<StatueControler>().howMuchBack = 2f;                
                break;

            

            /// <summary>
            /// SMRADOŠTÍT: Mýdlo + LP
            /// Nesmrtelnost (shield)
            /// </summary> 

            case 4:
                ShowPowerUpText("Smradoštít", true);
                ojoch.godMode = 5;
                effects.smradostit.SetActive(true);
                GameObject.Find("sprite").GetComponent<ColorChanger>().active = true;
                break;


            /// <summary>
            /// PÁN ČASU: LP + Ponozka
            /// Zpomalení času
            /// </summary>  

            case 11:
                ShowPowerUpText("Pán času", true);
                odpocet = 1;
                SlowTime(true);                
                //effects.slowtime.SetActive(true);                
                break;
                */



/*
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
}*/
