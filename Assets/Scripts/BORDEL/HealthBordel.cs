using UnityEngine;
using System.Collections;

public class HealthBordel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

/*
    //Kolize se střelou
    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();
        if (shot != null) {
            if (shot.isEnemyShot != isEnemy) {      //Jedna se o mou strelu?
                                
                if (gameObject.tag == "Player")
                {
                    //Pokud je hrac neni nesmrtelny, strely mu budou davat zraneni a prehraje se zvuk zraneni
                    if (gameObject.GetComponent<OjochScript>().godMode <= 0)
                    {
                        Damage(shot.damage);
                        GameManager.instance.GetComponent<SoundManager>().PlayRandom(GameManager.instance.GetComponent<SoundManager>().clipDamage1, GameManager.instance.GetComponent<SoundManager>().clipDamage2);
                        gameObject.GetComponent<OjochScript>().animator.SetTrigger("hit");
                    }                    
                }
                else
                {                    
                    Damage(shot.damage);                //dani zraneni
                }                          
                //shot.gameObject.GetComponentInChildren<Animator>().SetTrigger("shooted");
                //shot.GetComponent<Collider2D>().enabled = false;
                Destroy(shot.gameObject);           //zniceni strely
            }
            
        }
    }

    //Když nepřítel narazí do sochy, zničí se
    void OnCollisionEnter2D(Collision2D otherCollider)
    {

        if (otherCollider.gameObject.tag == "Socha" && (gameObject.tag == "Enemy" || gameObject.tag == "Boss"))
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<Animator>().SetTrigger("bDeath");
            GameManager.instance.GetComponent<SoundManager>().PlaySound(GameManager.instance.GetComponent<SoundManager>().clipEnemyHit);
            Destroy(gameObject, 0.5f);
        }
    }
    */
