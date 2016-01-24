using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {
    public Text scoreText;
    public Text modi;
    public float modifikatorScore = 1;              //Modfifikator
    public float tmpscore;                          //hracovo skore
    public float scorePerSecond = 0;                //pro zvyseni skore za kazdou vterinu  
    public int killedEnemies;                       //pocet zabitych nepratel
    public float fiveSecondsTimer = 0;               //Timer na vynulovani modifikatoru skore

    public Slider fiveSecondsSlider;
    public GameObject fiveSecondsObject;

    void Start()
    {
        
        scorePerSecond = 1;
        tmpscore = 0;
        killedEnemies = 0;
        fiveSecondsSlider = GameObject.Find("FiveSeconds").GetComponent<Slider>();
        fiveSecondsObject = GameObject.Find("FiveSeconds");
        fiveSecondsObject.SetActive(false);
        modi = GameObject.Find("Multi").GetComponent<Text>();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();

    }

    void Update()
    {
        scoreText.text = "Skóre: " + tmpscore;

        if (scorePerSecond <= 0)
        {
            tmpscore += 1 * modifikatorScore;
            scorePerSecond = 1;
        }
        scorePerSecond -= Time.deltaTime;

        if (modifikatorScore < 1)
        {
            modifikatorScore = 1;
        }

        modi.text = "Modifikátor: " + modifikatorScore + "x";

        if (killedEnemies == 3)
        {
            modifikatorScore += 1;
            killedEnemies = 0;
        }

        if (modifikatorScore > 9)
        {
            modifikatorScore = 9;
        }

        if (fiveSecondsTimer > 0)
        {

            fiveSecondsTimer -= Time.deltaTime;
            fiveSecondsSlider.value = fiveSecondsTimer;
            if (fiveSecondsTimer <= 0)
            {
                fiveSecondsObject.SetActive(false);
                modifikatorScore = 0;
                killedEnemies = 0;

            }
        }
    }

    public void AdjustScore(float value)
    {
        tmpscore += value * modifikatorScore;
    }

    public void FiveSecondsTimer()
    {
        fiveSecondsTimer = 5;
        fiveSecondsObject.SetActive(true);
        fiveSecondsSlider.value = fiveSecondsTimer;
    }
}
