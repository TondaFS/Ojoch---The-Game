using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public enum AIStates
{
    flyOnScreen,
    flyToPoints,
    kamikaze,
    stopAndShoot,
    chargeAttack,
    wait
}



public class EnemyAI : MonoBehaviour {

    public float movementSpeed;
    public AIStates currentState;
    public List<AIStates> nextStates;    
    
    //facing player
    private GameObject player;
    private bool facingLeft = true;

    //destroy off screeners
    private float leftBoundary;
    private float topBoundary;
    private float botBoundary;

    [Space(10, order=0)]
    [Header("==AI PROPERTIES==", order = 1)]
    [Space(5, order = 3)]
    [Header("Fly On Screen", order = 4)]
    [Space(5, order = 5)]
    //flyOnScreen
    [Range(0, 1)]
    public float flyOnScreenPosX; //hodnota je v procentech 0 vlevo, 1 vlevo
    private Vector3 flyOnScreenPos;

    [Space(5, order = 0)]
    [Header("Fly To Points (hodnoty jsou v procentech (0,1)", order = 1)]
    [Space(5, order = 3)]
    //flyToPoint
    public List<Vector2> points;

    //kamikaze

    [Space(5, order = 0)]
    [Header("Stop And Shoot", order = 1)]
    [Space(5, order = 3)]
    //stopAndShoot
    public Transform missile;
    public int ammo;

    //chargeAttack
    private float chargeUpTime = 2;

    [Space(5, order = 0)]
    [Header("Wait", order = 1)]
    [Space(5, order = 3)]
    //wait
    public float waitTime;

    

    void Start()
    {
        leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(-0.5f, 0)).x;
        topBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 1.5f)).y;
        botBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, -0.5f)).y;

        flyOnScreenPosX = Camera.main.ViewportToWorldPoint(new Vector3(flyOnScreenPosX, 0)).x;
        flyOnScreenPos = new Vector3(flyOnScreenPosX, transform.position.y);

        player = GameObject.FindGameObjectWithTag("Player");

        if (points.Count > 0)
        {
            points[0] = Camera.main.ViewportToWorldPoint(points[0]);
        }
    }
	
	void Update ()
    {
        //Debug.Log("State: " + currentState);
        DestroyOffScreeners();

        switch (currentState)
        {
            case AIStates.flyOnScreen:
                FlyOnScreen();
                break;

            case AIStates.flyToPoints:
                if (points.Count <= 0)
                {
                    SwitchToNextState();
                }
                else
                {
                    TurnAtPlayer(player);
                    FlyToPoint();
                }
                break;

            case AIStates.kamikaze:
                TurnAtPlayer(player);
                Kamikaze();
                break;

            case AIStates.stopAndShoot:
                TurnAtPlayer(player);
                StopAndShoot();
                break;

            case AIStates.chargeAttack:
                ChargeAttack();
                break;

            case AIStates.wait:
                if (waitTime == -1)
                {
                    break;
                }
                Wait();
                break;
        }
        
	
	}

    //AI leti na stanovene misto na obrazovku, kdyz tam doleti, prepne so dalsiho stavu
    private void FlyOnScreen()
    {
        transform.position = Vector3.MoveTowards(transform.position, flyOnScreenPos, movementSpeed * Time.deltaTime);             
        
        if (transform.position.x == flyOnScreenPosX)
        {
            SwitchToNextState();
        }
    }

    private void FlyToPoint()
    {      
        // pohybuj se smerem k dalsimu bodu
        transform.position = Vector3.MoveTowards(transform.position, (Vector3) points[0], movementSpeed * Time.deltaTime);
        
        // pokud jsi v bodu, let k dalsimu
        if (transform.position == (Vector3)points[0])
        {
            // vymaz tento bod
            points.RemoveAt(0);

            // pokud nejsou body, prepni se do dalsiho stavu
            if (points.Count <= 0)
            {
                SwitchToNextState();
            }
            // pokud jsou, preved je z procent na worldspace
            else
            {
                points[0] = Camera.main.ViewportToWorldPoint(points[0]);
            }
            
        }
    }

    //AI leti za hracem
    private void Kamikaze()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
    }

    private void StopAndShoot()
    {
        throw new NotImplementedException();
    }

    //AI se nabije a leti rovne az mimo obrazovku
    private void ChargeAttack()
    {
        chargeUpTime -= Time.deltaTime;

        if (chargeUpTime > 0)
        {
            transform.position = transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * 0.1f;
        }

        if (chargeUpTime <= 0)
        {
            Vector3 offScreenPos = new Vector3(Camera.main.ViewportToWorldPoint(new Vector3(-2, 0)).x, transform.position.y);
            transform.position = Vector3.MoveTowards(transform.position, offScreenPos, movementSpeed * 5 * Time.deltaTime);
        }
    }

    private void Wait()
    {
        waitTime -= Time.deltaTime;

        if (waitTime <= 0)
        {
            SwitchToNextState();
        }
    }

    //AI se prepne do dalsiho stavu
    private void SwitchToNextState()
    {
        currentState = nextStates[0];
        nextStates.RemoveAt(0);
    }

    //Sprite se otoci na stranu kde je hrac
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

    //Destroys any enemy way off the screen
    private void DestroyOffScreeners()
    {
        //Debug.DrawLine(new Vector3(leftBoundary, -20), new Vector3(leftBoundary, 20));

        if (transform.position.x < leftBoundary ||
           transform.position.y < botBoundary ||
           transform.position.y > topBoundary)
        {
            Destroy(this.gameObject);
        }
    }
}
