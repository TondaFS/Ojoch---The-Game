using UnityEngine;
using System.Collections;

/// <summary>
/// Strelba
/// </summary>

public class WeaponScript : MonoBehaviour {
    //Promenne
    public Transform shotPrefab;            //prefab pro strelbu
    public float shootingRate = 0.25f;      //doba mezi vystrely
    private float shootCooldown;            //cooldown

    void Start() {
        shootCooldown = 0f;
    }

    void Update() {
        if (shootCooldown > 0) {
            shootCooldown -= Time.deltaTime;
        }
    }

    //Utok
    public void Attack(bool isEnemy) {
        if (CanAttack)
        {
            shootCooldown = shootingRate;                               //nastaveni cooldownu
            var shotTransform = Instantiate(shotPrefab) as Transform;   //vytvoreni nove strely
            shotTransform.position = transform.position;              //prirazeni pozice strely

            // The is enemy property
            ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
            if (shot != null){
                shot.isEnemyShot = isEnemy;
            }

            // Make the weapon shot always towards it
            MovingScript move = shotTransform.gameObject.GetComponent<MovingScript>();
            if (move != null)
            {
                move.direction = this.transform.right; // towards in 2D space is the right of the sprite
            }
        }

    }

    //Muze se uz strilet?
    public bool CanAttack {
        get {
            return shootCooldown <= 0f;
        }
    }


}
