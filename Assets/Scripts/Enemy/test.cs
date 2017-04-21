using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

	void Update () {
        Debug.Log(Mathf.Sin(Time.time));
        transform.position += new Vector3(0, Mathf.Cos(Time.time)/10, 0);
	}
}
