using UnityEngine;
using System.Collections;

[RequireComponent (typeof (SpriteRenderer))]
public class BGScroller : MonoBehaviour
{
    public bool startOnScreen;
    public float startingScrollSpeed;

    private float tileSize;
    private Vector3 startPosition;
    private SessionController sessionController;

    void Awake()
    {
        sessionController = GameObject.FindWithTag("GameController").GetComponent<SessionController>();

        tileSize = GetComponent<SpriteRenderer>().bounds.size.x;
        transform.position = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 10));
        if (startOnScreen)
        {
            transform.Translate(new Vector3(-tileSize + 5, 0));
        }
        else
        {
            transform.Translate(new Vector3(5, 0));
        }
        startPosition = transform.position;
    }

    void Update()
    {

        //float newPosition = Mathf.Repeat(Time.time * (startingScrollSpeed + sessionController.gameSpeed), tileSize);
        float newPosition = Mathf.Repeat(Time.time * startingScrollSpeed, tileSize);
        transform.position = startPosition + Vector3.left * newPosition;

    }
}
