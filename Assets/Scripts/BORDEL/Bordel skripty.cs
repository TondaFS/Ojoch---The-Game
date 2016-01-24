using UnityEngine;
using System.Collections;

public class Bordelskripty : MonoBehaviour {	
}

/*
    SCORE Z OJOCHA      
      
    //promenne na score

    public Text scoreText;
    public Text modi;                   
    public float modifikatorScore = 1;              //Modfifikator
    public float tmpscore;                          //hracovo skore
    public float scorePerSecond = 0;                //pro zvyseni skore za kazdou vterinu  
    public int killedEnemies;                       //pocet zabitych nepratel
    public float tenSecondsTimer = 0;               //Timer na vynulovani modifikatoru skore
    public Slider tenSecondsSlider;
    public GameObject tenSecondsObject;


    //Co muze pryc
        scorePerSecond = 1;
        tmpscore = 0;
        killedEnemies = 0;
        tenSecondsObject.SetActive(false);
      
    /// <summary>
        /// Co by mohlo z Ojocha pryc
        /// </summary> 

        //Skore
        this.scoreText.text = "Skóre: " + tmpscore;
        if (scorePerSecond <= 0) {
            tmpscore += 1 * modifikatorScore;
            scorePerSecond = 1;
        }
        scorePerSecond -= Time.deltaTime;  

        if(modifikatorScore < 1)
        {
            modifikatorScore = 1;
        }
        this.modi.text = "Modifikátor: " + modifikatorScore + "x";

        if (killedEnemies == 3)
        {
            modifikatorScore += 1;
            killedEnemies = 0;
        }

        if (modifikatorScore > 9)
        {
            modifikatorScore = 9;
        }

        if (tenSecondsTimer > 0)
        {
            
            tenSecondsTimer -= Time.deltaTime;
            tenSecondsSlider.value = tenSecondsTimer;
            if(tenSecondsTimer <= 0)
            {
                tenSecondsObject.SetActive(false);
                modifikatorScore = 0;
                killedEnemies = 0;

            }
        }      
*/

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

    


        //Aby ojoch nevyletel pryc
        var dist = (transform.position - Camera.main.transform.position).z;

        var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
            Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
            transform.position.z);




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





