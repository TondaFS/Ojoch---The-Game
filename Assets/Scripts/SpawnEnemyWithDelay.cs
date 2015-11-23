using UnityEngine;
using System.Collections;

public class SpawnEnemyWithDelay : MonoBehaviour {
    public GameObject enemy;
    public float delay = 1;

    void Update()
    {
        delay -= Time.deltaTime;

        if (delay < 0)
        {
            //Debug.Log(gameObject.transform.position);
            Vector3 spawnPos = (gameObject.transform.position);

            Instantiate(enemy, spawnPos, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
