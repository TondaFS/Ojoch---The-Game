using UnityEngine;
using System.Collections;

public class CollideOnlyWithPlayer : MonoBehaviour {

    void Start()
    {
        Physics2D.IgnoreLayerCollision(9, 0, true);
    }
}
