using UnityEngine;
using System.Collections;

/// <summary>
/// Skript pohybující objekty ve hře
/// </summary>

public class MovingScriptNoRigidBody : MonoBehaviour {
    
    //Proměnné
    public float speed = 1;

    void Update() {
        transform.Translate(-speed * Time.deltaTime, 0, 0);
    }
}
