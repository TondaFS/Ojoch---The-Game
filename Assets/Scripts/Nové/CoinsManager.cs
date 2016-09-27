using UnityEngine;
using System.Collections;

[System.Serializable]
public class CoinsManager : MonoBehaviour {
    public int coins;
    
    /// <summary>
    /// Upraví peníze v Ojochově pěněžence o danou hodnotu.
    /// </summary>
    /// <param name="value">Kolik peněz Ojoch získal nebo ztratil.</param>
    public void AjustCoins(int value)
    {
        GameManager.instance.GetComponent<SoundManager>().PlaySound(GameManager.instance.GetComponent<SoundManager>().clipGrab);
        coins += value;
    }    
}
