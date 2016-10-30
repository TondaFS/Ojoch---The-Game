using UnityEngine;
using System.Collections;

public class Coins : MonoBehaviour {
    public int value = 0; 
    //public float scale;

    void Start()
    {
        Destroy(this.gameObject, 20);
    }
}
