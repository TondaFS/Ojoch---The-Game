using UnityEngine;
using System.Collections;

public class EnemyCollisions : MonoBehaviour {
    /// <summary>
    /// reference na CommonAI script, abychom nemuseli používat GetComponent<>() pokaždé, když nepřítele zraníme
    /// </summary>
    CommonAI commonAIRef;

    void Start()
    {
        commonAIRef = GetComponent<CommonAI>();
    }

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
    
    /// <summary>
    /// Funkce zajistí všechny náležitosti, když nepřítel umře.
    /// <para>Vypnutí Collideru nepřítele, spuštění animace smrti, přehrání zvuku smrti a nakonec zničení nepřítele.</para>
    /// </summary>
    /// <param name="col">Objekt, co způsobí nepříteli smrt.</param>
    public void EnemyDamage(GameObject col)
    {
        Debug.Log("Enemy die");
        col.GetComponent<Collider2D>().enabled = false;
        col.GetComponent<Animator>().SetTrigger("bDeath");
        GameManager.instance.GetComponent<SoundManager>().PlaySound(GameManager.instance.GetComponent<SoundManager>().clipEnemyHit);
    }

    /// <summary>
    /// Nepřítelova kolize s Ojochovou (ne nepřátelskou) střelou.
    /// <para>Dá nepříteli zranění a zničí střelu.</para>
    /// </summary>
    /// <param name="col">Střela</param>
    public void ProjectileEnemyCollision(Collider2D col)
    {
        if (!col.gameObject.GetComponent<ShotScript>().isEnemyShot)
        {
            commonAIRef.EnemyDamage(col.gameObject.GetComponent<ShotScript>().damage);
            Destroy(col.gameObject);
        }
    }
}
