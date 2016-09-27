using UnityEngine;
using System.Collections;

public class PowerUpSpawnerDestroyer : MonoBehaviour
{
    void Start () {
        Destroy(this.gameObject, 5);
	}	
}
