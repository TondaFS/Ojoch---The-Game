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
    public int enemyType;
    private bool dead = false;

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
                if (!dead)
                {
                    EnemyDeathSound(gameObject.GetComponent<HealthScript>().enemyType);
                    dead = true;
                    gameObject.GetComponent<Collider2D>().enabled = false;
                    gameObject.GetComponent<Animator>().SetTrigger("bDeath");
                                        
                    if (!gameObject.GetComponent<EnemyAI>().exploded)
                    {                        
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
                Destroy(gameObject, .5f);  
            }
        }
        
    }

    //Funkce, ktera ojochovi ubira pricetnost a nasledne upravuje pocet mozku ve hre
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

                //Pokud ojoch ztrati veskerou pricetnost, informace o tom se ulozi a  spusti se efekt soufl
                case 0:
                    sanityOne.SetActive(false);
                    
                    GameObject.Find("Session Controller").GetComponent<EndGameScript>().sanityLost = true;
                    GameObject.Find("Session Controller").GetComponent<ShowingEffects>().soufl.SetActive(true);
                    ojoch.zakaleniTime = 1;
                    break;
            }
            
        }
    }

    //Funkce pri smrti Ojocha, kde se nejdrive pocka na prehrani animace a teprve pak se vypne hudba a spusti funkce na konci hry
    private IEnumerator DieOjoch()
    {
        ojoch.animator.SetTrigger("dead");
        yield return new WaitForSeconds(0.75f);
        GameObject.Find("Session Controller").GetComponent<EndGameScript>().EndGame();
        Time.timeScale = 0.1f;
        GameObject.Find("Music").GetComponent<AudioSource>().mute = true;
        Destroy(gameObject);
    }

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

    //Da zraneni ojochovi a podle toho upravi pocet zivotu, pokud dostava bublinaci, vyleci vse
    public void AdjustHealthBar(int damage)
    {        
        bool smth;
        if(damage > 0)
        {
            hp -= damage;
            smth = false;
            ojoch.godMode = 2;
            ojoch.powerCombo.effects.smradostit.SetActive(true);
            GameObject.Find("sprite").GetComponent<ColorChanger>().active = true;
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

    //Jak zemre nepritel, vybere spravny zvuk smrti a prehraje jej
    public void EnemyDeathSound(int enemy)
    {
        switch (enemy)
        {
            case 0:
                GameManager.instance.GetComponent<SoundManager>().PlaySound(GameManager.instance.GetComponent<SoundManager>().birdDeath);
                break;
            case 1:
                GameManager.instance.GetComponent<SoundManager>().PlaySound(GameManager.instance.GetComponent<SoundManager>().squirrelDeath);
                break;
            case 2:
                GameManager.instance.GetComponent<SoundManager>().PlaySound(GameManager.instance.GetComponent<SoundManager>().pokoutnikDeath);
                break;
            case 3:
                GameManager.instance.GetComponent<SoundManager>().PlaySound(GameManager.instance.GetComponent<SoundManager>().ratDeath);
                break;
        }
    }
}
