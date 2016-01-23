using UnityEngine;
using System.Collections;

[RequireComponent (typeof (SpriteRenderer))]
public class ShakeImg : MonoBehaviour
{
    public float shake;
    private SpriteRenderer sr;
    private Vector3 originalPosition;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

	// Tenhle script trepe se zakalenym pohledem
	void Update () {
        if (sr.color.a != 0)
        {
            float x = Random.Range(-0.9f, 0.9f);
            float y = Random.Range(0.5f, 1.5f);
            transform.position = Vector3.Lerp(transform.position, new Vector3(x, y), Time.deltaTime * shake);
        }
    }
}
