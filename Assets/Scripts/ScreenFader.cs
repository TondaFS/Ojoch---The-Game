using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScreenFader : MonoBehaviour
{
    public Image fadeImg;
    public float fadeSpeed = 2f;
    public bool isSceneStarting = true;
    public bool isSceneEnding = false;

    private string newScene;

    void Update()
    {
        //Debug.Log(fadeImg.color.a + "\n" + (fadeSpeed * Time.deltaTime) / (fadeImg.color.a + 0.1f));

        if (isSceneStarting)
        {
            FadeIn();
        }
        else if (isSceneEnding)
        {
            FadeOut();
        }        
    }

    void FadeIn()
    {
        fadeImg.color = Color.Lerp(fadeImg.color, Color.clear, (fadeSpeed * Time.deltaTime) / (fadeImg.color.a + 0.1f));
        
        if (fadeImg.color.a <= 0.05f)
        {
            fadeImg.color = Color.clear;
            fadeImg.enabled = false;
            
            isSceneStarting = false;
        }
    }

    void FadeOut()
    {
        fadeImg.color = Color.Lerp(fadeImg.color, Color.black, (fadeSpeed * Time.deltaTime) / (fadeImg.color.a + 0.1f));

        if(fadeImg.color.a >= 0.95f)
        {
            fadeImg.color = Color.black;

            isSceneEnding = false;
            SceneManager.LoadScene(newScene);
        }
    }

    public void FadeOutLoadNewScene(string newScene)
    {
        Time.timeScale = 1;        
        fadeImg.enabled = true;
        isSceneEnding = true;        
        this.newScene = newScene;
    }
}