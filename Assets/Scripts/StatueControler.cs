using UnityEngine;
using System.Collections;

public class StatueControler : MonoBehaviour {
    
    //Statue movement
    public float howMuchForward = 0;
    public float howMuchBack = 0;
    //float c;

    void Update() {
        if (howMuchForward > 0)
        {            
            transform.Translate(0.01f, 0, 0);
            howMuchForward -= Time.deltaTime;
           
        }

        if (howMuchBack > 0)
        {
            transform.Translate(-0.02f, 0, 0);
            howMuchBack -= Time.deltaTime;
            if (transform.position.x <= -6.2f)
            {
                howMuchBack = 0;
            }
        }

        /*
        c += 0.2f;
        Vector2 sine = new Vector2(transform.position.x, 1 + Mathf.Sin(c));
        transform.position = sine;
        */
    }



    
    
	
	
}
