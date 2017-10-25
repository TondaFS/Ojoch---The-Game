using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ma nas starost warning menu pri ukladani, uschovava v sobe cestu a vlnu, kterou chtel hrac ulozit
/// </summary>
public class WarningScript : MonoBehaviour {
    /// <summary>
    /// cesta k ulozeni
    /// </summary>
    public string path;
    /// <summary>
    /// vlna k ulozeni
    /// </summary>
    public WaveXML xml;

    void Update()
    {
        ///zmacknu li esc, tak zrusim warning menu
        if (Input.GetKeyDown(KeyCode.Escape))
            Cancel();
    }

    /// <summary>
    /// Zavola metodu, ktera ulozi vlnu do souboru
    /// </summary>
    /// <param name="pathToSave">Cesta kam ulozit</param>
    /// <param name="wave">Vlna k ulozeni</param>
    public void SaveOverride()
    {
        EditorManager.Instance.Serialize(path, xml);
        Cancel();
    }

    /// <summary>
    /// Zrusi ulozeni vlny a deaktivuje warning message
    /// </summary>
    public void Cancel()
    {
        path = "";
        xml = null;
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// Creates new wave and hides the warnign message
    /// </summary>
    public void YesCreateNew()
    {
        EditorManager.Instance.WaveReferencePoint.isSaved = true;
        EditorManager.Instance.CallCreateNewWave();
        EditorManager.Instance.NewWarningMenu.SetActive(false);
    }

    /// <summary>
    /// hides warning message
    /// </summary>
    public void NoCreateNew()
    {
        EditorManager.Instance.NewWarningMenu.SetActive(false);
    }
}
