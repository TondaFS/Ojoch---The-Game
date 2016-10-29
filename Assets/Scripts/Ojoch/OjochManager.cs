using UnityEngine;
using System.Collections;

/// <summary>
/// Skript pro rychlý (veřejný) přístup k jednotlivým Ojochovým skriptům.
/// <para>OjochScript, HealthScript, WeaponScript, CollectingScript, PowerUpScript, OjochCollisions</para>
/// </summary>
public class OjochManager : MonoBehaviour {
    public static OjochManager instance = null;

    public OjochScript ojochScript;
    public HealthScript ojochHealth;
    public WeaponScript ojochWeapon;
    public CollectingScript ojochCollect;
    public PowerUpScript ojochPowerUp;
    public OjochCollisions ojochCollision;

    public ColorChanger sprite;
    
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if(instance != this)
        {
            Destroy(this.gameObject);
        }

        ojochScript = GetComponent<OjochScript>();
        ojochHealth = GetComponent<HealthScript>();
        ojochWeapon = GetComponent<WeaponScript>();
        ojochCollect = GetComponent<CollectingScript>();
        ojochPowerUp = GetComponent<PowerUpScript>();
        ojochCollision = GetComponent<OjochCollisions>();
    } 

    void Start () {
        //kvůli zranění - viz. HealthScript
        sprite = GetComponentInChildren<ColorChanger>();//GameObject.Find("sprite").GetComponent<ColorChanger>();
    }
	
}
