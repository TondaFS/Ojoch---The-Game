using UnityEngine;
using System.Collections;

/// <summary>
///  Životy a zranění
/// </summary>

public class HealthScript : MonoBehaviour {

    //Promenne
    public int hp = 1;              //pocet zivotu
    public bool isEnemy = true;     //jedna se o hrace/nepritele?
    
    // Započítání zranení a kontrola, jestli nemá být objekt zničen
    public void Damage(int damageCount) {
        hp -= damageCount;

        if (hp <= 0) {
            if (gameObject.tag != "Player")
            {
                Destroy(gameObject);
            }
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
