using UnityEngine;
using System.Collections;

public class EnemyCollisions : MonoBehaviour {
    
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Socha")
        {
            EnemyDamage(gameObject);
        }
    }

	void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Projectile")
        {
            if (!col.gameObject.GetComponent<ShotScript>().isEnemyShot)
            {
                GetComponent<HealthScript>().Damage(col.gameObject.GetComponent<ShotScript>().damage);
                Destroy(col.gameObject);
            }            
        }
    }

    //Vypne nepriteli kolize, prehraje smrt a znici jej
    public void EnemyDamage(GameObject col)
    {
        col.GetComponent<Collider2D>().enabled = false;
        col.GetComponent<Animator>().SetTrigger("bDeath");
        GameManager.instance.GetComponent<SoundManager>().PlaySound(GameManager.instance.GetComponent<SoundManager>().clipEnemyHit);
        Destroy(col, 0.5f);
    }
}
