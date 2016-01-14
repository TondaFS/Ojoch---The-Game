using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
///  Životy a zranění
/// </summary>

public class HealthScript : MonoBehaviour {

    //Promenne
    public int hp = 1;              //pocet zivotu
    public int sanity = 30;         //Pocet pricetnosti          
    public bool isEnemy = true;     //jedna se o hrace/nepritele?
    GameObject ojoch;

    void Start() {
        ojoch = GameObject.FindWithTag("Player");
    }

    void Update() {
        
        if (gameObject.tag == "Player") {
            if (hp > 100) {
                hp = 100;
            }
            if (hp <= 0) {
                ojoch.GetComponent<OjochScript>().panelText.text = "GameOver!";
                float finalScore = ojoch.GetComponent<OjochScript>().tmpscore;
                if (sanity > 25)                {
                    
                    finalScore *= 3;
                    ojoch.GetComponent<OjochScript>().scoreText.text = "Skore: " + finalScore;
                }
                else if(sanity > 15)
                {                    
                    finalScore *= 1.5f;
                    ojoch.GetComponent<OjochScript>().scoreText.text = "Skore: " + finalScore;
                }  
                else if (sanity < 5)
                {                    
                    finalScore /= 2;
                    ojoch.GetComponent<OjochScript>().scoreText.text = "Skore: " + finalScore;
                }

                if (GameManager.instance.highscore < finalScore)
                {
                    GameManager.instance.highscore = (int)finalScore;
                }
                GameManager.instance.SaveData();
                Time.timeScale = 0.1f; 
                Destroy(gameObject);

            }
        }
    }

    // Započítání zranení a kontrola, jestli nemá být objekt zničen
    public void Damage(int damageCount) {
        hp -= damageCount;

        if (hp <= 0 && gameObject.tag != "Player") {        //pouze pro nepratele
            ojoch.GetComponent<OjochScript>().tenSecondsTimer = 5;
            ojoch.GetComponent<OjochScript>().tenSecondsObject.SetActive(true);
            ojoch.GetComponent<OjochScript>().tenSecondsSlider.value = ojoch.GetComponent<OjochScript>().tenSecondsTimer;
            ojoch.GetComponent<OjochScript>().killedEnemies += 1;
            Destroy(gameObject);            
            ojoch.GetComponent<OjochScript>().tmpscore += 10 * ojoch.GetComponent<OjochScript>().modifikatorScore;      //zapocitani skore
            
        }         
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();      

        if (shot != null) {
            if (shot.isEnemyShot != isEnemy) {      //Jedna se o mou strelu?
                Damage(shot.damage);                //ddani zraneni
                Destroy(shot.gameObject);           //zniceni strely
            }

            
        }
    }
}