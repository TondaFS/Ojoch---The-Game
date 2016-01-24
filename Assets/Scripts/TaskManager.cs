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
}

public class TaskManager : MonoBehaviour {
    public Task activeTask = new Task();

    public int enemiesKilled;
    public int powerUpsCollected;
    public int gamesPlayed;
    public int differentPowerUps;    

    void Start()
    {
        enemiesKilled = 0;
        powerUpsCollected = 0;
        gamesPlayed = 0;
        differentPowerUps = 0;
    }

    public void InitiateTask (int taskId){        
        switch(taskId){
            case 1:
                setTask("Dosáhni skóre 500.", taskId, 500, 0, "score", false);
                break;

            case 2:
                setTask("Zabij 5 nepřátel.", taskId, 5, 0, "kill", false);
                break;

            case 3:
                setTask("Seber 5 PowerUpů.", taskId, 5, 0, "grab", false);
                break;

            case 4:
                setTask("Odehrej hru 5x.", taskId, 5, 0, "play", false);
                break;            

            case 5:
                setTask("Dosáhni skóre 1 000.", taskId, 1000, 0, "score", false);
                break;
            default:
                setTask("Všechny úkoly splněny.", -1, -1, 0, "complete", false);
                break;
        }
    }

    public void setTask(string description, int id, int target, int progress, string type, bool complete)
    {
        activeTask.id = id;        
        activeTask.description = description;
        activeTask.target = target;
        activeTask.progress = progress;
        activeTask.type = type;
        activeTask.completed = complete;
    }

    public void CheckScoreTask(float finalScore)
    {
        if (activeTask.progress < (int)finalScore)
        {
            activeTask.progress = (int)finalScore;
        }
        if(activeTask.progress >= activeTask.target)
        {
            activeTask.completed = true;
        }
    }        

    /// <summary>
    /// Pro splneni tasku: zabiti nepratel, sebrani powerUpu, odehrani her
    /// </summary>
    public void CheckCountingTask()
    {
        activeTask.progress += 1;
        if(activeTask.progress >= activeTask.target)
        {
            activeTask.completed = true;
        }
    }

    public void displayTask()
    {
        GameObject.Find("taskDescription").GetComponent<Text>().text = activeTask.description;
        GameObject.Find("taskprogress").GetComponent<Text>().text = "Splněno: " + activeTask.progress + " / " + activeTask.target;
        if (activeTask.completed)
        {
            GameObject.Find("scoreManager").GetComponent<ScoreManager>().completedTask.SetActive(true);
        }
    }    
}
