using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SessionController : MonoBehaviour
{
    public float gameSpeed = 1;
    public float speedUpTime = 50;

    public bool pause;
    public GameObject pauseMenu;
    public bool ojochDead;

    public GameObject deathMenu;
    public GameObject newHighScoreText;
    public GameObject taskCompletedText;
    public GameObject ojoch;
      
    
    void Start() {
        pauseMenu = GameObject.Find("PAUSE");
        pauseMenu.SetActive(false);
        taskCompletedText = GameObject.Find("taskCompleted");
        taskCompletedText.SetActive(false);
        newHighScoreText = GameObject.Find("NewHighscore");
        newHighScoreText.SetActive(false);
        deathMenu = GameObject.Find("DEATH");
        deathMenu.SetActive(false);
               
        pause = false;
        ojochDead = false;        
        GameObject.Find("Music").GetComponent<AudioSource>().volume = GameManager.instance.GetComponent<SoundManager>().musicVolume;
        GameManager.instance.GetComponent<SoundManager>().MuteEverything(false);
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
        if (pause)
        {
            if (Input.GetButtonDown("Pause"))
            {
                PausingGame(false);   
            }
        }
        else if (ojochDead)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Pause"))
            {
                GameObject.Find("OVERLAY").GetComponent<ScreenFader>().FadeOutLoadNewScene("highscore");
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                GameObject.Find("OVERLAY").GetComponent<ScreenFader>().FadeOutLoadNewScene("level");
            }
        }
        else
        {
            if (Input.GetButtonDown("Pause"))
            {
                PausingGame(true);
            }
            
        }
        
    }

    public void PausingGame(bool bolean)
    {
        if (bolean)
        {
            Time.timeScale = 0;
            
        }
        else
        {
            Time.timeScale = 1;
        }
        GameManager.instance.GetComponent<SoundManager>().MuteEverything(bolean);
        pauseMenu.SetActive(bolean);
        pause = bolean;
    }
}
