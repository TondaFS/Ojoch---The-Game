using UnityEngine;
using System.Collections;

public class SpinSocks : MonoBehaviour {

    void OnEnable()
    {
        SpriteRenderer[] socks = gameObject.transform.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sock in socks)
        {
            sock.enabled = true;
        }

    }

	void Update ()
    {
            transform.Rotate(Vector3.forward, 10);            	
	}

    void OnDisable()
    {
        SpriteRenderer[] socks = gameObject.transform.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sock in socks)
        {
            sock.enabled = false;
        }
    }
}
