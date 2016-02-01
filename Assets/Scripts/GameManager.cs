using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public BestScores highscores;
    public bool newRecord = false;
    public float recordScore = 0;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        highscores = GetComponent<BestScores>();        
        LoadData();
    }
    
    //Ulozi data do Slozky (pokud neexistuje, vytvori jej)
    public void SaveData() {
        if (!Directory.Exists("NothingHere"))
            Directory.CreateDirectory("NothingHere");

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Create("NothingHere/notSavesLOL.sav");

        formatter.Serialize(saveFile, highscores.scores);                           //ulozi tabulku skore
        formatter.Serialize(saveFile, GetComponent<SoundManager>().musicVolume);    //ulozci nastaveni hlasitosti
        formatter.Serialize(saveFile, GetComponent<SoundManager>().soundsVolume);
        formatter.Serialize(saveFile, GetComponent<TaskManager>().activeTasks);     //ulozi prave aktvini ukoly   
        saveFile.Close();
    }

    //nacte data ze slozky
    public void LoadData()
    {
        if (File.Exists("NothingHere/notSavesLOL.sav"))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream saveFile = File.Open("NothingHere/notSavesLOL.sav", FileMode.Open);

            highscores.scores = (List<ScoreElement>)formatter.Deserialize(saveFile);
            GetComponent<SoundManager>().musicVolume = (float)formatter.Deserialize(saveFile);
            GetComponent<SoundManager>().soundsVolume = (float)formatter.Deserialize(saveFile);
            GetComponent<TaskManager>().activeTasks = (Task[])formatter.Deserialize(saveFile);
            saveFile.Close();            
        }
        //Pokud soubor neexistuje, vytvori novou nahodnou tabulku se skore a pripravi prvni ukol z kayde sady ukolu
        else {
            highscores.InitiateBestScores();
            GetComponent<TaskManager>().InitiateTask(0, 0);
            GetComponent<TaskManager>().InitiateTask(0, 1);
            GetComponent<TaskManager>().InitiateTask(0, 2);
        }
    }   
}
