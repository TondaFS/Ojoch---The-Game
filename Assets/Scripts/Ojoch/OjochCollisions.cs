using UnityEngine;
using System.Collections;

public class OjochCollisions : MonoBehaviour {
    private GameObject socha;
    public ScoreScript session;
    public PowerUpScript powerCombo;
    public float statueForward = 0.75f;

    // Use this for initialization
    void Start () {
        socha = GameObject.Find("statue");
        session = GameObject.Find("Session Controller").GetComponent<ScoreScript>();
        powerCombo = GetComponent<PowerUpScript>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            if (OjochManager.instance.ojochScript.godMode <= 0)
            {
                OjochDamage(col, -1, false);
                col.gameObject.GetComponent<EnemyCollisions>().EnemyDamage(col.gameObject);
            }
            else
            {
                session.UpdateScoreStuff(50, 0, 1, true);
                col.gameObject.GetComponent<EnemyCollisions>().EnemyDamage(col.gameObject);
            }
        }

        if (col.gameObject.tag == "Boss")
        {
            if (OjochManager.instance.ojochScript.godMode <= 0)
            {
                OjochDamage(col, -1, false);
            }
            else
            {
                session.UpdateScoreStuff(100, 1, 1, true);
                col.gameObject.GetComponent<EnemyCollisions>().EnemyDamage(col.gameObject);
            }
        }

        if (col.gameObject.tag == "Socha")
        {
            GameObject.Find("Statue Sprite").GetComponent<Animator>().SetTrigger("hit");
            OjochDamage(col, -3, true);
        }

        if (col.gameObject.tag == "PowerUp")
        {
            int powerUpCount = 0;
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
    } 

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Projectile")
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
    }

    //Posune sochu, zrani ojocha, upravi modifikator
    void OjochDamage(Collision2D col, int modifier, bool statue)
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
}
