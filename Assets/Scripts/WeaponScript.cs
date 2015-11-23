using UnityEngine;
using System.Collections;

/// <summary>
/// Strelba
/// </summary>

public class WeaponScript : MonoBehaviour {
    //Promenne
    public Transform shotPrefab;            //prefab pro strelbu
    public Transform akPrefab;              //prefab pro AK47
    public float shootingRate = 0.25f;      //doba mezi vystrely
    private float shootCooldown;            //cooldown


    void Start() {
        shootCooldown = 0f;                 //inicialni cooldown strelby
    }

    //pokud je cooldown > 0, nelze strilet a odpocita se cas
    void Update() {
        if (shootCooldown > 0) {
            shootCooldown -= Time.deltaTime;
        }        
    }

    //Utok
    public void Attack(bool isEnemy, Vector2 directionP) {
        if (CanAttack)      // viz fce nize
        {           
            shootCooldown = shootingRate;                                                       //nastaveni cooldownu
                       
                var shotTransform = Instantiate(shotPrefab) as Transform;                              //vytvoreni nove strely
                shotTransform.position = transform.position + new Vector3(0.3f, 0, 0);                //prirazeni pozice strely

                ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();                 //Bereme ShotScript
                if (shot != null)
                {
                    shot.isEnemyShot = isEnemy;                                                        //aplikace vstupniho parametru a jeho prirazeni ke strele - kdo vystrelil? hrac/enemy
                }

                MovingScript move = shotTransform.gameObject.GetComponent<MovingScript>();             //Bereme MovingScript strely
                if (move != null)
                {
                    move.direction = directionP;                                          //Strela se vystreli smerem vpravo
                }          
            
            
        }
    }

    //Muze se uz strilet?
    public bool CanAttack {
        get {
            return shootCooldown <= 0f;
        }
    }  
    
    public void Ak47Attack(bool isEnemy, Vector2 directionP)
    {
        if (CanAttack)
        {
            shootCooldown = shootingRate;

            var akTransform = Instantiate(akPrefab) as Transform;
            akTransform.position = transform.position + new Vector3(0.2f, 0, 0);

            ShotScript shot = akTransform.gameObject.GetComponent<ShotScript>();                 //Bereme ShotScript

            if (shot != null)
            {
                shot.isEnemyShot = isEnemy;                                                        //aplikace vstupniho parametru a jeho prirazeni ke strele - kdo vystrelil? hrac/enemy
            }

            MovingScript move = akTransform.gameObject.GetComponent<MovingScript>();             //Bereme MovingScript strely
            if (move != null)
            {
                move.direction = directionP;                                                    //Strela se vystreli smerem vpravo
            }
        }
    }
}
