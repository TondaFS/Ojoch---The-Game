using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        // Make a button. We pass in the GUIStyle defined above as the style to use
        GUI.Label(new Rect(10, 10, 150, 20), "PRÁVĚ HRAJE:", label);
        GameManager.instance.playerName = GUI.TextField(new Rect(10, 30, 150, 20), GameManager.instance.playerName, 20, nameLabel);
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

    public void ChangeToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

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

    public void HowToPlay(bool change) {
        extraScreen.SetActive(!change);
        howto.SetActive(change);
        logo.SetActive(!change);
    }


    public void ExtraScreen(bool change)
    {
        menuScreen.SetActive(!change);
        extraScreen.SetActive(change);
    }

    public void AuthorsScreen(bool change) {
        authorsScreen.SetActive(change);
        extraScreen.SetActive(!change);
    }
}
