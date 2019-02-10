using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;
using SmartLocalization;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public BestScores highscores;
    public bool newRecord = false;
    public float recordScore = 0;
    public LanguageManager languageManager;
    public string playerName;
    public string activeLanguage;

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
        playerName = "Ojoch";
        
        LoadData();
        languageManager = LanguageManager.Instance;
        LanguageManager.SetDontDestroyOnLoad();
        languageManager.ChangeLanguage(activeLanguage);
          
    }
    
    //Ulozi data do Slozky (pokud neexistuje, vytvori jej)
    public void SaveData() {
        if (!Directory.Exists("NothingHere"))
            Directory.CreateDirectory("NothingHere");

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Create("NothingHere/notSaves.lol");
        formatter.Serialize(saveFile, GetComponent<CoinsManager>().coins);
        formatter.Serialize(saveFile, GetComponent<GameStatistics>().stats);

        formatter.Serialize(saveFile, highscores.scores);                           //ulozi tabulku skore

        formatter.Serialize(saveFile, GetComponent<SoundManager>().musicVolume);    //ulozci nastaveni hlasitosti
        formatter.Serialize(saveFile, GetComponent<SoundManager>().soundsVolume);
        formatter.Serialize(saveFile, GetComponent<TaskManager>().activeTasks);     //ulozi prave aktvini ukoly 
        formatter.Serialize(saveFile, playerName);
        formatter.Serialize(saveFile, activeLanguage);
        formatter.Serialize(saveFile, GetComponent<BonusManager>().ojochHasHat);
        formatter.Serialize(saveFile, GetComponent<BonusManager>().hatBought);
        
        
        saveFile.Close();
    }

    //nacte data ze slozky
    public void LoadData()
    {
        if (File.Exists("NothingHere/notSaves.lol"))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream saveFile = File.Open("NothingHere/notSaves.lol", FileMode.Open);
            GetComponent<CoinsManager>().coins = (int)formatter.Deserialize(saveFile);
            GetComponent<GameStatistics>().stats = (Stats)formatter.Deserialize(saveFile);
            highscores.scores = (List<ScoreElement>)formatter.Deserialize(saveFile);
            GetComponent<SoundManager>().musicVolume = (float)formatter.Deserialize(saveFile);
            GetComponent<SoundManager>().soundsVolume = (float)formatter.Deserialize(saveFile);
            GetComponent<TaskManager>().activeTasks = (Task[])formatter.Deserialize(saveFile);
            playerName = (String)formatter.Deserialize(saveFile);
            activeLanguage = (String)formatter.Deserialize(saveFile);
            GetComponent<BonusManager>().ojochHasHat = (bool)formatter.Deserialize(saveFile);
            GetComponent<BonusManager>().hatBought = (bool)formatter.Deserialize(saveFile);         
            
            saveFile.Close();            
        }
        //Pokud soubor neexistuje, vytvori novou nahodnou tabulku se skore a pripravi prvni ukol z kayde sady ukolu
        else {
            GetComponent<CoinsManager>().coins = 0;
            highscores.InitiateBestScores();
            GetComponent<TaskManager>().InitiateTask(0, 0);
            GetComponent<TaskManager>().InitiateTask(0, 1);
            GetComponent<TaskManager>().InitiateTask(0, 2);
            activeLanguage = "cs-CZ";
            GetComponent<BonusManager>().ojochHasHat = false;
            GetComponent<BonusManager>().hatBought = false;
        }
    }   
}
