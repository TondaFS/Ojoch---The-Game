using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// ÚKOL a jeho jednotlivé položky.
/// <para>
/// ID: Identifikační číslo úkolu.
/// DESCRIPTION: Popis úkolu.
/// PROGRESS: Aktuální progress.
/// TARGET: Cílová hodnota úkolu (při zabití, sebrání předmětů apod.)
/// TYPE: O jaký typ úkolu se jedná? (kill, modify, score...)
/// COMPLETED: Je již splněn?
/// </para>
/// </summary>
[System.Serializable]
public class Task
{    
    public int id;
    public string description;
    public int progress;
    public int target;
    public string type;
    public bool completed;

    public Task(int v1, string v2, int v3, int v4, string v5, bool v6)
    {
        this.id = v1;
        this.description = v2;
        this.progress = v3;
        this.target = v4;
        this.type = v5;
        this.completed = v6;
    }
}

public class TaskManager : MonoBehaviour {
    public Task[] activeTasks = new Task[]
    {
        new Task(0, "none", 0, 0, "none", false),
        new Task(0, "none", 0, 0, "none", false),
        new Task(0, "none", 0, 0, "none", false)
    };

    //Prvni sada ukolu
    public Task[] allFirstTasks = new Task[] {  new Task(0, "Zabij 20 nepřátel.", 0, 20, "kill", false),
                                                new Task(1, "Zabij 15 nepřátel v jedné hře.", 0, 15, "killRound", false),
                                                new Task(2, "Zabij 50 nepřátel.", 0, 50, "kill", false),
                                                new Task(3, "Zabij 30 nepřátel v jedné hře.", 0, 30, "killRound", false),
                                                new Task(4, "Zabij 75 nepřátel.", 0, 75, "kill", false),
                                                new Task(5, "Zabij 40 nepřátel v jedné hře.", 0, 40, "killRound", false),
                                                new Task(6, "Přijď o veškerou příčetnost.", 0, 1, "sanity", false),
                                                new Task(7, "Zabij 100 nepřátel.", 0, 100, "kill", false),
                                                new Task(8, "Zabij 50 nepřátel v jedné hře.", 0, 50, "killRound", false),
                                                new Task(9, "Přijď o veškerou příčetnost 3x.", 0, 3, "sanity", false),
                                                new Task(10, "Zabij 150 nepřátel.", 0, 150, "kill", false),
                                                new Task(11, "Zabij 75 nepřátel v jedné hře.", 0, 75, "killRound", false),
                                                new Task(12, "Přijď o veškerou příčetnost 5x.", 0, 5, "sanity", false),
                                                new Task(13, "Zabij 300 nepřátel.", 0, 300, "kill", false),
                                                new Task(14, "Zabij 100 nepřátel v jedné hře.", 0, 100, "killRound", false),
                                                new Task(15, "Opusť ten Hejt!", 0, 0, "completed", false),
    };

    //Druha sada ukolu
    public Task[] allSecondTasks = new Task[] { new Task(0, "Dosáhni skóre 10 000.", 0, 10000, "score", false),
                                                new Task(1, "Vydrž s Ojochem naživu po dobu 60s.", 0, 60, "distance", false),
                                                new Task(2, "Udrž si modifikator x9 po dobu 10s.", 0, 10, "modify", false),
                                                new Task(3, "Dosáhni skóre 25 000.", 0, 25000, "score", false),
                                                new Task(4, "Vydrž s Ojochem naživu po dobu 120s.", 0, 120, "distance", false),
                                                new Task(5, "Udrž si modifikator x9 po dobu 15s.", 0, 15, "modify", false),
                                                new Task(6, "Dosáhni skóre 50 000.", 0, 50000, "score", false),
                                                new Task(7, "Vydrž s Ojochem naživu po dobu 150s.", 0, 150, "distance", false),
                                                new Task(8, "Udrž si modifikator x9 po dobu 20s.", 0, 20, "modify", false),
                                                new Task(9, "Dosáhni skóre 100 000.", 0, 100000, "score", false),
                                                new Task(10, "Vydrž s Ojochem naživu po dobu 200s.", 0, 200, "distance", false),
                                                new Task(11, "Udrž si modifikator x9 po dobu 25s.", 0, 25, "modify", false),
                                                new Task(12, "Dosáhni skóre 150 000.", 0, 150000, "score", false),
                                                new Task(13, "Vydrž s Ojochem naživu po dobu 300s.", 0, 300, "distance", false),
                                                new Task(14, "Udrž si modifikator x9 po dobu 35s.", 0, 35, "modify", false),
                                                new Task(15, "Když chceš postavit loď, tak nepotřebuješ dřevo... nebo tak nějak.", 0, 0, "completed", false),
    };

