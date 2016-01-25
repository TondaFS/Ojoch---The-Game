using UnityEngine;
using System.Collections;

public class PlacePrefab : MonoBehaviour {

    public GameObject prefab;

	void Awake ()
    {
        GameObject go = (GameObject) Instantiate(prefab, transform.position, transform.rotation);
        go.transform.parent = transform.parent;
        Destroy(gameObject);
    }
}
