using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Stats 
{
    public int gamesPlayed;
    public int enemiesKilled;
    public int powerUpsCollected;
    public int longestTimePlayed;
    public int gotMad;
    public int fullSanity;

    
    public Stats(int v1, int v2, int v3, int v4, int v5, int v6)
    {
        this.gamesPlayed = v1;
        this.enemiesKilled = v2;
        this.powerUpsCollected = v3;
        this.longestTimePlayed = v4;
        this.gotMad = v5;
        this.fullSanity = v6;
    }
    
}

public class GameStatistics : MonoBehaviour{
    public Stats stats = new Stats(0,0,0,0,0,0);    

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
    
}


