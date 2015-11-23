using UnityEngine;
using System.Collections;

public class SpawnRandomPowerup : MonoBehaviour
{

    public GameObject[] powerup;

    void Start()
    {
        GameObject randomPowerup = powerup[Random.Range(0, powerup.Length)];
        Vector3 spawnPos = (gameObject.transform.position);

        Instantiate(randomPowerup, spawnPos, Quaternion.identity);
        Destroy(gameObject);
    }

}
