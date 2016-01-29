using UnityEngine;
using System.Collections;

public class CollideOnlyWithPlayer : MonoBehaviour {

    void Start()
    {
        Physics2D.IgnoreLayerCollision(9, 0, true);
        Physics2D.IgnoreLayerCollision(9, 11, true);
        Physics2D.IgnoreLayerCollision(9, 12, true);
        Physics2D.IgnoreLayerCollision(12, 12, true);
        Physics2D.IgnoreLayerCollision(11, 12, true);

    }
}
