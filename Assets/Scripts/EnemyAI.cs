using UnityEngine;
using System.Collections;
using System;

public enum AIStates
{
    letNaObrazovku,
    kamikaze,
    stopShoot
}

public class EnemyAI : MonoBehaviour {

    public AIStates currentState;
    public float movementSpeed;
    [Range(0, 1)]public float targetPosX; //hodnota je v procentech 0 vlevo, 1 vlevo

    private GameObject player;
    private bool facingLeft = true;
    private Vector3 targetPos;

    void Start()
    {
        targetPosX = Camera.main.ViewportToWorldPoint(new Vector3(targetPosX, 0, 0)).x;
        targetPos = new Vector3(targetPosX, transform.position.y);

        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	void Update ()
    {
        //Debug.Log("State: " + currentState);

        TurnAtPlayer(player);

        switch (currentState)
        {
            case AIStates.letNaObrazovku:
                LetNaObrazovku();
                break;

            case AIStates.kamikaze:
                Kamikaze();
                break;             
        }
	
	}

    private void TurnAtPlayer(GameObject player)
    {
        if (transform.position.x > player.transform.position.x)
        {
            if (!facingLeft)
            {
                Vector3 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
                facingLeft = true;
            }                      
        }
        else
        {
            if (facingLeft)
            {
                Vector3 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
                facingLeft = false;
            }            
        }
    }

    private void Kamikaze()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
    }

    private void LetNaObrazovku()
    {

        transform.position = Vector3.MoveTowards(transform.position, targetPos, movementSpeed * Time.deltaTime);             
        
        if (transform.position.x == targetPosX)
        {
            currentState = AIStates.kamikaze;
        }
    }
}
