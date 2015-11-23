using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
///  Životy a zranění
/// </summary>

public class HealthScript : MonoBehaviour {

    //Promenne
    public int hp = 1;              //pocet zivotu
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
                Time.timeScale = 0.1f;                
                Destroy(gameObject);

            }
        }
    }

    // Započítání zranení a kontrola, jestli nemá být objekt zničen
    public void Damage(int damageCount) {
        hp -= damageCount;

        if (hp <= 0 && gameObject.tag != "Player") {        //pouze pro nepratele
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