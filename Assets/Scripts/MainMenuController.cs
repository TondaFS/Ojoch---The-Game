using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject menuScreen;
    public GameObject settingScreen;
    public GameObject extraScreen;
    public GameObject authorsScreen;
    public GameObject howto;
    public GameObject logo;
    public bool changeSetting;
    //public bool mainMenu;

    public Slider music;
    public Slider sounds;

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
        Application.LoadLevel(sceneName);
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
