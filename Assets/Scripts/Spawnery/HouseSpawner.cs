using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HouseSpawner : MonoBehaviour {

    public GameObject[] houses; //seznam domu, ktere se muzou objevit    
    public float spawnDistance; //vzdalenost (ose x) ve ktere se budou spawnovat

    private float maxSpawnHeight; //vyska (osa y) ve ktere se budou spawnovat
    private float destroyDistance;

    private List<GameObject> currentHouses = new List<GameObject>();
    private float firstHousePos;
    private float lastHousePos;

    private GameObject randomHouse;
    private Vector3 randomHousePos;

    void Start()
    {        
        destroyDistance = Camera.main.ViewportToWorldPoint(new Vector3(0, 0)).x; //measure position of point after which houses will be destroyed
        maxSpawnHeight = Camera.main.ViewportToWorldPoint(new Vector3(0, 0)).y;

        Debug.Log("( " + destroyDistance + " , " + maxSpawnHeight + " )");

        GameObject firstRandomHouse = houses[Random.Range(0, houses.Length - 1)];
        Vector3 firstRandomHousePos = new Vector3(spawnDistance, maxSpawnHeight);
        currentHouses.Add((GameObject)Instantiate(firstRandomHouse, firstRandomHousePos, Quaternion.identity)); //instatiate first random house and add it to currentHouses ArrayList
    }

    void Update()
    {
        firstHousePos = currentHouses[0].GetComponent<SpriteRenderer>().bounds.max.x;
        lastHousePos = currentHouses[currentHouses.Count - 1].GetComponent<SpriteRenderer>().bounds.max.x;

        if (firstHousePos < destroyDistance)
        {
            GameObject toDestroy = currentHouses[0];
            currentHouses.RemoveAt(0);
            Destroy(toDestroy);
        }

        if (lastHousePos < spawnDistance)
        {
            randomHouse = houses[Random.Range(0, houses.Length - 1)];
            randomHousePos = new Vector3(spawnDistance, maxSpawnHeight - Random.Range(0f, 1f));
            currentHouses.Add((GameObject)Instantiate(randomHouse, randomHousePos, Quaternion.identity));
        }
    }
}