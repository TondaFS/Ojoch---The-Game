using UnityEngine;
using System.Collections;

public class OjochManager : MonoBehaviour {
    public static OjochManager instance = null;

    public OjochScript ojochScript;
    public HealthScript ojochHealth;
    public WeaponScript ojochWeapon;
    public CollectingScript ojochCollect;
    public PowerUpScript ojochPowerUp;
    public OjochCollisions ojochCollision;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if(instance != this)
        {
            Destroy(this.gameObject);
        }
    } 

    void Start () {
        ojochScript = GetComponent<OjochScript>();
        ojochHealth = GetComponent<HealthScript>();
        ojochWeapon = GetComponent<WeaponScript>();
        ojochCollect = GetComponent<CollectingScript>();
        ojochPowerUp = GetComponent<PowerUpScript>();
        ojochCollision = GetComponent<OjochCollisions>();	
	}
	
}
