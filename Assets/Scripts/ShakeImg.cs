using UnityEngine;
using System.Collections;

public class ShakeImg : MonoBehaviour {
    public float shake;
	// Tenhle script trepe se zakalenym pohledem
	void Update () {
        transform.position = Vector3.Lerp(transform.position, new Vector3(Random.Range(-shake, shake), Random.Range(-shake, shake) + 1), Time.deltaTime);
	}
}
