using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour {   
    
    //textova pole pro skore 
    public Text prvni;
    public Text prvniScore;
    public Text druhy;
    public Text druhyScore;
    public Text treti;
    public Text tretiScore;
    public Text ctvrty;
    public Text ctvrtyScore;
    public Text paty;
    public Text patyScore;
    public Text sesty;
    public Text sestyScore;
    public Text sedmy;
    public Text sedmyScore;
    public Text osmy;
    public Text osmyScore;
    public Text devaty;
    public Text devatyScore;
    public Text desaty;
    public Text desatyScore;

    public List<ScoreElement> score;
    public ScreenFader fader;
    public BestScores best;

    //Pole pro zadavani jmena
    public GameObject pole;
    public bool shown = false;

    //Tlacitka zpet a restart
    public GameObject back;
    public GameObject restart;

    private bool highscore = true;
    
    //Veci tykajici se ukolu
    public GameObject tasks;
    public GameObject scores;

    public GameObject firstComplete;
    public GameObject secondComplete;
    public GameObject thirdComplete;

    public Text changer;
    public Text prepnutiText;

    public GameObject prepnuti;

    void Start() {
        pole = GameObject.Find("policko");
        pole.SetActive(false);
        score = GameManager.instance.highscores.scores;
        back = GameObject.Find("BacktoMenu");
        restart = GameObject.Find("Restart");
        //restart.SetActive(false);
        //back.SetActive(false);
        firstComplete = GameObject.Find("newFirst");
        firstComplete.SetActive(false);
        secondComplete = GameObject.Find("newSecond");
        secondComplete.SetActive(false);
        thirdComplete = GameObject.Find("newThird");
        thirdComplete.SetActive(false);
        tasks = GameObject.Find("Task");
        tasks.SetActive(false);
        scores = GameObject.Find("scores");

        fader = gameObject.GetComponent<ScreenFader>();

        prepnuti = GameObject.Find("prepnutiWhole");
        //prepnuti.SetActive(false);

        prepnuti.SetActive(true);        
        DisplayScore();
        back.SetActive(true);
        restart.SetActive(true);
        GameManager.instance.SaveData();
       

    }

    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.GetComponent<ScreenFader>().FadeOutLoadNewScene("menu");
        }


        /*    
        //Az hrac stiskne enter, zobrazi se skore se zadanym jmenem
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameManager.instance.newRecord = false;
            NewName();

        }
        //Pokud bylo dosahnuto noveho rekordu, lze zadat nove jmeno
        if (GameManager.instance.newRecord)
        {            
            pole.SetActive(true);
            pole.GetComponent<InputField>().Select();
        }

        //Zobrazeni skore a ostatnich prvku
        else
        {
            prepnuti.SetActive(true);    
            if (!shown)
            {
                DisplayScore();
                back.SetActive(true);
                restart.SetActive(true);
                GameManager.instance.SaveData();                
            }           
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                this.GetComponent<ScreenFader>().FadeOutLoadNewScene("menu");
            }
        }
        */
    }
    
    /*
    //Vlozi zadane jmeno k prave ziskanemu rekordu ve hre
    void NewName()
    {        
        //najde odpovidajici skore
        for (int i = 0; i < 10; i++)
        {
            if(score[i].score == GameManager.instance.recordScore)
            {
                score[i].name = pole.GetComponent<InputField>().text;                
                GameManager.instance.recordScore = 0;                
                break;
            }
        }
        //vypne pole
        pole.SetActive(false);
    }
    */

    //Zobrazi tabulku skore
    void DisplayScore()
    {
        prvni.text = score[0].name;
        prvniScore.text = "" + score[0].score;

        druhy.text = score[1].name;
        druhyScore.text = "" + score[1].score;

        treti.text = score[2].name;
        tretiScore.text = "" + score[2].score;

        ctvrty.text = score[3].name;
        ctvrtyScore.text = "" + score[3].score;

        paty.text = score[4].name;
        patyScore.text = "" + score[4].score;

        sesty.text = score[5].name;
        sestyScore.text = "" + score[5].score;

        sedmy.text = score[6].name;
        sedmyScore.text = "" + score[6].score;

        osmy.text = score[7].name;
        osmyScore.text = "" + score[7].score;

        devaty.text = score[8].name;
        devatyScore.text = "" + score[8].score;

        desaty.text = score[9].name;
        desatyScore.text = "" + score[9].score;

        shown = true;         
    }

    public void ChangeToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //Prepne zobrazeni skore a ukolu
    public void SwitchShow() {
        if (highscore)
        {
            highscore = false;
            tasks.SetActive(true);
            scores.SetActive(false);
            GameManager.instance.GetComponent<TaskManager>().displayTasks();
            changer.text = "Aktivní úkoly";
            prepnutiText.text = "Skóre";


        }
        else
        {
            highscore = true;
            tasks.SetActive(false);
            scores.SetActive(true);
            changer.text = "Tabulka nejlepších";
            prepnutiText.text = "Úkoly";
        }
    }

    //Pokud hrac klikne na moynost noveho ukolu, vytvori se a ihned zobrazi
    public void NewTask(int questRow)
    {        
        GameManager.instance.GetComponent<TaskManager>().InitiateTask(GameManager.instance.GetComponent<TaskManager>().activeTasks[questRow].id + 1, questRow);
        switch (questRow)
        {
            case 0:
                firstComplete.SetActive(false);
                GameManager.instance.GetComponent<TaskManager>().DisplayFirst();
                break;
            case 1:
                secondComplete.SetActive(false);
                GameManager.instance.GetComponent<TaskManager>().DisplaySecond();
                break;
            case 2:
                thirdComplete.SetActive(false);
                GameManager.instance.GetComponent<TaskManager>().DisplayThird();
                break;
        }
        //vse se ulozi
        GameManager.instance.SaveData();
    }
}
