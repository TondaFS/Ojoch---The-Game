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

	
}
