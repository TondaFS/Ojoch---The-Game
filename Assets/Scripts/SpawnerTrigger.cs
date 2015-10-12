using UnityEngine;
using System.Collections;

public class SpawnerTrigger : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("end marker entered spawner trigger");
    }
}