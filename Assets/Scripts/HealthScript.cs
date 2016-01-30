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

    //Ojochovy staty
    [Space(10, order = 0)]
    [Header("OJOCHOVY REFERENCE NA UI", order = 1)]
    [Space(5, order = 3)]
    public GameObject sanityOne;
    public GameObject sanityTwo;
    public GameObject sanityThree;

    public GameObject healthOne;
    public GameObject healthTwo;
    public GameObject healthThree;
    public GameObject healthFour;
    public GameObject healthFive;
    public GameObject healthSix;


    void Start() {
        ojoch = GameObject.FindWithTag("Player").GetComponent<OjochScript>();
    }          

    // Započítání zranení a kontrola, jestli nemá být objekt zničen
    public void Damage(int damageCount)
    {
        if (gameObject.tag == "Player")
        {
            AdjustHealthBar(damageCount);
        }

        else
        {
            hp -= damageCount;
            if (hp <= 0 && (gameObject.tag == "Enemy" || gameObject.tag == "Boss"))
            { 
                GameManager.instance.GetComponent<SoundManager>().PlaySound(GameManager.instance.GetComponent<SoundManager>().clipEnemyHit);
                gameObject.GetComponent<Animator>().SetTrigger("bDeath");
                Destroy(gameObject, .5f);
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
                    else if (GameManager.instance.GetComponent<TaskManager>().activeTasks[i].type == "killRound")
                    {
                        GameManager.instance.GetComponent<TaskManager>().killsPerGame += 1;
                    }
                }

            }
        }
        
    }

    public void LooseSanity(int damage) {
        if (sanity > 0)
        {
            sanity -= damage;
            switch (sanity)
            {
                case 3:
                    ojoch.sanityBar.SetActive(true);
                    break;
                case 2:
                    sanityThree.SetActive(false);
                    break;
                case 1:
                    sanityTwo.SetActive(false);
                    break;
                case 0:
                    sanityOne.SetActive(false);
                    GameObject.Find("Session Controller").GetComponent<EndGameScript>().sanityLost = true;
                    break;
            }
            
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

        if (otherCollider.gameObject.tag == "Socha" && (gameObject.tag == "Enemy" || gameObject.tag == "Boss"))
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<Animator>().SetTrigger("bDeath");
            Destroy(gameObject, 0.5f);
        }
    }

    public void AdjustHealthBar(int damage)
    {        
        bool smth;
        if(damage > 0)
        {
            hp -= damage;
            smth = false;
            switch (hp)
            {
                case 6:
                    damage = 0;
                    break;
                case 5:
                    healthSix.SetActive(smth);
                    break;
                case 4:
                    healthFive.SetActive(smth);
                    break;
                case 3:
                    healthFour.SetActive(smth);
                    break;
                case 2:
                    healthThree.SetActive(smth);
                    break;
                case 1:
                    healthTwo.SetActive(smth);
                    break;
                case 0:
                    healthOne.SetActive(smth);
                    GameManager.instance.GetComponent<SoundManager>().PlaySound(GameManager.instance.GetComponent<SoundManager>().clipOjochDeath);
                    StartCoroutine(DieOjoch());
                    break;
            }
        }
        else
        {
            smth = true;
            hp = 6;
            healthTwo.SetActive(smth);
            healthThree.SetActive(smth);
            healthFour.SetActive(smth);
            healthFive.SetActive(smth);
            healthSix.SetActive(smth);
        }       
    }
}