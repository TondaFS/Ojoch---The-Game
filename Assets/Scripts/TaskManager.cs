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

    public Task[] allFirstTasks = new Task[] {  new Task(0, "Udrž si maximální příčetnost po 1 hru.", 0, 1, "sanityFull", false),
                                                new Task(1, "Dosáhni skóre 500.", 0, 500, "score", false),
                                                new Task(2, "Přijď o veškerou příčetnost.", 0, 1, "sanity", false),
                                                new Task(3, "Dosáhni skóre 5000.", 0, 5000, "score", false),
                                                new Task(4, "Přijď o veškerou příčetnost 3x.", 0, 3, "sanity", false),};

    public Task[] allSecondTasks = new Task[] { new Task(0, "Udrž modifikator x9 po dobu 10s", 0, 10, "modify", false),
                                                new Task(1, "Zabij 15 nepřátel.", 0, 15, "kill", false),
                                                new Task(2, "Zabij 10 nepřátel v jedné hře", 0, 10, "killRound", false),
                                                new Task(3, "Zabij 5 nepřátel.", 0, 5, "kill", false),
                                                new Task(4, "Udrž modifikator x9 po dobu 20s", 0, 20, "modify", false),};

    public Task[] allThirdTasks = new Task[] {  new Task(0, "Seber v 1 hře jeden powerUp.", 0, 1, "grabRound", false),
                                                new Task(1, "Odehrej hru 3x", 0, 3, "play", false),
                                                new Task(2, "Uraž vzdálenost 20.", 0, 20, "distance", false),
                                                new Task(3, "Seber 3 PowerUpy.", 0, 3, "grab", false),
                                                new Task(4, "Zabij 25 nepřátel v jedné hře", 0, 15, "killRound", false),};

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
    
    public void InitiateTask (int taskId, int activeQuest)
    {
        Task brandNewTask = new Task(0, "none", 0, 0, "none", false);
        switch (activeQuest) {
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
        activeTasks[activeQuest].id = brandNewTask.id;
        activeTasks[activeQuest].description = brandNewTask.description;
        activeTasks[activeQuest].target = brandNewTask.target;
        activeTasks[activeQuest].progress = brandNewTask.progress;
        activeTasks[activeQuest].type = brandNewTask.type;
        activeTasks[activeQuest].completed = brandNewTask.completed;
    }    

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

    public void displayTasks()
    {
        DisplayFirst();
        DisplaySecond();
        DisplayThird();       
    }  
    public void DisplayFirst()
    {
        GameObject.Find("firstDescription").GetComponent<Text>().text = activeTasks[0].description;
        GameObject.Find("firstProgress").GetComponent<Text>().text = "Splněno: " + activeTasks[0].progress + " / " + activeTasks[0].target;
        if (activeTasks[0].completed)
        {
            GameObject.Find("scoreManager").GetComponent<ScoreManager>().firstComplete.SetActive(true);
        }
    }  
    public void DisplaySecond()
    {
        GameObject.Find("secondDescription").GetComponent<Text>().text = activeTasks[1].description;
        GameObject.Find("secondProgress").GetComponent<Text>().text = "Splněno: " + activeTasks[1].progress + " / " + activeTasks[1].target;
        if (activeTasks[1].completed)
        {
            GameObject.Find("scoreManager").GetComponent<ScoreManager>().secondComplete.SetActive(true);
        }
    }
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
