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
            case 1:
                health.Damage(-5);
                ojoch.healthSlider.value = health.hp;
                break;
        }

    }

    //provedení komba po sebrání 2 powerupů
    public void PowerCombo(int combo) {
        switch (combo)
        {
            case 2:                
                health.Damage(-20);
                ojoch.healthSlider.value = health.hp;
                break;

            case 4:
                Debug.Log("Bublinky + LP");
                break;

        }
            
    }

}
