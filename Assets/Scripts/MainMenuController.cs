using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SmartLocalization;

public class MainMenuController : MonoBehaviour
{
    public GameObject menuScreen;
    public GameObject settingScreen;
    public GameObject extraScreen;
    public GameObject authorsScreen;
    public GameObject howto;
    public GameObject logo;
    public bool changeSetting;

    public Slider music;
    public Slider sounds;   

    public GUIStyle label;
    public GUIStyle nameLabel;

    void OnGUI()
    {

        GUI.Label(new Rect(10, 10, 150, 50), "PRÁVĚ HRAJE:", label);
        GameManager.instance.playerName = GUI.TextField(new Rect(10, 40, 200, 50), GameManager.instance.playerName, 15, nameLabel);
    }

    void Start() {
        //mainMenu = true;
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
        }
        else
        {
            GameManager.instance.languageManager.ChangeLanguage("en-GB");
        }
    }    
}
