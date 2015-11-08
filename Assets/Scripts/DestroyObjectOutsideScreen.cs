using UnityEngine;
using System.Collections;

public class DestroyObjectOutsideScreen : MonoBehaviour {

    public float destroyDistance;

	void Update ()
    {
        if (this.transform.position.x < destroyDistance)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
