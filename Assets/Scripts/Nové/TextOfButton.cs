using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SmartLocalization;

public class TextOfButton : MonoBehaviour {
    string buttonText;

    void Start()
    {
        GameManager.instance.languageManager.OnChangeLanguage += OnChangeLanguage;
        buttonText = GetComponent<Text>().text;
        GetComponent<Text>().text = GameManager.instance.languageManager.GetTextValue(buttonText);
    }
    
    void OnChangeLanguage(LanguageManager thisLanguageManager)
    {
        if(!(this == null))
        {
            GetComponent<Text>().text = GameManager.instance.languageManager.GetTextValue(buttonText);
        }        
    }
}
