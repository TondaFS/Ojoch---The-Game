using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    public void ChangeToScene(string sceneName)
    {
        Application.LoadLevel(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
