using UnityEngine;
using System.Collections;

public class testDebugLog : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Debug.DrawLine(this.transform.position, new Vector3(0, 10, 0));
    }
}
