using UnityEngine;
using System.Collections;

public class EndGameScript : MonoBehaviour {
    HealthScript ojochHealth;
    OjochScript ojochScript;
    float finalScore;


    void Start () {
        ojochHealth = GameObject.FindWithTag("Player").GetComponent<HealthScript>();
        ojochScript = GameObject.FindWithTag("Player").GetComponent<OjochScript>();
        finalScore = 0;
    }	

    public void EndGame() {
        ojochScript.panelText.text = "GameOver!";
        FinalScore();             
    }

    void FinalScore() {
        float finalScore = ojochScript.tmpscore;
        if (ojochHealth.sanity > 25)
        {
            finalScore *= 3;
            ojochScript.scoreText.text = "Skore: " + finalScore;
        }
        else if (ojochHealth.sanity > 15)
        {
            finalScore *= 1.5f;
            ojochScript.scoreText.text = "Skore: " + finalScore;
        }
        else if (ojochHealth.sanity < 5)
        {
            finalScore /= 2;
            ojochScript.scoreText.text = "Skore: " + finalScore;
        }
        
        GameManager.instance.highscores.CheckScores(finalScore);        
    }

    
     
}
