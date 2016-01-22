using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SessionController : MonoBehaviour
{
    public float gameSpeed = 1;
    public float speedUpTime = 50;

    public Text highscoreText;
    
    void Start() {
        highscoreText.text = "Nejvyšší skóre: " + GameManager.instance.highscores.scores[0].name + " " + GameManager.instance.highscores.scores[0].score;
        GameObject.Find("Music").GetComponent<AudioSource>().volume = GameManager.instance.GetComponent<SoundManager>().musicVolume;
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject.Find("OVERLAY").GetComponent<ScreenFader>().FadeOutLoadNewScene("highscore");
            Time.timeScale = 1;
        }
    }
}
