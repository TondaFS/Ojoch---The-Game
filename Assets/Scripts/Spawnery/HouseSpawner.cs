using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HouseSpawner : MonoBehaviour {

    public GameObject[] houses; //seznam domu, ktere se muzou objevit   
     
    private float spawnDistance; //vzdalenost (ose x) ve ktere se budou spawnovat
    private float maxSpawnHeight; //vyska (osa y) ve ktere se budou domy spawnovat
    private float destroyDistance; //vzdalenost ve ktere se dum znici

    private List<GameObject> currentHouses = new List<GameObject>(); //list vsech aktualne spawnutych domu
    private float firstHousePos; //pozice domu uplne ve predu
    private float lastHousePos; //pozice domu uplne vzadu (posledni spawnuty)

    private GameObject randomHouse; //
    private Vector3 randomHousePos;

    void Start()
    {        
        spawnDistance = Camera.main.ViewportToWorldPoint(new Vector3(1, 0)).x; //measure position of point where houses will spawn
        destroyDistance = Camera.main.ViewportToWorldPoint(new Vector3(0, 0)).x; //measure position of point after which houses will be destroyed
        maxSpawnHeight = Camera.main.ViewportToWorldPoint(new Vector3(0, 0)).y; //measure position of point at which houses will be spawned

        GameObject firstRandomHouse = houses[Random.Range(0, houses.Length)]; //choose first house at random
        Vector3 firstRandomHousePos = new Vector3(spawnDistance, maxSpawnHeight); //choose position of first house
        currentHouses.Add((GameObject)Instantiate(firstRandomHouse, firstRandomHousePos, Quaternion.identity)); //instatiate first random house and add it to currentHouses ArrayList
    }

    void Update()
    {
        firstHousePos = currentHouses[0].GetComponent<SpriteRenderer>().bounds.max.x; //update position of first house
        lastHousePos = currentHouses[currentHouses.Count - 1].GetComponent<SpriteRenderer>().bounds.max.x; //update position of the last house
        
        //====================================================================================================//
        //Debugovaci cary
        Vector3 debugStart = currentHouses[currentHouses.Count - 1].GetComponent<SpriteRenderer>().bounds.min;
        Vector3 debugMid = currentHouses[currentHouses.Count - 1].GetComponent<SpriteRenderer>().bounds.center;
        Vector3 debugEnd = currentHouses[currentHouses.Count - 1].GetComponent<SpriteRenderer>().bounds.max;

        Debug.DrawLine(debugStart, new Vector3(), Color.red);
        Debug.DrawLine(debugMid, new Vector3(), Color.green);
        Debug.DrawLine(debugEnd, new Vector3(), Color.blue);

        Debug.DrawLine(new Vector3(spawnDistance, -10), new Vector3(spawnDistance, 10), Color.white);
        Debug.DrawLine(new Vector3(destroyDistance, -10), new Vector3(destroyDistance, 10), Color.white);
        //====================================================================================================//

        //if first house is out of screen remove it from list and destroy it
        if (firstHousePos < destroyDistance)
        {
            GameObject toDestroy = currentHouses[0];
            currentHouses.RemoveAt(0);
            Destroy(toDestroy);
        }

        //if last house move past spawn distance spawn new house and append it to the list
        if (lastHousePos < spawnDistance)
        {
            randomHouse = houses[Random.Range(0, houses.Length)];
            randomHousePos = new Vector3(spawnDistance, maxSpawnHeight - Random.Range(0f, 1f));
            currentHouses.Add((GameObject)Instantiate(randomHouse, randomHousePos, Quaternion.identity));
        }
    }
}