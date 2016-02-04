using UnityEngine;
using System.Collections;

public class DelayWaveSpawning : MonoBehaviour {

    public float delay;

    private EnemySpawner spawner;

    void Start()
    {
        spawner = gameObject.GetComponent<EnemySpawner>();
        spawner.enabled = false;
    }

    void Update()
    {
        delay -= Time.deltaTime;
        if (delay < 0)
        {
            spawner.enabled = true;
            this.enabled = false;
        }
    }
}
