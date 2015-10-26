using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour {

    public GameObject[] obstacles;
    public float coolDown;

    private float spawnCountdown = 0;
	
	// Update is called once per frame
	void Update () {
        spawnCountdown += Time.deltaTime;
        //Debug.Log("T = " + spawnCountdown);

        if (spawnCountdown > coolDown)
        {
            spawnCountdown = 0;
            Instantiate(obstacles[Random.Range(0, obstacles.Length)], new Vector3 (transform.position.x, transform.position.y + Random.Range(0,5)), new Quaternion());
        }
    }
}