    //Treti sada ukolu
    public Task[] allThirdTasks = new Task[] {  new Task(0, "Seber 5 PowerUpů / PowerDownů.", 0, 5, "grab", false),
                                                new Task(1, "Seber 5 PowerUpů / PowerDownů během jedné hry.", 0, 5, "grabRound", false),
                                                new Task(2, "Udrž si maximální příčetnost. Délka hry musí být alsepoň 30s.", 0, 30, "sanityFull", false),
                                                new Task(3, "Seber 10 PowerUpů / PowerDownů.", 0, 10, "grab", false),
                                                new Task(4, "Seber 10 PowerUpů / PowerDownů během jedné hry.", 0, 10, "grabRound", false),
                                                new Task(5, "Udrž si maximální příčetnost. Délka hry musí být alsepoň 60s.", 0, 60, "sanityFull", false),
                                                new Task(6, "Seber 25 PowerUpů / PowerDownů.", 0, 25, "grab", false),
                                                new Task(7, "Seber 15 PowerUpů / PowerDownů během jedné hry.", 0, 15, "grabRound", false),
                                                new Task(8, "Udrž si maximální příčetnost. Délka hry musí být alsepoň 120s.", 0, 120, "sanityFull", false),
                                                new Task(9, "Seber 50 PowerUpů / PowerDownů.", 0, 50, "grab", false),
                                                new Task(10, "Seber 20 PowerUpů / PowerDownů během jedné hry.", 0, 20, "grabRound", false),
                                                new Task(11, "Udrž si maximální příčetnost. Délka hry musí být alsepoň 180s.", 0, 180, "sanityFull", false),
                                                new Task(12, "Seber 100 PowerUpů / PowerDownů.", 0, 100, "grab", false),
                                                new Task(13, "Seber 30 PowerUpů / PowerDownů během jedné hry.", 0, 30, "grabRound", false),
                                                new Task(14, "Udrž si maximální příčetnost. Délka hry musí být alsepoň 240s.", 0, 240, "sanityFull", false),
                                                new Task(15, "Iä! Iä! Cthulhu fhtagn! Ph'nglui mglw'nfah Cthulhu R'lyeh wgah'nagl fhtagn!", 0, 0, "completed", false),
    };

    public int killsPerGame;
    public int grabsPerGame;
    public float modifyTime;
    public float modifyTmp;


    void Start()
    {
        modifyTime = 0;
        killsPerGame = 0;
        modifyTmp = 0;
        grabsPerGame = 0;
    }
    
    //Vytvori ukol taskID ze sady activeQuest
    public void InitiateTask (int taskId, int activeQuest)
    {
        Task brandNewTask = new Task(-1, "Všechno splněno", 0, 0, "done", false);

        //Pokud jsou vsechny ukoly ze sady splneny -> nastavi tam ze je vse splneno
        if (taskId >= 17)
        {
            activeTasks[activeQuest] = brandNewTask;
        }
        else
        {
            //Vybere sadu
            switch (activeQuest)
            {
                case 0:
                    brandNewTask = allFirstTasks[taskId];
                    break;
                case 1:
                    brandNewTask = allSecondTasks[taskId];
                    break;
                case 2:
                    brandNewTask = allThirdTasks[taskId];
                    break;
            }

            //nastavi parametry
            activeTasks[activeQuest].id = brandNewTask.id;
            activeTasks[activeQuest].description = brandNewTask.description;
            activeTasks[activeQuest].target = brandNewTask.target;
            activeTasks[activeQuest].progress = brandNewTask.progress;
            activeTasks[activeQuest].type = brandNewTask.type;
            activeTasks[activeQuest].completed = brandNewTask.completed;
        }
    }    

