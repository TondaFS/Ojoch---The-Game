using UnityEngine;
using System.Collections;

public class PositionDetector : MonoBehaviour {

    public Camera camera;
    public GameObject nextObjectToSpawn;

    private bool spawnedNew = false;

    void Update () {
        //Debug.Log("X = " + transform.position.x);
        Vector3 position = camera.WorldToViewportPoint(transform.position);
        Debug.Log("X screen =" + position.x);

        if (position.x < 1.2f && spawnedNew == false)
        {
            Instantiate(nextObjectToSpawn, transform.position, new Quaternion());
            spawnedNew = true;
        }

        if (position.x < -0.2f)
        {
            Destroy(transform.parent.gameObject);
            Debug.Log("is it BOOM?");
        }
    }
}
