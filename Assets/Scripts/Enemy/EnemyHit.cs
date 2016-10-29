using UnityEngine;
using System.Collections;

/// <summary>
/// Skript zajistí, aby nepřítel po zásahu na chvíli zčervenal
/// </summary>
public class EnemyHit : MonoBehaviour {
    /// <summary>
    /// Dostal nepřítel zásah?
    /// </summary>
    public bool isHit;
    /// <summary>
    /// Jakou rychle se bude měnit barva na původní
    /// </summary>
    public float speedOfChange = 1f;
    /// <summary>
    /// Čas, kdy nepřítel dostal zásah
    /// </summary>
    public float timeOfHit;

	void Start () {
        isHit = false;
	}	
	
	void Update () {             
	    if (isHit)
        {
            ChangeColor();
        }      
	}

    /// <summary>
    /// Mění postupně barvu z červené na původní
    /// </summary>
    public void ChangeColor()
    {
        float actualColor = this.GetComponent<SpriteRenderer>().color.g + ( speedOfChange * Time.deltaTime);
        if (actualColor >= 1)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
            isHit = false;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().color = new Color(1, actualColor, actualColor);
        }
    }

    /// <summary>
    /// Nastavtaví baru na červenou
    /// </summary>
    public void SetRedColor()
    {
        Debug.Log("Setting red");
        this.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
    }
}
