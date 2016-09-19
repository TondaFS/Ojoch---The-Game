using UnityEngine;
using System.Collections;

public class MissileScript : MonoBehaviour {

    private GameObject player;
    private Vector3 flightDirection;
    public float movementSpeed = 4;
    public bool homing = false;
    public bool spinning = false;
    public int health;
    public bool death;

    private float rotation;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        if(player != null){
            flightDirection = (player.transform.position - transform.position).normalized;
        }
        
        rotation = Random.Range(10, 20);
        death = false;

	}

	void Update () {
        if (homing && (player != null))
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(flightDirection * movementSpeed * Time.deltaTime);
        }

        if (spinning)
        {
            transform.GetChild(0).transform.Rotate(0, 0, rotation);
        }
        else
        {
            flightDirection = (player.transform.position - transform.position).normalized;
            float rot_z = Mathf.Atan2(flightDirection.y, flightDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 180);
        }

        if (health == 0)
        {
            GetComponentInChildren<Animator>().SetTrigger("shooted");
            GetComponent<Collider2D>().enabled = false;
            if (!death)
            {
                GameManager.instance.GetComponent<SoundManager>().PlaySound(GameManager.instance.GetComponent<SoundManager>().vodka);
                death = true;
            }
            Destroy(gameObject, 0.33f);
        }

    }

    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
        else
        {
            health--;
        }
    }
}
