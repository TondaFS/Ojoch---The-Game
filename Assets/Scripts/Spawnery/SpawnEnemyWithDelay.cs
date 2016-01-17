using UnityEngine;
using System.Collections;

public class SpawnEnemyWithDelay : MonoBehaviour {
    public GameObject enemy;
    public float delay = 1;

    private bool spawned = false;

    void Update()
    {
        delay -= Time.deltaTime;

        if (delay < 0 && !spawned)
        {
            //Debug.Log(gameObject.transform.position);
            Vector3 spawnPos = (gameObject.transform.position);
            
            (Instantiate(enemy, spawnPos, Quaternion.identity) as GameObject).transform.parent = transform;
            spawned = true;
            //Destroy(gameObject);
        }
    }
}
