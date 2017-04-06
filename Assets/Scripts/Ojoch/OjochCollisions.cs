using UnityEngine;
using System.Collections;

/// <summary>
/// Skript řeší veškeré Ojochovy kolize s nepřáteli a objekty ve hře.
/// </summary>
public class OjochCollisions : MonoBehaviour {
    /// <summary>
    /// Reference na sochu
    /// </summary>
    private GameObject socha;  
    /// <summary>
    /// Reference na ScoreScript
    /// </summary>                     
    public ScoreScript session;
    /// <summary>
    /// Reference na PowerUpScript
    /// </summary>
    public PowerUpScript powerCombo;     
    /// <summary>
    /// Jak dlouho se bude socha pohybovat dopředu oté co dostane Ojoch zranění.
    /// </summary>               
    public float statueForward = 0.75f;
    
    void Start () {
        socha = GameObject.Find("statue");
        session = GameObject.Find("Session Controller").GetComponent<ScoreScript>();
        powerCombo = GetComponent<PowerUpScript>();
    }
        
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            EnemyCollision(col);
        }

        if (col.gameObject.tag == "Boss")
        {
            BossCollision(col);            
        }

        if (col.gameObject.tag == "Socha")
        {
            StatueCollision(col);
        }

        if (col.gameObject.tag == "PowerUp")
        {
            PowerUpCollision(col);
        }    
        
       if (col.gameObject.tag == "coin")
        {
            CoinCollision(col);
        }
        
    } 

    void OnTriggerEnter2D(Collider2D col)
    {        
        if (col.gameObject.tag == "Projectile")
        {
            ProjectileCollision(col);         
        }

        if (col.gameObject.tag == "Laser")
        {
            Debug.Log("smth");
            LaserCollision(col);
        }
    }
    
    /// <summary>
    /// Funkce provede všechny náležitosti, které mají po zranění Ojocha nastat.
    /// Tedy: Přidání zranění, přehrání zvuku, posunutí Sochy dopředu (pokud je třeba), upravení skóre  
    /// </summary>
    /// <param name="modifier">Hodnota úpravy modifikátoru</param>
    /// <param name="statue">Narazil Ojoch do sochy?</param>
    void OjochDamage(int modifier, bool statue)
    {
        OjochManager.instance.ojochScript.PlayDamageSound();        
        OjochManager.instance.ojochHealth.Damage(1);

        if (statue)
        {
            OjochManager.instance.ojochScript.animator.SetTrigger("isBack");
            OjochManager.instance.ojochScript.push = 0.25f;
        }
        else
        {
            socha.GetComponent<StatueControler>().howMuchForward += statueForward;
            socha.GetComponent<StatueControler>().howMuchBack = 0;
            OjochManager.instance.ojochScript.animator.SetTrigger("hit");
        }

        session.UpdateScoreStuff(0, modifier, 0, false);
    }

    /// <summary>
    /// Ojochova kolize s nepřítelem.
    /// <para>Pokud není Ojoch nesmrtelný, dá mu zranění. V opačném případě pouze upraví skóre a modifikátor. V obou případech je nepřítel zničen.</para>
    /// </summary>
    /// <param name="col">Nepřítel</param>
    void EnemyCollision(Collision2D col)
    {
        col.gameObject.GetComponent<EnemyCollisions>().EnemyDamage(col.gameObject);
        if (OjochManager.instance.ojochScript.godMode <= 0)
        {
            OjochDamage(-1, false);
        }
        else
        {
            session.UpdateScoreStuff(50, 0, 1, true);
        }        
    }

    /// <summary>
    /// Ojochova kolize s Bossem
    /// <para>Pokud není Ojoch nesmrtelný, dá mu zranění. V opačném případě upraví skóre a modifikátor a zničí nepřítele.</para>
    /// </summary>
    /// <param name="col">Boss</param>
    void BossCollision(Collision2D col)
    {
        if (OjochManager.instance.ojochScript.godMode <= 0)
        {
            OjochDamage(-1, false);
        }
        else
        {
            session.UpdateScoreStuff(100, 1, 1, true);
            col.gameObject.GetComponent<EnemyCollisions>().EnemyDamage(col.gameObject);
        }        
    }

    /// <summary>
    /// Ojochova kolize se Sochou.
    /// <para>Spustí animaci sochy a dá Ojochovi zranění.</para>
    /// </summary>
    /// <param name="col">Socha</param>
    void StatueCollision(Collision2D col)
    {
        GameObject.Find("Statue Sprite").GetComponent<Animator>().SetTrigger("hit");
        OjochDamage(-3, true);
    }

    /// <summary>
    /// Ojochova kolize s předmětem.
    /// <para>Přehraje Ojochův zvuk sebrání, zvýší počet sebraných předmětů a přičte ID powerUpu do komba.
    /// Sebraný předmět je pak zobrazen a pokud došlo k sebrání již druhého, vznikne PowerUp/Down.
    /// Nakonec upraví skóre a předmět zničí.</para>
    /// </summary>
    /// <param name="col">Předmět</param>
    void PowerUpCollision(Collision2D col)
    {        
        int powerUpCount = 0;               //slouží pouze v případě, že vznikl PowerUp a tím pádem se pak zvýší modifikátor skóre

        GameManager.instance.GetComponent<SoundManager>().PlaySound(GameManager.instance.GetComponent<SoundManager>().clipGrab);

        powerCombo.powerUps += 1;
        powerCombo.powerUpCombo += col.gameObject.GetComponent<PowerUpID>().powerUpID;

        OjochManager.instance.ojochCollect.showObject(col.gameObject.GetComponent<PowerUpID>().powerUpID, powerCombo.powerUps, powerCombo.powerUpCombo);

        if (powerCombo.powerUps == 2)
        {
            powerUpCount += 1;
            powerCombo.PowerCombo(powerCombo.powerUpCombo);
            powerCombo.powerUps = 0;
            powerCombo.powerUpCombo = 0;
        }

        session.UpdateScoreStuff(25, powerUpCount, 0, true);
        Destroy(col.gameObject);
    }

    /// <summary>
    /// Ojochova kolize se střelou.
    /// <para>Pokud není Ojoch nesmrtelný a jedná se o nepřátelskou střelu, přehraje zvuk zranění, spustí animaci, dá zranění, upraví skóre a nakonec střelu zničí.</para>
    /// </summary>
    /// <param name="col">Střela</param>
    void ProjectileCollision(Collider2D col)
    {
        if (col.gameObject.GetComponent<ShotScript>().isEnemyShot && OjochManager.instance.ojochScript.godMode <= 0)
        {
            OjochManager.instance.ojochScript.PlayDamageSound();
            OjochManager.instance.ojochScript.animator.SetTrigger("hit");
            OjochManager.instance.ojochHealth.Damage(1);
            session.UpdateScoreStuff(0, -1, 0, false);

            Destroy(col.gameObject);
        }
    }

    /// <summary>
    /// Ojochova kolize s mincí
    /// <para>
    /// Přidá jednu minci
    /// </para>
    /// </summary>
    /// <param name="col">Mince</param>
    void CoinCollision(Collision2D col)
    {
        GameManager.instance.GetComponent<CoinsManager>().AjustCoins(col.gameObject.GetComponent<Coins>().value);
        OjochManager.instance.ojochScript.animator.SetTrigger("good");
        Destroy(col.gameObject);
    }

    /// <summary>
    /// Ojochova kolize s laserem. 
    /// Pokud není Ojoch nesmrtelný a jedná se o nepřátelskou střelu, přehraje zvuk zranění, spustí animaci, dá zranění, upraví skóre
    /// </summary>
    /// <param name="col"></param>
    void LaserCollision(Collider2D col)
    {
        if(OjochManager.instance.ojochScript.godMode <= 0)
        {
            OjochManager.instance.ojochScript.PlayDamageSound();
            OjochManager.instance.ojochScript.animator.SetTrigger("hit");
            OjochManager.instance.ojochHealth.Damage(1);
            session.UpdateScoreStuff(0, -1, 0, false);
        }
    }

}
