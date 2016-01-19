using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public int numberOfClicks;
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
        highscores = this.GetComponent<BestScores>();
        LoadData();    
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Application.LoadLevel("menu");
        }

        if (Input.GetMouseButtonDown(0))
        {
            ++numberOfClicks;
            Debug.Log(numberOfClicks);
        }
    }

    public void SaveData() {
        if (!Directory.Exists("NothingHere"))
            Directory.CreateDirectory("NothingHere");

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Create("NothingHere/notSavesLOL.sav");

        formatter.Serialize(saveFile, highscores.scores);
        saveFile.Close();
    }

    public void LoadData()
    {
        if (File.Exists("NothingHere/notSavesLOL.sav"))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream saveFile = File.Open("NothingHere/notSavesLOL.sav", FileMode.Open);

            highscores.scores = (List<ScoreElement>)formatter.Deserialize(saveFile);
            saveFile.Close();
        }
        else {
            highscores.InitiateBestScores();            
        }
    }   
}
