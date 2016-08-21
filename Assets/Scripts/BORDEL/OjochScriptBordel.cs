using UnityEngine;
using System.Collections;

public class OjochScriptBordel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}


/*
    //Kolize 
    void OnCollisionEnter2D(Collision2D collision) {

        //S nepritelem -> ubere zivot a nepritele znici
        if ((collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Boss") && (godMode <= 0)) {

            //Posunuti sochy kupredu
            socha.GetComponent<StatueControler>().howMuchForward += 0.75f;
            socha.GetComponent<StatueControler>().howMuchBack = 0;

            session.modifikatorScore -= 1;
            managerSound.PlayRandom(managerSound.clipDamage1, managerSound.clipDamage2);
            animator.SetTrigger("hit");

            //Pokud se nejedna o Bosse, je nepritel znicen, prehrana animace, a zvuk narazeni do nepritele
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<Collider2D>().enabled = false;
                collision.gameObject.GetComponent<Animator>().SetTrigger("bDeath");
                GameManager.instance.GetComponent<SoundManager>().PlaySound(GameManager.instance.GetComponent<SoundManager>().clipEnemyHit);
                Destroy(collision.gameObject, 0.5f);
            }

            //Ojochovi da zraneni
            if (playerHealth != null) {
                playerHealth.Damage(1);
            }
        }

        //Pokud je srazka a Ojoch je nesmrtelny, zvuk srazky a upravi se skore a podobne
        else if ((collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Boss") && ( godMode != 0)) 
        {
            collision.gameObject.GetComponent<Collider2D>().enabled = false;
            collision.gameObject.GetComponent<Animator>().SetTrigger("bDeath");
            GameManager.instance.GetComponent<SoundManager>().PlaySound(GameManager.instance.GetComponent<SoundManager>().clipEnemyHit);
            Destroy(collision.gameObject, 0.5f);

            session.AdjustScore(50);
            session.killedEnemies += 1;
            session.FiveSecondsTimer();            
        }

        
        //S prekazkou -> ubere zivot a ucini na 3 vterin Ojocha nesmrtelnym
        if (collision.gameObject.tag == "Obstacle")
        {
            //Posunuti sochy
            socha.GetComponent<StatueControler>().howMuchForward += 1;
            socha.GetComponent<StatueControler>().howMuchBack = 0;
            session.modifikatorScore -= 2;

            //Ojoch neni mrty a neni nesmrtelny -> dostane zraneni a prehraje zvuk zraneni
            if (playerHealth != null || godMode == 0)
            {
                playerHealth.Damage(1);
                managerSound.PlayRandom(managerSound.clipDamage1, managerSound.clipDamage2);
            }

            collision.gameObject.GetComponent<ObstacleDestruction>().Destruction();
            godMode = 3;
        }
    

        //Se sochou -> Da zraneni, prehraje se animace a zvuk, Ojocha to odhcodi kupredu
        if (collision.gameObject.tag == "Socha")
        {
            playerHealth.Damage(1);
            managerSound.PlayRandom(managerSound.clipDamage1, managerSound.clipDamage2);
            GameObject.Find("Statue Sprite").GetComponent<Animator>().SetTrigger("hit");
            if (playerHealth.hp > 0)
            {
                animator.SetTrigger("isBack");                
            }     
                   
            session.modifikatorScore -= 3;     
            push = 0.25f;
        }

        //S powerUpem -> Zvysi pocet sebranych predmety a pokud je sbran jiz druhy , provede kombo }powerUp/Down]
        if (collision.gameObject.tag == "PowerUp") {
            session.FiveSecondsTimer();                            
            managerSound.PlaySound(managerSound.clipGrab);      //zvuk sebrani
            session.AdjustScore(25);                                                                 //Zapocitani skore 
            powerCombo.powerUps += 1;                                                               //zvyseni powerUpu
            powerCombo.powerUpCombo += collision.gameObject.GetComponent<PowerUpID>().powerUpID;    //pridani ID   
            
            //zobrazeni sebraneho predmetu         
            collect.showObject(collision.gameObject.GetComponent<PowerUpID>().powerUpID, powerCombo.powerUps, powerCombo.powerUpCombo);
            
            //pokud je sebran jiz druhy powerUp -> provede se kombo
            if (powerCombo.powerUps == 2) {
                session.modifikatorScore += 1;
                powerCombo.PowerCombo(powerCombo.powerUpCombo);                                     //provedeni komba
                powerCombo.powerUps = 0;                                                            //nastaveni poctu powerupu na nula
                powerCombo.powerUpCombo = 0;                                                        //vymazani komba a priprava na dalsi                
            }
            
            Destroy(collision.gameObject);
        }
    }    
    */

