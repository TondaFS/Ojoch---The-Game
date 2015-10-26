using UnityEngine;
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
                break;

            //bublinky + LP
            case 4:
                Debug.Log("Bublinky + LP");
                break;

            //bublinky + ponozky
            case 9:
                Debug.Log("Ciste ponozky");
                break;

            //bublinky + smetak
            case 12:
                Debug.Log("Bublinky + smetak");
                break;

            //bublinky + koreni
            case 21:
                Debug.Log("Bublinky + koreni");
                break;

            //lp + lp
            case 6:
                Debug.Log("Lp + Lp");
                break;

            //lp + ponozky
            case 11:
                Debug.Log("Lp + ponozky");
                break;

            //lp + smetak
            case 14:
                Debug.Log("Lp + smetak");
                break;

            //lp + koreni
            case 23:
                Debug.Log("Lp + koreni");
                break;

            //ponozky + ponozky
            case 16:
                Debug.Log("Ponozky + ponozky");
                break;

            //ponozky + smetak
            case 19:
                Debug.Log("ponozky + smetak");
                break;

            //ponozky + koreni
            case 28:
                Debug.Log("ponozky + koreni");
                break;

            //smetak + smetak
            case 22:
                Debug.Log("smetak + smetak");
                break;

            //smetak + koreni
            case 31:
                Debug.Log("smetak + koreni");
                break;

            //koreni + koreni
            case 40:
                Debug.Log("koreni + koreni");
                break;
                

        }
            
    }

}
