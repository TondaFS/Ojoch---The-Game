using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    void Start() {
        GetComponent<AudioSource>().volume = 0.2f * GameManager.instance.GetComponent<SoundManager>().musicVolume;
    }

    void Update() {
        GetComponent<AudioSource>().volume = 0.2f * GameManager.instance.GetComponent<SoundManager>().musicVolume;
    }

    public void ChangeToScene(string sceneName)
    {
        Application.LoadLevel(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
