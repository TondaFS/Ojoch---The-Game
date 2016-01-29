using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {

    public List<Transform> waves = new List<Transform>();

    private List<Transform> currentEnemies = new List<Transform>();
    private List<Transform> currentBosses = new List<Transform>();
    private List<Transform> currentPowerUps = new List<Transform>();

    private float spawnDistance = 10;
    //private float destroyDistance;

    void Start()
    {
        //destroyDistance = Camera.main.ViewportToWorldPoint(new Vector3(0, 0)).x - 2;
    }

    void Update ()
    {       

        currentEnemies.RemoveAll(item => item == null);
        currentBosses.RemoveAll(item => item == null);

        Debug.Log(currentBosses.Count);

        if (currentEnemies.Count < 2 && currentBosses.Count == 0)
        {
            Transform newWave = waves[Random.Range(0, waves.Count)];
            Transform[] newWaveChildren = newWave.GetComponentsInChildren<Transform>(true);

            foreach (Transform child in newWaveChildren)
            {
                
                if (child.tag == "Enemy")
                {
                    Transform newEnemy = (Transform) Instantiate(child, new Vector3(child.position.x, child.position.y), Quaternion.identity);
                    currentEnemies.Add(newEnemy);
                }

                if (child.tag == "Boss")
                {
                    Transform newBoss = (Transform) Instantiate(child, new Vector3(child.position.x, child.position.y), Quaternion.identity);
                    currentBosses.Add(newBoss);
                }

                if (child.tag == "PowerUp")
                {
                    Debug.Log(child + " spawned!");
                    Transform newPowerUp = (Transform) Instantiate(child, new Vector3(child.position.x, child.position.y), Quaternion.identity);
                    currentPowerUps.Add(newPowerUp);
                }                 
            }              
        }
    }
}
