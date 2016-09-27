using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SmartLocalization;

public class MainMenuController : MonoBehaviour
{
    public GameObject logo;

    //menus
    GameObject menuScreen;
    GameObject settingScreen;
    GameObject extraScreen;
    GameObject authorsScreen;
    GameObject howto;
    GameObject bonus;
    GameObject stats;
    GameObject highscoreMenu;
    GameObject tasksMenu;

    public GameObject money;

    //Profile
    public GameObject nameChanger;
    public GameObject playername;

    //Stats
    GameObject statsDescription;
    GameObject statsValues;

    //Highscore
    GameObject scoreNames;
    GameObject scoreValues;

    //Tasks
    GameObject taskOne;
    GameObject taskOneNew;
    GameObject taskTwo;
    GameObject taskTwoNew;
    GameObject taskThree;
    GameObject taskThreeNew;

    //Bonus
    GameObject buyHatButton;
    GameObject activateButton;
    GameObject error;


    public bool changeSetting;

    public Slider music;
    public Slider sounds;   

    void Start() {
        money = GameObject.Find("Wallet");
        statsDescription = GameObject.Find("textStats");
        statsValues = GameObject.Find("numbersStats");
        scoreNames = GameObject.Find("scoreNames");
        scoreValues = GameObject.Find("scoreValues");

        menuScreen = GameObject.Find("GameMenu");
        menuScreen.SetActive(true);
        settingScreen = GameObject.Find("setMen");
        settingScreen.SetActive(false);
        extraScreen = GameObject.Find("Extra");
        extraScreen.SetActive(false);
        authorsScreen = GameObject.Find("screenAuthors");
        authorsScreen.SetActive(false);
        howto = GameObject.Find("HowToPlay");
        howto.SetActive(false);
        logo = GameObject.Find("logo");
        nameChanger = GameObject.Find("InputNameChange");
        nameChanger.SetActive(false);
        playername = GameObject.Find("PlayerName");

        //bonus
        bonus = GameObject.Find("bonusMenu");
        buyHatButton = GameObject.Find("BuyHat");
        activateButton = GameObject.Find("Active");
        error = GameObject.Find("error");

        buyHatButton.SetActive(false);
        activateButton.SetActive(false);
        bonus.SetActive(false);
        
        stats = GameObject.Find("Statistics");
        stats.SetActive(false);
        highscoreMenu = GameObject.Find("highscoreMenu");
        highscoreMenu.SetActive(false);

        //Tasks
        taskOne = GameObject.Find("taskOne");
        taskOneNew = GameObject.Find("newOne");
        taskTwo = GameObject.Find("taskTwo");
        taskTwoNew = GameObject.Find("newTwo");
        taskThree = GameObject.Find("taskThree");
        taskThreeNew = GameObject.Find("newThree");

        taskOneNew.SetActive(false);
        taskTwoNew.SetActive(false);
        taskThreeNew.SetActive(false);
        
        tasksMenu = GameObject.Find("taskMenu");
        tasksMenu.SetActive(false);   

        playername.GetComponent<Text>().text = GameManager.instance.playerName;
        
        music.value = (int)(GameManager.instance.GetComponent<SoundManager>().musicVolume * 10);
        sounds.value = (int)(GameManager.instance.GetComponent<SoundManager>().soundsVolume * 10);
    }

    void Update() {
        GetComponent<AudioSource>().volume = 0.2f * GameManager.instance.GetComponent<SoundManager>().musicVolume;

        if (changeSetting)
        {
            GameManager.instance.GetComponent<SoundManager>().musicVolume = music.value/10;
            GameManager.instance.GetComponent<SoundManager>().soundsVolume = sounds.value/10;
        }
        
    }

