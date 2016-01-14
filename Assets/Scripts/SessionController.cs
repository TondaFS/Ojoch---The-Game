using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SessionController : MonoBehaviour
{
    public float gameSpeed = 1;
    public float speedUpTime = 50;

    public Text highscoreText;

    /*
    public int score;
    public int scoreMultiplier;
    public int highscore;*/

    void Start() {
        highscoreText.text = "Nejvyšší skóre: " + GameManager.instance.highscore;
    }

	void FixedUpdate () {
        //Debug.Log("Game runtime: " + Time.time);
        gameSpeed += Time.deltaTime / speedUpTime;

        if (gameSpeed < 1)
        {
            gameSpeed = 1;
        }

        if (speedUpTime < 10)
        {
            speedUpTime = 10;
        }

        if(speedUpTime > 80)
        {
            speedUpTime = 80;
        }
    }


}
