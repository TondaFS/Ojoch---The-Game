using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class Task {
    public int id;
    public bool done;
}


public class TaskManager : MonoBehaviour {

    public void InitiateTasks() {
        for(int i = 0; i < 5; i++)
        {
            Task newTask = new Task();
            newTask.id = i;
            newTask.done = false;
        }
    }
}
