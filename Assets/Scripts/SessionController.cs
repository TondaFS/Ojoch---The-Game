using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SessionController : MonoBehaviour
{
    public static SessionController instance = null;

    public float gameSpeed = 1;
    public float speedUpTime = 50;

    #region GameObj references
    public bool pause;
    [HideInInspector]
    public GameObject pauseMenu;
    public bool ojochDead;
    [HideInInspector]
    public GameObject deathMenu;
    [HideInInspector]
    public GameObject newHighScoreText;
    [HideInInspector]
    public GameObject taskCompletedText;
    [HideInInspector]
    public GameObject deathScore;
    [HideInInspector]
    public GameObject highscoreMenu;
    [HideInInspector]
    public GameObject tasksMenu;
    [HideInInspector]
    public GameObject scoreNames;
    [HideInInspector]
    public GameObject scoreValues;
    [HideInInspector]
    GameObject taskOne;
    [HideInInspector]
    GameObject taskOneNew;
    [HideInInspector]
    GameObject taskTwo;
    [HideInInspector]
    GameObject taskTwoNew;
    [HideInInspector]
    GameObject taskThree;
    [HideInInspector]
    GameObject taskThreeNew;
    #endregion

    /// <summary>
    /// Počet nepřátel ve scéně
    /// </summary>
    public int numberOfEnemies;
    /// <summary>
    /// Seznam ptáků ve hře
    /// </summary>
    public List<GameObject> birdsInScene;
    /// <summary>
    /// Seznam krys ve hře
    /// </summary>
    public List<GameObject> ratsInScene;
    /// <summary>
    /// Seznam sputniků ve hře
    /// </summary>
    public List<GameObject> sputniksInScene;
    /// <summary>
    /// Seznam veverek ve hře
    /// </summary>
    public List<GameObject> squirrelsInScene;
    /// <summary>
    /// Seznam prasat ve hře
    /// </summary>
    public List<GameObject> pigsInScene;
    /// <summary>
    /// Refrence na bosse ve scéně
    /// </summary>
    public GameObject bossInScene;

    public GameObject Canvas;
    
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
        Canvas.SetActive(true);
    }
    void Start() {
        

        pauseMenu = GameObject.Find("PAUSE");
        pauseMenu.SetActive(false);
        taskCompletedText = GameObject.Find("taskCompleted");
        newHighScoreText = GameObject.Find("NewHighscore");
        deathMenu = GameObject.Find("DEATH");
        deathScore = GameObject.Find("rip");
        tasksMenu = GameObject.Find("TASKS");
        highscoreMenu = GameObject.Find("HIGHSCORE");
        scoreNames = GameObject.Find("scoreNames");
        scoreValues = GameObject.Find("scoreValues");

        

        taskOne = GameObject.Find("taskOne");
        taskOneNew = GameObject.Find("newOne");
        taskTwo = GameObject.Find("taskTwo");
        taskTwoNew = GameObject.Find("newTwo");
        taskThree = GameObject.Find("taskThree");
        taskThreeNew = GameObject.Find("newThree");
        

        taskOneNew.SetActive(false);
        taskTwoNew.SetActive(false);
        taskThreeNew.SetActive(false);
        tasksMenu.SetActive(false);
        highscoreMenu.SetActive(false);

        deathMenu.SetActive(false);
               
        pause = false;
        ojochDead = false;        
        GameObject.Find("Music").GetComponent<AudioSource>().volume = GameManager.instance.GetComponent<SoundManager>().musicVolume;
        GameManager.instance.GetComponent<SoundManager>().MuteEverything(false);        
    }

	void FixedUpdate () {
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

    /// <summary>
    /// Funkce přepíná pozastavení hry.
    /// <para>Pozastaví, popř. opět spustí hru -> vypnutí/zapnutí hudby, zobrazení menu.</para>
    /// </summary>
    /// <param name="bolean">Mám pozastavit hru?</param>
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

    /// <summary>
    /// Přepne mezi death Menu a Highscore
    /// </summary>
    /// <param name="change"></param>
    public void HighscoreMenu(bool change)
    {
        deathMenu.SetActive(!change);
        highscoreMenu.SetActive(change);

        if (change)
        {
            GameManager.instance.GetComponent<BestScores>().DisplayBestScores(scoreNames.GetComponent<Text>(), scoreValues.GetComponent<Text>());
        }
    }
        

    public void TasksMenu(bool change)
    {
        deathMenu.SetActive(!change);

        tasksMenu.SetActive(change);
        GameManager.instance.GetComponent<TaskManager>().DisplayAllTasks(taskOne.GetComponent<Text>(), taskTwo.GetComponent<Text>(), taskThree.GetComponent<Text>());
        CheckQuest();
    }

    /// <summary>
    /// Zkontroluje, jestli není nějaký úkol splněn. Pokud je, zobrazí pod ním tlačítko pro vytvoření nového.
    /// </summary>
    public void CheckQuest()
    {
        if (GameManager.instance.GetComponent<TaskManager>().activeTasks[0].completed)
        {
            taskOneNew.SetActive(true);
        }
        if (GameManager.instance.GetComponent<TaskManager>().activeTasks[0].completed)
        {
            taskTwoNew.SetActive(true);
        }
        if (GameManager.instance.GetComponent<TaskManager>().activeTasks[0].completed)
        {
            taskThreeNew.SetActive(true);
        }
    }

    /// <summary>
    /// Vytvoří nový úkol v dané sadě.
    /// </summary>
    /// <param name="questRow">Sada, ve které vytvoří nový úkol.</param>
    public void NewTask(int questRow)
    {
        GameManager.instance.GetComponent<TaskManager>().InitiateTask(GameManager.instance.GetComponent<TaskManager>().activeTasks[questRow].id + 1, questRow);
        switch (questRow)
        {
            case 0:
                GameManager.instance.GetComponent<TaskManager>().DisplayTaskOne(taskOne.GetComponent<Text>());
                taskOneNew.SetActive(false);
                break;
            case 1:
                GameManager.instance.GetComponent<TaskManager>().DisplayTaskTwo(taskTwo.GetComponent<Text>());
                taskTwoNew.SetActive(false);
                break;
            case 2:
                GameManager.instance.GetComponent<TaskManager>().DisplayTaskThree(taskThree.GetComponent<Text>());
                taskThreeNew.SetActive(false);
                break;
        }

        GameManager.instance.SaveData();
    }


}
