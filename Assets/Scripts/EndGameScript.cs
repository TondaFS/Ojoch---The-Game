using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndGameScript : MonoBehaviour {
    
    HealthScript ojochHealth;    
    ScoreScript session;
    public float finalScore;
    public bool sanityLost = false;         //Prisel Ojoch o pricetnost?
    public float distance = 0;              //Urazena vzdalenost (kvuli ukolum)

    public int enemyInSession;
    public int powersInSession;


    public GameObject coins;

    public GameObject newRecord;

    void Start()
    {
        newRecord.SetActive(false);
        ojochHealth = OjochManager.instance.ojochHealth;

        session = GetComponent<ScoreScript>();
        finalScore = 0;

        enemyInSession = 0;        
        powersInSession = 0;       

    }

    void Update()
    {
        distance += Time.deltaTime;
    }

    public void EndGame() {
        session.end = true;     //prestane pocitat skore
        GameObject.Find("ShowPowerUP").SetActive(false);
        FinalScore();
        GetComponent<SessionController>().ojochDead = true;
        GetComponent<SessionController>().deathMenu.SetActive(true);

        if (GameManager.instance.newRecord)
        {
            newRecord.SetActive(true);
        }        

        for (int i = 0; i < 3; i++)
        {
            if (GameManager.instance.GetComponent<TaskManager>().activeTasks[i].completed)
            {
                GetComponent<SessionController>().taskCompletedText.SetActive(true);
                break;
            }
        }

        
        coins.GetComponent<Text>().text = "" + GameManager.instance.GetComponent<CoinsManager>().coins;

        GameManager.instance.GetComponent<GameStatistics>().UpdateStatistics(1, enemyInSession, powersInSession, (int)distance);
        enemyInSession = 0;
        powersInSession = 0;
        GameManager.instance.SaveData();
    }

    //Spocita finalni skore na zaklade hracovy finalni pricetnosti a zkontroluje aktivni ukoly, ktere se cekuji vydzy na knci hry
    void FinalScore() {
        finalScore = session.tmpscore;
        if (ojochHealth.sanity > 3)
        {
            finalScore *= 2;
            session.tmpscore = (int)finalScore;

            //Staistika
            GameManager.instance.GetComponent<GameStatistics>().stats.fullSanity += 1;

            //check tasku
            for (int i = 0; i < 3; i++)
            {
                //Zkontroluje ukol maximalni pricetnosti
                if (GameManager.instance.GetComponent<TaskManager>().activeTasks[i].type == "sanityFull")
                {
                    GameManager.instance.GetComponent<TaskManager>().CheckOnceTask(distance, i);                    
                }
            }         
        }
        else if (ojochHealth.sanity > 2)
        {
            finalScore *= 1.5f;
            session.tmpscore = (int)finalScore;
        }
        else if (ojochHealth.sanity < 1)
        {
            
            finalScore /= 1.5f;
            session.tmpscore = (int)finalScore;

            //Statistika
            GameManager.instance.GetComponent<GameStatistics>().stats.gotMad += 1;

            if (ojochHealth.sanity <= 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    //Zkontroluje ukol ztraty veskere pricetnosti
                    if (GameManager.instance.GetComponent<TaskManager>().activeTasks[i].type == "sanity")
                    {
                        GameManager.instance.GetComponent<TaskManager>().CheckCountingTask(i);
                    }
                }
            }
        }
        session.scoreText.text = "" + finalScore;        
        GetComponent<SessionController>().deathScore.GetComponent<Text>().text = GameManager.instance.languageManager.GetTextValue("Death.Score") + " " + finalScore;
                
        //Kontrola ukolu
        for (int i = 0; i < 3; i++) {
            if (GameManager.instance.GetComponent<TaskManager>().activeTasks[i].type == "score")   
            {
                GameManager.instance.GetComponent<TaskManager>().CheckOnceTask(finalScore, i);
            }
            if (GameManager.instance.GetComponent<TaskManager>().activeTasks[i].type == "kill")
            {
                GameManager.instance.GetComponent<TaskManager>().CheckCountingTaskAtTheEnd(i, enemyInSession);
            }
            if (GameManager.instance.GetComponent<TaskManager>().activeTasks[i].type == "grab")
            {
                GameManager.instance.GetComponent<TaskManager>().CheckCountingTaskAtTheEnd(i, powersInSession);
            }
            else if (GameManager.instance.GetComponent<TaskManager>().activeTasks[i].type == "distance")
            {
                GameManager.instance.GetComponent<TaskManager>().CheckOnceTask(distance, i);
            }
            else if (GameManager.instance.GetComponent<TaskManager>().activeTasks[i].type == "killRound")
            {
                GameManager.instance.GetComponent<TaskManager>().CheckOnceTask(enemyInSession, i);
                
            } else if(GameManager.instance.GetComponent<TaskManager>().activeTasks[i].type == "modify")
            {
                GameManager.instance.GetComponent<TaskManager>().CheckOnceTask(GameManager.instance.GetComponent<TaskManager>().modifyTmp, i);
                GameManager.instance.GetComponent<TaskManager>().modifyTmp = 0;
            } else if (GameManager.instance.GetComponent<TaskManager>().activeTasks[i].type == "grabRound")
            {
                GameManager.instance.GetComponent<TaskManager>().CheckOnceTask(powersInSession, i);
            }
        }

        //Zkontroluje, jestli neni novy rekord
        GameManager.instance.highscores.CheckScores(finalScore);        
    }

}
