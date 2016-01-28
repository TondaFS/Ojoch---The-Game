using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

    //Proměnné
    public Vector2 direction = new Vector2(0, 1);     //smer    
    private Vector2 movement; //pohyb

    public float speed;
    public AnimationCurve speedAnimation;
    private float creationTime;
    private Vector3 originalScale;


    

    void Start()
    {
        originalScale = transform.localScale;
        transform.localScale = new Vector3(0, 0, 1);
        direction = new Vector2(direction.x, direction.y + Random.Range(-0.1f, 0.1f));
        creationTime = Time.time;        
    }

    void Update()
    {
        transform.localScale = new Vector3(Mathf.Clamp(transform.localScale.x + 0.1f, 0, originalScale.x), Mathf.Clamp(transform.localScale.y + 0.1f, 0, originalScale.y), 1);

        float newSpeed = speedAnimation.Evaluate(Time.time - creationTime) * speed;
        
        movement = new Vector2(newSpeed * direction.x * Time.deltaTime, newSpeed * direction.y * Time.deltaTime);   //samotny pohyb
        transform.Translate(movement, 0);    
    }


}
