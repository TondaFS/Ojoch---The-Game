using UnityEngine;

/// <summary>
/// Skript, který zavolá načtení vlny z Editor manageru
/// </summary>
public class WaveButtonScript : MonoBehaviour {
    /// <summary>
    /// Název vlny (souboru), který budeme později načítat
    /// </summary>
    public string nameOfWave = "";

    /// <summary>
    /// Zavolá editor manažer a načte nepřátelskou vlnu
    /// </summary>
    public void LoadXML()
    {
        EditorManager.Instance.LoadWave(nameOfWave);
       
    }
}
