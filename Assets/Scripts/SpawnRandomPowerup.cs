using UnityEngine;
using System.Collections;

public class SpawnRandomPowerup : MonoBehaviour
{

    public GameObject[] powerup;

    void Start()
    {
        GameObject randomPowerup = powerup[Random.Range(0, powerup.Length)];
        //Debug.Log(randomPowerup);
        Vector3 spawnPos = (gameObject.transform.position);

        if (randomPowerup != null)
        {
            (Instantiate(randomPowerup, spawnPos, Quaternion.identity) as GameObject).transform.parent = transform; ;
        }
        //Destroy(gameObject);
    }

}