    /// <summary>
    /// Přechod do jiné scény
    /// </summary>
    /// <param name="sceneName">Jméno scény</param>
    public void ChangeToScene(string sceneName)
    {
        GameManager.instance.SaveData();
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Ukončí hru
    /// </summary>
    public void ExitGame()
    {
        GameManager.instance.GetComponent<GameStatistics>().stats.playedTime += Time.realtimeSinceStartup;
        GameManager.instance.SaveData();
        Application.Quit();
    }

    /// <summary>
    /// Přepíná mezi nastavením a menu Extra
    /// </summary>
    /// <param name="change">Mám ukázat nastavení?</param>
    public void SettingScreen(bool change)
    {
        extraScreen.SetActive(!change);
        settingScreen.SetActive(change);
        changeSetting = change;
        if (!change)
        {
            GameManager.instance.SaveData();
        }
    }

    /// <summary>
    /// Zobrazení: Jak hrát
    /// </summary>
    /// <param name="change">Mám zobrazit jak hrát?</param>
    public void HowToPlay(bool change) {
        extraScreen.SetActive(!change);
        howto.SetActive(change);
        logo.SetActive(!change);
    }

    /// <summary>
    /// Přepínání mezi Extra a Hlavním Menu
    /// </summary>
    /// <param name="change">Mám přepnout na Extra?</param>
    public void ExtraScreen(bool change)
    {
        menuScreen.SetActive(!change);
        extraScreen.SetActive(change);
    }

    /// <summary>
    /// Přepínání mezi Hlavním menu a Bonusy
    /// </summary>
    /// <param name="change">Mám přepnout na Bonusy?</param>
    public void BonusScreen(bool change)
    {
        menuScreen.SetActive(!change);
        bonus.SetActive(change);
        logo.SetActive(!change);
        money.GetComponentInChildren<Text>().text = GameManager.instance.GetComponent<CoinsManager>().coins.ToString();

        if (!change)
        {
            error.GetComponent<Text>().text = "";
        }

        if (GameManager.instance.GetComponent<BonusManager>().hatBought)
        {
            activateButton.SetActive(change);
            if (GameManager.instance.GetComponent<BonusManager>().ojochHasHat)
            {
                activateButton.GetComponentInChildren<Text>().text = GameManager.instance.languageManager.GetTextValue("Bonus.Deactivate");
            }
            else
            {
                activateButton.GetComponentInChildren<Text>().text = GameManager.instance.languageManager.GetTextValue("Bonus.Activate");
            }
        }
        else
        {
            buyHatButton.SetActive(change);
        }
    }

    /// <summary>
    /// Přepínání mezi Extra a Statistikami
    /// </summary>
    /// <param name="change">Mám přepnout na Statistiky?</param>
    public void StatsScreen(bool change)
    {
        extraScreen.SetActive(!change);
        stats.SetActive(change);
        if (change)
        {
            GameManager.instance.GetComponent<GameStatistics>().ShowStatistics(statsDescription.GetComponent<Text>(), statsValues.GetComponent<Text>());
        }
    }

    /// <summary>
    /// Přepne z Extra na Autory
    /// </summary>
    /// <param name="change">Mám přepnout na autory?</param>
    public void AuthorsScreen(bool change) {
        authorsScreen.SetActive(change);
        extraScreen.SetActive(!change);
    }

    /// <summary>
    /// Změní jazyk hry.
    /// </summary>
    /// <param name="language">true: čeština, false: eng</param>
    public void ChangeLanguage(bool language)
    {
        if (language)
        {
            GameManager.instance.languageManager.ChangeLanguage("cs-CZ");
            GameManager.instance.activeLanguage = "cs-CZ";
        }
        else
        {
            GameManager.instance.languageManager.ChangeLanguage("en-GB");
            GameManager.instance.activeLanguage = "en-GB";
        }
    }    

    /// <summary>
    /// Zobrazí textové pole pro zadání hráčova jména a uloží jej do GameManageru pro použití u skóre
    /// </summary>
    /// <param name="active">Mám ho zobrazit?</param>
    public void InputFieldActivator(bool active)
    {
        nameChanger.SetActive(active);

        if (!active)
        {
            playername.GetComponent<Text>().text = nameChanger.GetComponent<InputField>().text;
            GameManager.instance.playerName = playername.GetComponent<Text>().text;
        }
    }

    /// <summary>
    /// Přepne z menu na Tabulku nejlepších.
    /// </summary>
    /// <param name="change">Mám přepnout na tabulku nejlepších?</param>
    public void HighscoreScreen(bool change)
    {
        highscoreMenu.SetActive(change);
        menuScreen.SetActive(!change);
        logo.SetActive(!change);

        if (change)
        {
            GameManager.instance.GetComponent<BestScores>().DisplayBestScores(scoreNames.GetComponent<Text>(), scoreValues.GetComponent<Text>());            
        }
    }

    /// <summary>
    /// Přepne z menu do nabídky s Úkoly.
    /// </summary>
    /// <param name="change"></param>
    public void TasksMenu(bool change)
    {
        menuScreen.SetActive(!change);        
        logo.SetActive(!change);

        tasksMenu.SetActive(change);
        GameManager.instance.GetComponent<TaskManager>().DisplayAllTasks(taskOne.GetComponent<Text>(), taskTwo.GetComponent<Text>(), taskThree.GetComponent<Text>());
        CheckQuest();
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



    //Prozatím
    public void BuyHat()
    {
        if(GameManager.instance.GetComponent<CoinsManager>().coins >= 20)
        {
            GameManager.instance.GetComponent<CoinsManager>().coins -= 20;
            GameManager.instance.GetComponent<BonusManager>().hatBought = true;
            money.GetComponentInChildren<Text>().text = GameManager.instance.GetComponent<CoinsManager>().coins.ToString();
            buyHatButton.SetActive(false);
            activateButton.SetActive(true);
            activateButton.GetComponentInChildren<Text>().text = GameManager.instance.languageManager.GetTextValue("Bonus.Activate");
            GameManager.instance.SaveData();
        }
        else
        {
            error.GetComponent<Text>().text = GameManager.instance.languageManager.GetTextValue("Bonus.Error"); 
        }
    }

    public void ActivateHat()
    {
        GameManager.instance.GetComponent<BonusManager>().ojochHasHat = !GameManager.instance.GetComponent<BonusManager>().ojochHasHat;
        if (GameManager.instance.GetComponent<BonusManager>().ojochHasHat)
        {
            activateButton.GetComponentInChildren<Text>().text = GameManager.instance.languageManager.GetTextValue("Bonus.Deactivate");            
        }
        else
        {
            activateButton.GetComponentInChildren<Text>().text = GameManager.instance.languageManager.GetTextValue("Bonus.Activate");            
        }
        GameManager.instance.SaveData();
    }
}
