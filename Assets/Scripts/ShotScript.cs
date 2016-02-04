﻿using UnityEngine;
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
    
    void Start()
    {
        if (lifeTime != -1)
        {
            Destroy(gameObject, lifeTime);  //Zniceni objektu po vyprseni jeho doby zivotnosti   
        }
    }    

    //Pri srazce 
    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        //S prekazkou -> znici 
        if (otherCollider.tag == "Obstacle")
        {
            SoundScript.instance.PlaySingle(blop);
            Destroy(this.gameObject);                           

        }

        //s hracem a je to nepratelska strela
        if (otherCollider.tag == "Player" && isEnemyShot)
        {
            GameObject.Find("Session Controller").GetComponent<ScoreScript>().modifikatorScore -= 1;
        }

        if (otherCollider.gameObject.layer == LayerMask.NameToLayer("Corners"))
        {
            Debug.Log("Touched a rail");
        }
    }

    //Znici se po opusteni obrazovky
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
