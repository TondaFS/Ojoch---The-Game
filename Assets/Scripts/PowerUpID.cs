using UnityEngine;
using System.Collections;

public class PowerUpID : MonoBehaviour {

    public int powerUpID = 0;

    void Start()
    {
        Physics2D.IgnoreLayerCollision(11, 12, true);
        Physics2D.IgnoreLayerCollision(11, 0, true);
    }
}
