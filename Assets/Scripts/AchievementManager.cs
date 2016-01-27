using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Achievement{
    public string name;
    public string description;
    public bool achieved;
    public int progress;
    public int target;

    public Achievement(string name, string v2, bool v3, int v4, int v5)
    {
        this.name = name;
        this.description = v2;
        this.achieved = v3;
        this.progress = v4;
        this.target = v5;
    }    
}

public class AchievementManager : MonoBehaviour {

    public Achievement[] allAchievements = new Achievement[] {  new Achievement("Pravidelný hráč", "Zahraj si hru 10x.", false, 0, 10),
                                                                new Achievement("Závislák", "zahraj si hru 50x.", false, 0, 50),
                                                                new Achievement("Obránce", "Zabij 20 nepřátel.", false, 0, 20),
                                                                new Achievement("Zabiják", "Zabij 50 nepřátel.", false, 0, 50),
                                                                new Achievement("Ojoší nováček", "Dosáhni skóre 5 000.", false, 0, 5000),
                                                                new Achievement("Ojoší znalec", "Dosáhni skóre 10 000.", false, 0, 10000),
    };



}
