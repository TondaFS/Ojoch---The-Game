using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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

        if (Input.GetKeyDown(KeyCode.Escape)) {
            FadeOutLoadNewScene("menu");
            Time.timeScale = 1;
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
            Application.LoadLevel(newScene);
        }
    }

    public void FadeOutLoadNewScene(string newScene)
    {
        fadeImg.enabled = true;
        isSceneEnding = true;
        this.newScene = newScene;
    }
}