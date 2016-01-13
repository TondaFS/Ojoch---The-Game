using UnityEngine;
using System.Collections;

public class Bordelskripty : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}



/***
                 ---    BORDEL CO SE NEVYUŽIL   ---
                           Z OJOCH SKRIPTU
   
   *************************
   NECO S POHYBEM
   *************************

    void FixedUpdate() {
       GetComponent<Rigidbody2D>().velocity = movement; //Aplikace pohybu na objekt
    }

    //powerCombo.PowerEffect(collision.gameObject.GetComponent<PowerUpID>().powerUpID);     //efekt powerUpu



    *********************
    VECI S ROTACI
    *********************  
    
        public float vterina = 0;                   //vterina
        public float rotace = 10;                   //rychlost nezavisle rotace


        //Ocekuje, jestli je natoceni  na 0, pokud ne, zacne aplikovat rotaci v danem smeru
        if (transform.rotation.z <= 0 && transform.rotation.z >= -0.8)
        {
            transform.Rotate(0, 0, -rotace * Time.deltaTime + transform.rotation.z*0.8f);
        }
        else if (transform.rotation.z > 0 && transform.rotation.z <= 0.8) {
            transform.Rotate(0, 0, rotace * Time.deltaTime + transform.rotation.z*0.8f);
        }

        //Ocekuje jaka je rotace a pripadne odebere palivo
        if (transform.rotation.z <= -0.5 || transform.rotation.z >= 0.5)
        {
            if (vterina > 0)
            {
                vterina -= Time.deltaTime;
            }
            else
            {
                playerHealth.Damage(2);
                healthSlider.value = playerHealth.hp;
                vterina = 1;
            }
        }

    
        //Balancovani
        float rotation = Input.GetAxis("Rotation");                 //* (isInverted ? -1 : 1)
        if (rotation < 0 && transform.rotation.z >= -0.8) {
            transform.Rotate(0, 0, rotation * 1.5f);
        }
        if (rotation >= 0 && transform.rotation.z <= 0.8) {
            transform.Rotate(0, 0, rotation * 1.5f);
        }

        
            //orotuje ojocha / pri kolizi
            if (transform.rotation.z < 0)
                transform.Rotate(0, 0, Random.Range(-40, -30));
            else
                transform.Rotate(0, 0, Random.Range(30, 40));


        --------------------------------------------------------------------------------------

        
        //Metoda pro zapnuti/vynuti BoxCollideru - nesmrtelnost

        public void CollisionDisable(bool enableGod) {
            this.GetComponent<BoxCollider2D>().enabled = enableGod;
        }        
     
    
    
    POWERUPS SCRIPT


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





