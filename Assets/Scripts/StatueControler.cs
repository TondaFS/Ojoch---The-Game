using UnityEngine;
using System.Collections;

public class StatueControler : MonoBehaviour {

    float c;
	// Update is called once per frame
	void Update () {
        c += 0.2f;
        Vector2 sine = new Vector2(transform.position.x, 1 + Mathf.Sin(c));
        transform.position = sine;
    }
}
