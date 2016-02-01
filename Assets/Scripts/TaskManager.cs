using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

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
    public Task[] allFirstTasks = new Task[] {  new Task(0, "Dosáhni skóre 500.", 0, 500, "score", false),
                                                new Task(1, "Udrž si maximální příčetnost 3x.", 0, 3, "sanityFull", false),
                                                new Task(2, "Přijď o veškerou příčetnost.", 0, 1, "sanity", false),
                                                new Task(3, "Dosáhni skóre 5000.", 0, 5000, "score", false),
                                                new Task(4, "Přijď o veškerou příčetnost 3x.", 0, 3, "sanity", false),};

    //Druha sada ukolu
    public Task[] allSecondTasks = new Task[] { new Task(0, "Zabij 15 nepřátel.", 0, 15, "kill", false),
                                                new Task(1, "Zabij 20 nepřátel v jedné hře", 0, 20, "killRound", false),
                                                new Task(2, "Udrž si modifikator x9 po dobu 15s", 0, 15, "modify", false ),
                                                new Task(3, "Zabij 50 nepřátel.", 0, 50, "kill", false),
                                                new Task(4, "Udrž si modifikator x9 po dobu 30s", 0, 30, "modify", false),};

    //Treti sada ukolu
    public Task[] allThirdTasks = new Task[] {  new Task(0, "Seber v jedné hře 5 powerUpů.", 0, 5, "grabRound", false),
                                                new Task(1, "Odehrej hru 5x", 0, 5, "play", false),
                                                new Task(2, "Uraž vzdálenost 100.", 0, 100, "distance", false),
                                                new Task(3, "Seber 30 PowerUpů.", 0, 30, "grab", false),
                                                new Task(4, "Zabij 25 nepřátel v jedné hře", 0, 25, "killRound", false),};

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
        if (taskId >= 5)
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
}
