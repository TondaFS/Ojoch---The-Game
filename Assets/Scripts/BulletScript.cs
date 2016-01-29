using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

    //Proměnné
    public Vector2 direction;     //smer    
    private Vector2 movement;                         //pohyb

    public float speed;                               //rychlost
    private Vector3 originalScale;                    //max velikost    

    void Start()
    {
        originalScale = transform.localScale;
        transform.localScale = new Vector3(0, 0, 1);
        direction = new Vector2(direction.x, direction.y + Random.Range(-0.1f, 0.1f));  
    }

    void Update()
    {
        transform.localScale = new Vector3(Mathf.Clamp(transform.localScale.x + (0.8f * Time.deltaTime), 0, originalScale.x), Mathf.Clamp(transform.localScale.y + (0.8f * Time.deltaTime), 0, originalScale.y), 1);
                
        movement = new Vector2(speed * direction.x * Time.deltaTime, speed * direction.y * Time.deltaTime);   //samotny pohyb
        transform.Translate(movement, 0);    
    }


}
