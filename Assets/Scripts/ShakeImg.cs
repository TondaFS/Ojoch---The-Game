using UnityEngine;
using System.Collections;

public class ShakeImg : MonoBehaviour {
    public float shake;
    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

	// Tenhle script trepe se zakalenym pohledem
	void Update () {
        if (sr.color.a != 0)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(Random.Range(-shake, shake), Random.Range(-shake, shake) + 1), Time.deltaTime);
        }
    }
}
