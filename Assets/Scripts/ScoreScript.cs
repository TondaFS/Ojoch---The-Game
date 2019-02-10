using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {
    public Text scoreText;
    public Text modi;
    /// <summary>
    /// Modifikátor skóre
    /// </summary>
    public float modifikatorScore = 1;       
    /// <summary>
    /// Hráčovo aktuální skóre
    /// </summary>
    public float tmpscore;
    /// <summary>
    /// Hodnota, kterou hráč dostane každou vteřinou
    /// </summary>
    public float scorePerSecond = 0;
    /// <summary>
    /// Počet zabitých nepřátel pro referenci, zda zvýšit modifikátor
    /// </summary>
    public int killedEnemies;

    //Brzy nebude třeba
    public float fiveSecondsTimer = 0;              //Timer na vynulovani modifikatoru skore

    //Skončila hra? brzy nebude třeba
    public bool end;

    public Slider fiveSecondsSlider;
    public GameObject fiveSecondsObject;

    void Start()
    {
        end = false;
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
        //Pokud neni konec, zobrazuje nove skore
        if (!end)
        {
            scoreText.text = "" + tmpscore;
        }

        //kazdou vterinu pricita skore
        if (scorePerSecond <= 0)
        {
            tmpscore += 5 * modifikatorScore;
            scorePerSecond = 1;
        }
        scorePerSecond -= Time.deltaTime;

        
        if (modifikatorScore < 1)
        {
            modifikatorScore = 1;
        }

        modi.text = "x" + modifikatorScore;

        //Pri 3 zabitych nepratelich zvysi modifikator o 1
        if (killedEnemies == 3)
        {
            modifikatorScore += 1;
            killedEnemies = 0;
        }

        //modifikator 9 je max!
        if (modifikatorScore > 9)
        {            
            modifikatorScore = 9;
        }

        //Pocita dobu udrzeni modifikatoru x9
        if (modifikatorScore == 9)
        {
            GameManager.instance.GetComponent<TaskManager>().modifyTime += Time.deltaTime;

        }

        //V pripade ze neni modif.9 - nuluje veskery cas a uklada nejdelsi dobu, kterou v kole hrac ziskal
        else
        {
            if(GameManager.instance.GetComponent<TaskManager>().modifyTmp < GameManager.instance.GetComponent<TaskManager>().modifyTime)
            {
                GameManager.instance.GetComponent<TaskManager>().modifyTmp = GameManager.instance.GetComponent<TaskManager>().modifyTime;
            }
            GameManager.instance.GetComponent<TaskManager>().modifyTime = 0;
        }      

        //Kontrola Timeru pro modifikator 
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
        
    /// <summary>
    /// Aktualizuje skóre, modifikátor, počet zabitých nepřátel a časovač modifikátoru.
    /// </summary>
    /// <param name="score">Získané skóre</param>
    /// <param name="modifier">Úprava modifikátoru</param>
    /// <param name="enemy">Počet zabitých nepřátel</param>
    /// <param name="timer">Mám nastavit 5s časovač modifikátoru skóre?</param>
    public void UpdateScoreStuff(float score, int modifier, int enemy, bool timer)
    {
        AdjustScore(score);
        modifikatorScore += modifier;
        killedEnemies += enemy;
        if (timer)
        {
            FiveSecondsTimer();
        }
    }
    
    /// <summary>
    /// Upraví skóre o danou hodnotu
    /// </summary>
    /// <param name="value">Získané skóre</param>
    public void AdjustScore(float value)
    {
        tmpscore += value * modifikatorScore;
    }
    
    /// <summary>
    /// Nastaví timer modifikatoru skóre na 5s.
    /// </summary>
    public void FiveSecondsTimer()
    {
        fiveSecondsTimer = 5;
        fiveSecondsObject.SetActive(true);
        fiveSecondsSlider.value = fiveSecondsTimer;
    }
}
