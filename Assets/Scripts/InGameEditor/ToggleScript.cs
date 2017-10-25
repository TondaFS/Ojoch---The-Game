using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script na Togglu spravuje, ze pokud je vybran, nastavi spravnou obtiznost na referencni objekt vlny
/// </summary>
public class ToggleScript : MonoBehaviour {
    /// <summary>
    /// reference na toggle script objektu
    /// </summary>
    Toggle t;
    /// <summary>
    /// odpovidajici obtiznost u togglu
    /// </summary>
    public WaveDifficulty difficulty;
	
    void Start()
    {
        t = GetComponent<Toggle>();
    }

    /// <summary>
    /// Pokud je obtiznost vybrana, upravim obtiznost vlny na ref. objektu
    /// </summary>
    public void OnValueChanged()
    {
        if (t.isOn)
        {
            EditorManager.Instance.UpdateDifficulty(difficulty);
        }
            
    }
}
