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
            ProjectileEnemyCollision(col);                       
        }
    }

    //Vypne nepriteli kolize, prehraje smrt a znici jej
    /// <summary>
    /// Funkce zajistí všechny náležitosti, když nepřítel umře.
    /// <para>Vypnutí Collideru nepřítele, spuštění animace smrti, přehrání zvuku smrti a nakonec zničení nepřítele.</para>
    /// </summary>
    /// <param name="col">Objekt, co způsobí nepříteli smrt.</param>
    public void EnemyDamage(GameObject col)
    {
        col.GetComponent<Collider2D>().enabled = false;
        col.GetComponent<Animator>().SetTrigger("bDeath");
        GameManager.instance.GetComponent<SoundManager>().PlaySound(GameManager.instance.GetComponent<SoundManager>().clipEnemyHit);
        Destroy(col, 0.5f);
    }

    /// <summary>
    /// Nepřítelova kolize s Ojochovou (ne nepřátelskou) střelou.
    /// <para>Dá nepříteli zranění a zničí střelu.</para>
    /// </summary>
    /// <param name="col">Střela</param>
    void ProjectileEnemyCollision(Collider2D col)
    {
        if (!col.gameObject.GetComponent<ShotScript>().isEnemyShot)
        {
            GetComponent<HealthScript>().Damage(col.gameObject.GetComponent<ShotScript>().damage);
            Destroy(col.gameObject);
        }
    }
}
