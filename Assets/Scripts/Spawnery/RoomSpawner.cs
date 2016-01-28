using UnityEngine;
using System.Collections;

public class RoomSpawner : MonoBehaviour {

    public GameObject[] rooms; //seznam mistnosti
    public float spawnHeight; //vyska (osa y) ve ktere se budou spawnovat
    public float spawnDistance; //vzdalenost (ose x) ve ktere se budou spawnovat 

    private GameObject currentRoom; //posledni spawnuta mistnost

    void Start()
    {
        SpawnFirstRoom(); //po spusteni hry spawni okamzite jednu mistnost
    }

    void Update()
    {
        //pokud je konec predchozi mistnosti ve spawnDistance, spawne se nova mistnost
        
        if (currentRoom.transform.GetChild(0).position.x < spawnDistance)
        {
            SpawnRoom();
        }
    }

    void SpawnRoom()
    {
        //vyber nahodnou mistnost ze seznamu
        GameObject room = rooms[Random.Range(1, rooms.Length)];

        //nastav pozici kde se bude spawnovat
        Vector3 spawnPos = new Vector3(spawnDistance, spawnHeight, 0);

        //instancuj mistnost
        currentRoom = Instantiate(room, spawnPos, Quaternion.identity) as GameObject;
        //currentRoom = transform.GetChild(0).gameObject.AddComponent<>();
    }

    void SpawnFirstRoom()
    {
        //vyber nahodnou mistnost ze seznamu
        GameObject room = rooms[0];

        //nastav pozici kde se bude spawnovat
        Vector3 spawnPos = new Vector3(spawnDistance, spawnHeight, 0);

        //instancuj mistnost
        currentRoom = Instantiate(room, spawnPos, Quaternion.identity) as GameObject;
        //currentRoom = transform.GetChild(0).gameObject.AddComponent<>();
    }
}