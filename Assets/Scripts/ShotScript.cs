using UnityEngine;
using System.Collections;

/// <summary>
/// Chování střel
/// </summary>

public class ShotScript : MonoBehaviour {

    //Proměnné
    public int damage = 1;              //davane zraneni
    public bool isEnemyShot = false;    //patri strela hraci/nepriteli?
    public float lifeTime = 5;            //Doba zivotnosti strely

    public AudioClip blop;
    
    void Start()
    {
        if (lifeTime != -1)
        {
            Destroy(gameObject, lifeTime);  //Zniceni objektu po vyprseni jeho doby zivotnosti   
        }
    }    

    //Pri srazce 
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Corners") && gameObject.layer == LayerMask.NameToLayer("OjochProjectile"))
        {
            if (gameObject.GetComponent<BoxCollider2D>() != null)
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
            else
            {
                gameObject.GetComponent<CircleCollider2D>().enabled = false;
            }
        }
    }

    //Znici se po opusteni obrazovky
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
