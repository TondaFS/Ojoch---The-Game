using UnityEngine;
using System.Collections;

public class PowerUpSpawner : MonoBehaviour {

    public Transform[] powerUps;

    void Start()
    {
        Transform powerUp = powerUps[Random.Range(0,powerUps.Length)];
        Instantiate(powerUp, transform.position, Quaternion.identity);
    }
}
