using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using SmartLocalization;

[System.Serializable]
public class Stats 
{
    public int gamesPlayed;
    public int enemiesKilled;
    public int powerUpsCollected;
    public int longestTimePlayed;
    public int gotMad;
    public int fullSanity;
    public float playedTime;
    
    public Stats(int v1, int v2, int v3, int v4, int v5, int v6, int v7)
    {
        this.gamesPlayed = v1;
        this.enemiesKilled = v2;
        this.powerUpsCollected = v3;
        this.longestTimePlayed = v4;
        this.gotMad = v5;
        this.fullSanity = v6;
        this.playedTime = v7;
    }
    
}

public class GameStatistics : MonoBehaviour{
    public Stats stats = new Stats(0,0,0,0,0,0,0);    

    /// <summary>
    /// Zvýší statistiky o dané hondoty.
    /// </summary>
    /// <param name="games">Odehraných her.</param>
    /// <param name="killed">Zabito nepřítel.</param>
    /// <param name="power">Sebráno PowerUpů/Downů</param>
    /// <param name="time">Nejlepší odehraný čas</param>
    public void UpdateStatistics(int games, int killed, int power, int time)
    {
        stats.gamesPlayed += games;
        stats.enemiesKilled += killed;
        stats.powerUpsCollected += power;

        if (time > stats.longestTimePlayed)
        {
            stats.longestTimePlayed = time;
        }
    }

    /// <summary>
    /// Funkce vypíše všechny statistiky v menu. 
    /// </summary>
    /// <param name="description">Textové pole pro popis dané statistiky.</param>
    /// <param name="values">Textové pole pro hodnoty.</param>
    public void ShowStatistics(Text description, Text values)
    {
        string textDescr = "";
        string textVal = "";

        LanguageManager language = GameManager.instance.languageManager;

        textDescr += language.GetTextValue("Stats.Games") + "\n";
        textDescr += language.GetTextValue("Stats.Enemies") + "\n";
        textDescr += language.GetTextValue("Stats.Powers") + "\n";
        textDescr += language.GetTextValue("Stats.LongestTime") + "\n";
        textDescr += language.GetTextValue("Stats.Mad") + "\n";
        textDescr += language.GetTextValue("Stats.Sanity") + "\n";
        textDescr += language.GetTextValue("Stats.Times") + "\n";
        description.text = textDescr;

        textVal += stats.gamesPlayed + "\n";
        textVal += stats.enemiesKilled + "\n";
        textVal += stats.powerUpsCollected + "\n";
        textVal += PreciseTime(stats.longestTimePlayed);
        textVal += stats.gotMad + "\n";
        textVal += stats.fullSanity+ "\n";
        textVal += PreciseTime((int)stats.playedTime);
        values.text = textVal;
    }

    /// <summary>
    /// Funkce převede vteřiny na přesný čas ve formátu: hodiny, minuty, vteřiny a nakonec dá znak nového řádku.
    /// </summary>
    /// <param name="time">Čas ve vteřinách</param>
    /// <returns></returns>
    public string PreciseTime(int time)
    {
        string correctTime = "";
        int tmpTime = time;


        if (tmpTime > 3600)
        {
            int hours = tmpTime / 60;
            correctTime += hours + "h ";
            tmpTime = tmpTime % 60;
        }

        if(tmpTime > 60)
        {
            int minutes = tmpTime / 60;
            correctTime += minutes + "min ";
            tmpTime = tmpTime % 60;
        }

        correctTime += tmpTime + "s\n"; 
        return correctTime;
    }
    
}
