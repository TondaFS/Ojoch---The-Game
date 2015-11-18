﻿using UnityEngine;
using System.Collections;

public class PowerUpScript : MonoBehaviour {
    public int powerUps = 0;        //sebrano powerUpu
    public int powerUpCombo = 0;    //jake combo bude
    private HealthScript health;
    private OjochScript ojoch;


    void Start() {
        health = this.GetComponent<HealthScript>();
        ojoch = this.GetComponent<OjochScript>();
    }

    //Privedení efektu po sebrání 1 powerupu
    public void PowerEffect(int id) {
        switch(id){
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
            case  11:
                break;

            //koreni
            case 20:
                break;
        }

    }

    //provedení komba po sebrání 2 powerupů
    public void PowerCombo(int combo) {
        switch (combo)
        {
            //bublinky + bublinky
            case 2:                
                health.Damage(-20);
                ojoch.healthSlider.value = health.hp;
                ojoch.panelText.text = "Bublinace!";
                ojoch.odpocet = 3;
                break;

            //bublinky + LP
            case 4:
                ojoch.panelText.text = "Bublinové LP!";
                ojoch.odpocet = 3;
                break;

            //bublinky + ponozky
            case 9:
                ojoch.panelText.text = "Čisté ponožky!";
                ojoch.odpocet = 3;
                break;

            //bublinky + smetak
            case 12:
                ojoch.panelText.text = "Bulbinky a smeták!";
                ojoch.odpocet = 3;
                break;

            //bublinky + koreni
            case 21:
                ojoch.panelText.text = "Bublinove koreni!";
                ojoch.odpocet = 3;
                break;

            //lp + lp
            case 6:
                ojoch.panelText.text = "SLOWTIME!";
                ojoch.odpocet = 3;
                ojoch.SlowTime(true);
                break;

            //lp + ponozky
            case 11:
                ojoch.panelText.text = "LP s ponozkama!";
                ojoch.odpocet = 3;
                break;

            //lp + smetak
            case 14:
                ojoch.panelText.text = "Lp a smetak!";
                ojoch.odpocet = 3;
                break;

            //lp + koreni
            case 23:
                ojoch.panelText.text = "Lp a koreni!";
                ojoch.odpocet = 3;

                
                break;

            //ponozky + ponozky
            case 16:
                ojoch.panelText.text = "Double ponozky!";
                ojoch.odpocet = 3;
                break;

            //ponozky + smetak
            case 19:
                ojoch.panelText.text = "Smetakove ponozky!";
                ojoch.odpocet = 3;
                break;

            //ponozky + koreni
            case 28:
                ojoch.panelText.text = "Inverze!";
                ojoch.odpocet = 3;

                //Inverzni ovladani
                ojoch.InversionControlling();
                ojoch.invertTime = 10;
                break;

            //smetak + smetak
            case 22:
                ojoch.panelText.text = "Smetaky";
                ojoch.odpocet = 3;
                break;

            //smetak + koreni
            case 31:
                ojoch.panelText.text = "Koreneny smetak!";
                ojoch.odpocet = 3;
                break;

            //koreni + koreni
            case 40:
                ojoch.panelText.text = "Hodne koreni!";
                ojoch.odpocet = 3;
                break;
                

        }
       

    }

}
