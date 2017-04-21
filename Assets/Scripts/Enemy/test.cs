using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {
    public float number;
    private float startPosition;
    private float anotherNumber;

    void Start()
    {
        startPosition = transform.position.y;
        if(startPosition > 4)
        {
            startPosition = 4;
        }

        if(startPosition < -3)
        {
            startPosition = -3;
        }
        anotherNumber = Mathf.Asin(startPosition / 4) / number;
    }

	void Update () {
        //Debug.Log(Mathf.Sin(Time.time));
        anotherNumber += 0.1f;
        transform.position = new Vector3(transform.position.x, Mathf.Sin(anotherNumber*number)*4+1, 0);
        //transform.position += new Vector3(0, Mathf.Cos(number)/10, 0);
        //number += 0.1f;
    }
}
