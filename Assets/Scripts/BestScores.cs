using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class ScoreElement
{
    public string name;
    public int score;

    public ScoreElement(string v1, int v2)
    {
        this.name = v1;
        this.score = v2;
        
    }
}

public class BestScores : MonoBehaviour {
    public List<ScoreElement> scores;

    private ScoreElement[] tabulkaNejlepsich = new ScoreElement[]
    {
        new ScoreElement("Ojoch Master", 100000),
        new ScoreElement("MC Diktát", 80000),
        new ScoreElement("Tondo", 150000),
        new ScoreElement("Honzo", 50000),
        new ScoreElement("Kubo", 120000),
        new ScoreElement("#Hubert", 20000),
        new ScoreElement("Lama z Lemmy", 60000),
        new ScoreElement("Procent", 40000),
        new ScoreElement("Tetinka Mydlinka", 10000),
        new ScoreElement("Johnnyho máma", 500000)
    }; 

    //Funkce vytvori nahodnou tabulku 10 nejlepsich skore
    public void InitiateBestScores() {
        for(int i = 0; i < 10; i++){                        
            scores.Add(tabulkaNejlepsich[i]);   
        }
        SortScores();        
    }

    //Seradi skore v tabulce
    public void SortScores()
    {
        scores.Sort((x, y) => y.score.CompareTo(x.score));
    }

    //Zkontroluje, jestli nebylo dosahnuto nove skore, pokud ano, vytvori novy zaznam
    public void CheckScores(float final)
    {
        for (int i = 0; i < 10; i++)
        {
            if (final >= GameManager.instance.highscores.scores[i].score)
            {                
                GameManager.instance.highscores.scores.RemoveAt(9);
                ScoreElement skore = new ScoreElement("Nový Ojoch", (int)final);
                //skore.name = "Nový Ojoch";
                //skore.score = (int)final;
                GameManager.instance.newRecord = true;
                GameManager.instance.recordScore = final;
                GameManager.instance.highscores.scores.Add(skore);
                GameManager.instance.highscores.SortScores();
                break;
            }
        }
    }     
}
