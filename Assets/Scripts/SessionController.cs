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
    
    void Start() {
        pauseMenu = GameObject.Find("PAUSE");
        pauseMenu.SetActive(false);
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
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PausingGame(false);   
            }
        }
        else if (ojochDead)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameObject.Find("OVERLAY").GetComponent<ScreenFader>().FadeOutLoadNewScene("highscore");
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
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
