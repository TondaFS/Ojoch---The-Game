using UnityEngine;
using System.Collections;

public class PowerUpID : MonoBehaviour {

    public int powerUpID = 0;

    public AnimationCurve wobble;

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
        Physics2D.IgnoreLayerCollision(11, 12, true);
        Physics2D.IgnoreLayerCollision(11, 0, true);
    }

    void Update()
    {        
        float newScaleX = originalScale.x + (wobble.Evaluate(Time.time * 3) / 40);
        float newScaleY = originalScale.y + (wobble.Evaluate((Time.time + 0.5f) * 3) / 40);
        transform.localScale = new Vector3(newScaleX, newScaleY);
    }
}
