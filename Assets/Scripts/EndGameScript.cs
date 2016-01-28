using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndGameScript : MonoBehaviour {
    HealthScript ojochHealth;    
    ScoreScript session;
    public float finalScore;
    public bool sanityLost = false;
    public float distance = 0;
    


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
        FinalScore();
        GetComponent<SessionController>().ojochDead = true;        
        GameObject.Find("PanelText").GetComponent<Text>().text = "GameOver!"; 
    }

    void FinalScore() {
        finalScore = session.tmpscore;
        if (ojochHealth.sanity > 3)
        {
            finalScore *= 3;
            session.tmpscore = (int)finalScore;

            //check tasku
            for (int i = 0; i < 3; i++)
            {
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
                    if (GameManager.instance.GetComponent<TaskManager>().activeTasks[i].type == "sanity")
                    {
                        GameManager.instance.GetComponent<TaskManager>().CheckCountingTask(i);
                    }
                }
            }
        }
        session.scoreText.text = "" + finalScore;

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

        GameManager.instance.highscores.CheckScores(finalScore);        
    }    
}