    //Ukoly, ktere se kontroluji jen na konci hry (skore, vydalenost..)
    public void CheckOnceTask(float finalScore, int idTask)
    {
        if (activeTasks[idTask].progress < (int)finalScore)
        {
            activeTasks[idTask].progress = (int)finalScore;
        }
        if(activeTasks[idTask].progress >= activeTasks[idTask].target)
        {
            activeTasks[idTask].completed = true;
        }
    }        

    /// <summary>
    /// Pro splneni tasku: zabiti nepratel, sebrani powerUpu, odehrani her
    /// </summary>
    public void CheckCountingTask(int idTask)
    {
        activeTasks[idTask].progress += 1;
        if(activeTasks[idTask].progress >= activeTasks[idTask].target)
        {
            activeTasks[idTask].completed = true;
        }
    }

    /// <summary>
    /// Zkontroluje, jeslti není splněn jeden z počítacích úkolů (kill, grab)
    /// </summary>
    /// <param name="idTask">ID Úkoĺu</param>
    /// <param name="update">O kolik se zvětšilo</param>
    public void CheckCountingTaskAtTheEnd(int idTask, int update)
    {
        activeTasks[idTask].progress += update;
        if (activeTasks[idTask].progress >= activeTasks[idTask].target)
        {
            activeTasks[idTask].completed = true;
        }
    }    

    public void DisplayAllTasks(Text one, Text two, Text three)
    {
        DisplayTaskOne(one);
        DisplayTaskTwo(two);
        DisplayTaskThree(three);
    }

    public void DisplayTaskOne(Text one)
    {
        one.text = activeTasks[0].description + "\n" + "Splněno: " + activeTasks[0].progress + " / " + activeTasks[0].target;

    }

    public void DisplayTaskTwo(Text two)
    {
        two.text = activeTasks[1].description + "\n" + "Splněno: " + activeTasks[1].progress + " / " + activeTasks[1].target;
    }

    public void DisplayTaskThree(Text three)
    {
        three.text = activeTasks[2].description + "\n" + "Splněno: " + activeTasks[2].progress + " / " + activeTasks[2].target;
    }

    public void NewTask(int questRow, GameObject button, Text textField)
    {
        InitiateTask(activeTasks[questRow].id + 1, questRow);
        button.SetActive(false);
        switch (questRow)
        {
            case 0:
                
                GameManager.instance.GetComponent<TaskManager>().DisplayTaskOne(textField);
                break;
            case 1:
                GameManager.instance.GetComponent<TaskManager>().DisplayTaskTwo(textField);
                break;
            case 2:
                GameManager.instance.GetComponent<TaskManager>().DisplayTaskThree(textField);
                break;
        }

        GameManager.instance.SaveData();
    }

    /*
    //Zobrazi ukoly
    public void displayTasks()
    {
        DisplayFirst();
        DisplaySecond();
        DisplayThird();       
    }  

    //Zobrazi aktivni ukol prvni sady
    public void DisplayFirst()
    {
        GameObject.Find("firstDescription").GetComponent<Text>().text = activeTasks[0].description;
        GameObject.Find("firstProgress").GetComponent<Text>().text = "Splněno: " + activeTasks[0].progress + " / " + activeTasks[0].target;
        if (activeTasks[0].completed)
        {
            GameObject.Find("scoreManager").GetComponent<ScoreManager>().firstComplete.SetActive(true);
        }
    }

    //Zobrazi aktivni ukol druhe sady
    public void DisplaySecond()
    {
        GameObject.Find("secondDescription").GetComponent<Text>().text = activeTasks[1].description;
        GameObject.Find("secondProgress").GetComponent<Text>().text = "Splněno: " + activeTasks[1].progress + " / " + activeTasks[1].target;
        if (activeTasks[1].completed)
        {
            GameObject.Find("scoreManager").GetComponent<ScoreManager>().secondComplete.SetActive(true);
        }
    }

    //Zobrazi aktivni ukol treti sady
    public void DisplayThird()
    {
        GameObject.Find("thirdDescription").GetComponent<Text>().text = activeTasks[2].description;
        GameObject.Find("thirdProgress").GetComponent<Text>().text = "Splněno: " + activeTasks[2].progress + " / " + activeTasks[2].target;
        if (activeTasks[2].completed)
        {
            GameObject.Find("scoreManager").GetComponent<ScoreManager>().thirdComplete.SetActive(true);
        }
    }
    */
}
