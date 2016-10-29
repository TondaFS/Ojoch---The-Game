using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SmartLocalization;

/// <summary>
/// Skritp přiřadí text na základě aktuálního jazyka z databáze. Reference na odpovídající text je v komponentě objektu Text.text. 
/// </summary>
public class TextOfButton : MonoBehaviour {
    /// <summary>
    /// Text, který se má na talčítku objevit.
    /// </summary>
    string buttonText;

    /// <summary>
    /// Bere referenci z text komponenty a přiřadí text na základě aktuálního jazyka.
    /// </summary>
    void Start()
    {
        GameManager.instance.languageManager.OnChangeLanguage += OnChangeLanguage;
        buttonText = GetComponent<Text>().text;
        GetComponent<Text>().text = GameManager.instance.languageManager.GetTextValue(buttonText);
    }
    
    /// <summary>
    /// Při změně jazyka změní text tlačítka.
    /// </summary>
    /// <param name="thisLanguageManager">LanguageManager, na kterém se kontroluje, zda došlo ke změně jazyka.</param>
    void OnChangeLanguage(LanguageManager thisLanguageManager)
    {
        if(!(this == null))
        {
            GetComponent<Text>().text = GameManager.instance.languageManager.GetTextValue(buttonText);
        }        
    }
}
