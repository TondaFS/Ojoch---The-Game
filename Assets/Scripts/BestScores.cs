using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class ScoreElement
{
    public string name;
    public int score;
}

public class BestScores : MonoBehaviour {
    public List<ScoreElement> scores;      

    public void InitiateBestScores() {
        for(int i = 0; i < 10; i++){
            ScoreElement element = new ScoreElement();
            element.name = "OjochMaster";
            element.score = Random.Range(100, 10000);
            scores.Add(element);   
        }
        SortScores();        
    }

    public void SortScores()
    {
        scores.Sort((x, y) => y.score.CompareTo(x.score));
    }

    public void CheckScores(float final)
    {
        for (int i = 0; i < 10; i++)
        {
            if (final >= GameManager.instance.highscores.scores[i].score)
            {                
                GameManager.instance.highscores.scores.RemoveAt(9);
                ScoreElement skore = new ScoreElement();
                skore.name = "Nový Ojoch";
                skore.score = (int)final;
                GameManager.instance.newRecord = true;
                GameManager.instance.recordScore = final;
                GameManager.instance.highscores.scores.Add(skore);
                GameManager.instance.highscores.SortScores();
                break;
            }
        }
    }     
}
