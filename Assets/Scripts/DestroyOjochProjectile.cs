using UnityEngine;
using System.Collections;

public class DestroyOjochProjectile : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("it works");

        Debug.Log(coll.gameObject.layer);
        if (coll.gameObject.layer == LayerMask.NameToLayer("OjochProjectile"))
        {
            Destroy(coll.gameObject);
        }
    }
}
