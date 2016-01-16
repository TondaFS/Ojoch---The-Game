using UnityEngine;
using System.Collections;

public class BackgroundScrolling : MonoBehaviour
{
    public Vector2 speed = new Vector2(1, 1); // scrolling speed
    public Vector2 direction = new Vector2(-1, 0); // moving direction

    private SessionController sessionController;

    void Start()
    {
        sessionController = GameObject.FindWithTag("GameController").GetComponent<SessionController>();
    }

    void Update()
    {
        
          Vector3 movement = new Vector3(
          (speed.x + sessionController.gameSpeed) * direction.x,
          speed.y * direction.y,
          0);
        
        

        movement *= Time.deltaTime;
        transform.Translate(movement);

        //Debug.Log("bg scroll speed: " + movement.x);
    }
}
