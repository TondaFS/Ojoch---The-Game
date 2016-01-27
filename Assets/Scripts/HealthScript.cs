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

    // Započítání zranení a kontrola, jestli nemá být objekt zničen
    public void Damage(int damageCount) {
        hp -= damageCount;

        if (hp <= 0 && gameObject.tag != "Player") {        //pouze pro nepratele 
            GameManager.instance.GetComponent<SoundManager>().PlaySound(GameManager.instance.GetComponent<SoundManager>().clipEnemyHit);
            gameObject.GetComponent<Animator>().SetTrigger("bDeath");
            Destroy(gameObject, 0.5f);
            ojoch.session.FiveSecondsTimer();
            ojoch.session.killedEnemies += 1;
            ojoch.session.AdjustScore(10);

            //Čekování na úkoly
            for (int i = 0; i < 3; i++)
            {
                if ((GameManager.instance.GetComponent<TaskManager>().activeTasks[i].type == "kill") && (GameManager.instance.GetComponent<TaskManager>().activeTasks[i].completed != true))
                {
                    GameManager.instance.GetComponent<TaskManager>().CheckCountingTask(i);
                }
                else if (GameManager.instance.GetComponent<TaskManager>().activeTasks[i].type == "killRound") {
                    GameManager.instance.GetComponent<TaskManager>().killsPerGame += 1;
                }
            }
            
        }

        else if (gameObject.tag == "Player")
        {
            if (hp > 100)
            {
                hp = 100;
            }
            if (hp <= 0)
            {
                StartCoroutine(DieOjoch());                
            }
        }
    }

    public void LooseSanity(int damage) {
        ojoch.sanityBar.SetActive(true);
        sanity -= damage;        
        ojoch.sanitySlider.value = sanity;

        if(sanity <= 0)
        {
            sanity = 0;
            GameObject.Find("Session Controller").GetComponent<EndGameScript>().sanityLost = true;
        }
    }

    private IEnumerator DieOjoch()
    {
        ojoch.animator.SetTrigger("dead");
        yield return new WaitForSeconds(0.75f);
        GameObject.Find("Session Controller").GetComponent<EndGameScript>().EndGame();
        ojoch.powerCombo.powerUpImage.SetActive(true);
        Time.timeScale = 0.1f;
        Destroy(gameObject);
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

    void OnCollisionEnter2D(Collision2D otherCollider)
    {

        if (otherCollider.gameObject.tag == "Socha" && gameObject.tag == "Enemy")
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<Animator>().SetTrigger("bDeath");
            Destroy(gameObject, 0.5f);
        }
    }
}