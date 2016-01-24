using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndGameScript : MonoBehaviour {
    HealthScript ojochHealth;    
    ScoreScript session;
    public float finalScore;


    void Start () {
        ojochHealth = GameObject.FindWithTag("Player").GetComponent<HealthScript>();
        session = GameObject.Find("Session Controller").GetComponent<ScoreScript>();
        finalScore = 0;
    }	

    public void EndGame() {
        GetComponent<SessionController>().ojochDead = true;        
        GameObject.Find("PanelText").GetComponent<Text>().text = "GameOver!";        
        FinalScore();
    }

    void FinalScore() {
        finalScore = session.tmpscore;
        if (ojochHealth.sanity > 25)
        {
            finalScore *= 3;
            session.tmpscore = finalScore;            
        }
        else if (ojochHealth.sanity > 15)
        {
            finalScore *= 1.5f;
            session.tmpscore = finalScore;
        }
        else if (ojochHealth.sanity < 5)
        {
            finalScore /= 2;
            session.tmpscore = finalScore;
        }
        
        if(GameManager.instance.GetComponent<TaskManager>().activeTask.type == "score")
        {
            GameManager.instance.GetComponent<TaskManager>().CheckScoreTask(finalScore);
        } else if (GameManager.instance.GetComponent<TaskManager>().activeTask.type == "play")
        {
            GameManager.instance.GetComponent<TaskManager>().CheckCountingTask();
        }

        GameManager.instance.highscores.CheckScores(finalScore);        
    }    
}
