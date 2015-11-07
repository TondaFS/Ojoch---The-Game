using UnityEngine;
using System.Collections;

public class RoomSpawner : MonoBehaviour {

    public GameObject[] rooms;
    public float spawnHeight;
    public float spawnDistance;
    
    private GameObject room;
    private GameObject currentRoom;

    void Start()
    {
        SpawnRoom();
    }
    
    void Update()
    {
        if(spawnDistance > currentRoom.transform.GetChild(0).position.x)
        {
            SpawnRoom();
        }
    }

    void SpawnRoom()
    {
        room = rooms[Random.Range(0, rooms.Length)];
        Vector3 spawnPos = new Vector3(spawnDistance , spawnHeight, 0);
        currentRoom = Instantiate(room, spawnPos, Quaternion.identity) as GameObject;
        Debug.Log("ROOM SPAWNED");
    }

}
