using UnityEngine;
using System.Collections;

public class InfinitePowerUpSpawner : MonoBehaviour {

    public GameObject powerup;
    public float respawnTime = 3;

	void Update () {
        
            respawnTime -= Time.deltaTime;
            if (respawnTime < 0)
            {
                respawnTime = 3;
                Instantiate(powerup, transform.position, Quaternion.identity);
            }
        
	}
}
