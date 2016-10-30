using UnityEngine;
using System.Collections;

public class OjochAnimationsSetter : MonoBehaviour {
    /// <summary>
    /// Ojochova smrt. Spustí  EndGameScript, přehraje zvuk smrti a zpomalí hru. Nakonec Ojocha zničí.
    /// </summary>
    private void Death()
    {
        SessionController.instance.GetComponent<EndGameScript>().EndGame();
        Time.timeScale = 0.1f;
        GameObject.Find("Music").GetComponent<AudioSource>().mute = true;
        Debug.Log("Ahoj");
        Destroy(gameObject.transform.parent.gameObject);
    }
}
