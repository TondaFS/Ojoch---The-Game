using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// Položka Tabulky nejlepších.
/// <para>Obsahuje jméno a doažené skóre.</para>
/// </summary>
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

/// <summary>
/// Skript řeší Tabulku nejlepších dosažených výsledků
/// </summary>
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
        new ScoreElement("Johnnyho máma", 250000)
    };

    /// <summary>
    /// Vytvoří novou Tabulku nejlepších dle přednastaveného seznamu.
    /// </summary>
    public void InitiateBestScores() {
        for(int i = 0; i < 10; i++){                        
            scores.Add(tabulkaNejlepsich[i]);   
        }
        SortScores();        
    }
    
    /// <summary>
    /// Seřadí Tabulku nejlepších
    /// </summary>
    public void SortScores()
    {
        scores.Sort((x, y) => y.score.CompareTo(x.score));
    }

    /// <summary>
    /// Zkontroluje, jestli nebylo dosáhnuto nové skóre, pokud ano, vytvoří nový záznam.
    /// </summary>
    /// <param name="final">Nové dosažené skóre.</param>
    public void CheckScores(float final)
    {
        for (int i = 0; i < 10; i++)
        {
            if (final >= GameManager.instance.highscores.scores[i].score)
            {                
                GameManager.instance.highscores.scores.RemoveAt(9);
                ScoreElement skore = new ScoreElement("Nový Ojoch", (int)final);
                GameManager.instance.newRecord = true;
                GameManager.instance.recordScore = final;
                GameManager.instance.highscores.scores.Add(skore);
                GameManager.instance.highscores.SortScores();
                break;
            }
        }
    }     
}
