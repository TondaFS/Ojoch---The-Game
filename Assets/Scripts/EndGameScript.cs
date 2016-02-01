using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndGameScript : MonoBehaviour {
    
    HealthScript ojochHealth;    
    ScoreScript session;
    public float finalScore;
    public bool sanityLost = false;         //Prisel Ojoch o pricetnost?
    public float distance = 0;              //Urazena vzdalenost (kvuli ukolum)

    void Start()
    {
        ojochHealth = GameObject.FindWithTag("Player").GetComponent<HealthScript>();
        session = GameObject.Find("Session Controller").GetComponent<ScoreScript>();
        finalScore = 0;
    }        
    
    void Update()
    {
        distance += Time.deltaTime;
    }

    public void EndGame() {
        session.end = true;     //prestane pocitat skore
        FinalScore();
        GetComponent<SessionController>().ojochDead = true;
        GetComponent<SessionController>().deathMenu.SetActive(true);
        if (GameManager.instance.newRecord)
        {
            GetComponent<SessionController>().newHighScoreText.SetActive(true);
        }
        for(int i = 0; i < 3; i++)
        {
            if (GameManager.instance.GetComponent<TaskManager>().activeTasks[i].completed)
            {
                GetComponent<SessionController>().taskCompletedText.SetActive(true);
                break;
            }
        }
    }

    //Spocita finalni skore na zaklade hracovy finalni pricetnosti a zkontroluje aktivni ukoly, ktere se cekuji vydzy na knci hry
    void FinalScore() {
        finalScore = session.tmpscore;
        if (ojochHealth.sanity > 3)
        {
            finalScore *= 3;
            session.tmpscore = (int)finalScore;

            //check tasku
            for (int i = 0; i < 3; i++)
            {
                //Zkontroluje ukol maximalni pricetnosti
                if (GameManager.instance.GetComponent<TaskManager>().activeTasks[i].type == "sanityFull")
                {
                    GameManager.instance.GetComponent<TaskManager>().CheckCountingTask(i);
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
            
            finalScore /= 2;
            session.tmpscore = (int)finalScore;

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

        //Kontrola ukolu
        for (int i = 0; i < 3; i++) {
            if (GameManager.instance.GetComponent<TaskManager>().activeTasks[i].type == "score")   
            {
                GameManager.instance.GetComponent<TaskManager>().CheckOnceTask(finalScore, i);
            }
            else if (GameManager.instance.GetComponent<TaskManager>().activeTasks[i].type == "play")
            {
                GameManager.instance.GetComponent<TaskManager>().CheckCountingTask(i);
            }
            else if (GameManager.instance.GetComponent<TaskManager>().activeTasks[i].type == "distance")
            {
                GameManager.instance.GetComponent<TaskManager>().CheckOnceTask(distance, i);
            }
            else if (GameManager.instance.GetComponent<TaskManager>().activeTasks[i].type == "killRound")
            {
                GameManager.instance.GetComponent<TaskManager>().CheckOnceTask(GameManager.instance.GetComponent<TaskManager>().killsPerGame, i);
                GameManager.instance.GetComponent<TaskManager>().killsPerGame = 0;
            } else if(GameManager.instance.GetComponent<TaskManager>().activeTasks[i].type == "modify")
            {
                GameManager.instance.GetComponent<TaskManager>().CheckOnceTask(GameManager.instance.GetComponent<TaskManager>().modifyTmp, i);
                GameManager.instance.GetComponent<TaskManager>().modifyTmp = 0;
            } else if (GameManager.instance.GetComponent<TaskManager>().activeTasks[i].type == "grabRound")
            {
                GameManager.instance.GetComponent<TaskManager>().CheckOnceTask(GameManager.instance.GetComponent<TaskManager>().grabsPerGame, i);
                GameManager.instance.GetComponent<TaskManager>().grabsPerGame = 0;
            }
        }

        //Zkontroluje, jestli neni novy rekord
        GameManager.instance.highscores.CheckScores(finalScore);        
    }    

    
}
