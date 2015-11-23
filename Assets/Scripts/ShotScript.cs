using UnityEngine;
using System.Collections;

/// <summary>
/// Chování střel
/// </summary>

public class ShotScript : MonoBehaviour {

    //Proměnné
    public int damage = 1;              //davane zraneni
    public bool isEnemyShot = false;    //patri strela hraci/nepriteli?
    public int lifeTime = 5;            //Doba zivotnosti strely

    public AudioClip blop;


    void Start() {
        Destroy(gameObject, lifeTime);  //Zniceni objektu po vyprseni jeho doby zivotnosti   
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "Obstacle")
        {
            SoundScript.instance.PlaySingle(blop);
            Destroy(this.gameObject);           //zniceni strely

        }
    }


}
