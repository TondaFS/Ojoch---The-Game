using UnityEngine;
using System.Collections;

public class MoveImg : MonoBehaviour
{
    public bool startOnScreen;
    public float scrollSpeed;

    private float tileSize;
    private Vector3 startPosition;
    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();

        tileSize = sr.bounds.size.y;
        transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0 , 0, 10));
        if (startOnScreen)
        {
            transform.Translate(new Vector3(0, 0));
        }
        else
        {
            transform.Translate(new Vector3(tileSize, 0));
        }
        startPosition = transform.position;
    }

    void Update()
    {
        if (sr.color.a != 0)
        {
            float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSize);
            transform.position = startPosition + Vector3.up * newPosition;

            Color maxAlpha = sr.color;
            maxAlpha.a = 0.5f;
            sr.color = maxAlpha;
        }    
    }    
}
