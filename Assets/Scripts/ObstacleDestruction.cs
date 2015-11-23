using UnityEngine;
using System.Collections;

public class ObstacleDestruction : MonoBehaviour {

    public bool destroy = false;
    public float countdown = 0.5f;

    private bool startCountdown = false;

    void Update()
    {
        if (destroy)
        {
            Destruction();            
        }
        if (startCountdown)
        {
            countdown -= Time.deltaTime;
        }
        if (countdown <= 0)
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (countdown <= countdown - 2)
        {
            Destroy(this.gameObject);
        }         
    }

    public void Destruction()
    {
        foreach (Transform child in this.gameObject.transform) if (child.CompareTag("Particle"))
        {
            child.GetComponent<ParticleSystem>().Play();
        }
        startCountdown = true;
        destroy = false;
    }
}
