using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour {
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

    public GameObject pole;
    public bool shown = false;

    void Start() {
        pole = GameObject.Find("policko");
        pole.SetActive(false);
        score = GameManager.instance.highscores.scores;  
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameManager.instance.newRecord = false;
        }
        if (GameManager.instance.newRecord)
        {
            pole.SetActive(true);
        }
        else
        {
            NewName();
            if (!shown)
            {
                DisplayScore();
            }           
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                this.GetComponent<ScreenFader>().FadeOutLoadNewScene("menu");
            }
        }
    }

    void NewName()
    {
        for(int i = 0; i < 10; i++)
        {
            if(score[i].score == GameManager.instance.recordScore)
            {
                score[i].name = pole.GetComponent<InputField>().text;                
                GameManager.instance.recordScore = 0;
                pole.SetActive(false);
            }
        }
    }

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
        GameManager.instance.SaveData();
    }
}
