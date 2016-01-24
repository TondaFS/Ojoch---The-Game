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
    OjochScript ojoch;

    void Start() {
        ojoch = GameObject.FindWithTag("Player").GetComponent<OjochScript>();
    }

    void Update() {
        
        if (gameObject.tag == "Player") {
            if (hp > 100) {
                hp = 100;
            }
            if (hp <= 0) {
                Time.timeScale = 0.1f;
                GameObject.Find("Session Controller").GetComponent<EndGameScript>().EndGame();
                ojoch.powerCombo.powerUpImage.SetActive(true);         
                Destroy(gameObject);
            }
        }
    }

    // Započítání zranení a kontrola, jestli nemá být objekt zničen
    public void Damage(int damageCount) {
        hp -= damageCount;

        if (hp <= 0 && gameObject.tag != "Player") {        //pouze pro nepratele 
            GameManager.instance.GetComponent<SoundManager>().PlaySound(GameManager.instance.GetComponent<SoundManager>().clipEnemyHit);
            Destroy(gameObject);
            ojoch.session.FiveSecondsTimer();
            ojoch.session.killedEnemies += 1;
            ojoch.session.AdjustScore(10);
            if((GameManager.instance.GetComponent<TaskManager>().activeTask.type == "kill") && (GameManager.instance.GetComponent<TaskManager>().activeTask.completed != true)){                
                GameManager.instance.GetComponent<TaskManager>().CheckCountingTask();
            }
            
        }         
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();      

        if (shot != null) {
            if (shot.isEnemyShot != isEnemy) {      //Jedna se o mou strelu?
                Damage(shot.damage);                //dani zraneni
                Destroy(shot.gameObject);           //zniceni strely
            }
            
        }
    }
}