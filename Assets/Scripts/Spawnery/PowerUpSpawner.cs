using UnityEngine;
using System.Collections;

public class PowerUpSpawner : MonoBehaviour
{

    [Range(0, 100)]
    public float chanceOfSpawn; //sance, ze se spawne nejaky powerup
    [Range(0, 0.1f)]
    public float increaseOfChance; 
    public GameObject[] powerUps; //seznam powerUpu, ktere se muzou objevit   

    //tyto dva vectory definuji oblast kde se spawnujou objekty
    private Vector3 spawnAreaDL;
    private Vector3 spawnAreaTR;


    void Start()
    {
        float spawnAreaD = Camera.main.ViewportToWorldPoint(new Vector3(1, 0)).x + 1;
        float spawnAreaL = Camera.main.ViewportToWorldPoint(new Vector3(1, 0)).y;
        float spawnAreaT = Camera.main.ViewportToWorldPoint(new Vector3(1, 1)).x + 5;
        float spawnAreaR = Camera.main.ViewportToWorldPoint(new Vector3(1, 1)).y;

        spawnAreaDL = new Vector3(spawnAreaD, spawnAreaL);
        spawnAreaTR = new Vector3(spawnAreaT, spawnAreaR);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(spawnAreaDL, spawnAreaTR, Color.cyan);              

        if (Random.Range(0f, 100f) < chanceOfSpawn)
        {
            chanceOfSpawn = 0;
            SpawnRandomPowerUp();
        }
    }

    void FixedUpdate()
    {
        chanceOfSpawn += increaseOfChance;
    }

    void SpawnRandomPowerUp()
    {
        GameObject randomPowerUp = powerUps[Random.Range(0, powerUps.Length)];
        float randomPowerUpPosX = Random.Range(spawnAreaDL.x, spawnAreaTR.x);
        float randomPowerUpPosY = Random.Range(spawnAreaDL.y, spawnAreaTR.y);
        Vector3 randomPowerUpPos = new Vector3(randomPowerUpPosX, randomPowerUpPosY);

        Instantiate(randomPowerUp, randomPowerUpPos, Quaternion.identity);
    }
}