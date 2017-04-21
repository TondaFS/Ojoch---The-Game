using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;



public class EnemyAI : MonoBehaviour {

    public float movementSpeed;
    public List<AIStates> states;
    private AIStates currentState;

    //facing player
    public bool turns = true;
    private GameObject player;
    private bool facingLeft = true;

    //destroy off screeners
    private float leftBoundary;
    private float topBoundary;
    private float botBoundary;

    [Space(10, order = 0)]
    [Header("==AI PROPERTIES==", order = 1)]
    [Space(5, order = 3)]
    /////////////////////flyOnScreen
    [Header("Fly On Screen", order = 4)]
    [Space(5, order = 5)]
    [Range(0, 1)]
    public float flyOnScreenPosX; //hodnota je v procentech 0 vlevo, 1 vlevo
    private Vector3 flyOnScreenPos;


    /////////////////////flyToPoint
    [Space(5, order = 0)]
    [Header("Fly To Points (hodnoty jsou v procentech (0,1)", order = 1)]
    [Space(5, order = 3)]
    public List<Vector2> points;

    /////////////////////kamikaze
    [Space(5, order = 0)]
    [Header("Kamikaze", order = 1)]
    [Space(5, order = 3)]
    public float explosionCountdown;
    public float ignitionRadius;
    private float ignitionSpeed;
    private bool ignited = false;
    public bool exploded = false;
    private Vector3 originalScale;

    /////////////////////chase

    /////////////////////stopAndShoot
    [Space(5, order = 0)]
    [Header("Stop And Shoot", order = 1)]
    [Space(5, order = 3)]
    public Transform missile;
    public int ammo;
    private Vector3 missileLauncherPos;
    public float missileCooldown = 1;
    private float currentMissileCooldown;

    /////////////////////chargeAttack
    private float chargeUpTime = 2;

    /////////////////////wait
    [Space(5, order = 0)]
    [Header("Wait", order = 1)]
    [Space(5, order = 3)]
    public float waitTime;



    void Start()
    {
        SwitchToNextState();

        //Kamikaze
        originalScale = transform.localScale;
        ignitionSpeed = movementSpeed + 1;
        //End kamikaze

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

    void Update()
    {        
        DestroyOffScreeners();

        switch (currentState)
        {
            case AIStates.flyOnScreen:
                FlyOnScreen();
                break;
    
                /*
            case AIStates.flyToPoints:
                if (points.Count <= 0)
                {
                    SwitchToNextState();
                }
                else
                {
                    if (turns)
                    {
                        TurnAtPlayer();
                    }
                    FlyToPoint();
                }
                break;
                */
            case AIStates.kamikaze:
                if (turns)
                {
                    TurnAtPlayer();
                }
                Kamikaze();
                break;

            case AIStates.chase:
                if (turns)
                {
                    TurnAtPlayer();
                }
                Chase();
                break;

            case AIStates.stopAndShoot:
                if (turns)
                {
                    TurnAtPlayer();
                }
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
    
    private void FlyToPoint()
    {
        // pohybuj se smerem k dalsimu bodu
        transform.position = Vector3.MoveTowards(transform.position, (Vector3)points[0], movementSpeed * Time.deltaTime);

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
    //AI leti za hracem, kdyz je blizko, zazehne a vybouchne
    private void Kamikaze()
    {
        if (player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);

            float playerDistance = Vector2.Distance(transform.position, player.transform.position);

            if (playerDistance < ignitionRadius)
            {
                ignited = true;
                movementSpeed = ignitionSpeed;
            }

            if (ignited)
            {
                //countdown
                explosionCountdown -= Time.deltaTime;

                //blink
                float sin = Mathf.Sin(Time.time * 10) * 0.3f + 0.7f;
                Color newColor = new Color(255, sin, sin);
                this.GetComponent<SpriteRenderer>().color = newColor;

                //pulse
                float pulse = Mathf.Sin(Time.time * 30) * 0.05f;
                if (facingLeft)
                {
                    transform.localScale = new Vector3(originalScale.x - pulse, originalScale.y + pulse);
                }
                else
                {
                    transform.localScale = new Vector3((originalScale.x * -1) - pulse, originalScale.y - pulse);
                }
            }

            if (explosionCountdown < 0 && !exploded)
            {
                exploded = true;
                GetComponent<EnemyHealth>().EnemyDamage(100);
            }
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
    private void DestroyThis()
    {
        Destroy(gameObject);
    }
    
    //DONE
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

    //Sprite se otoci na stranu kde je hrac
    private void TurnAtPlayer()
    {
        if (player != null)
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
    }
    //AI se prepne do dalsiho stavu
    private void SwitchToNextState()
    {
        currentState = states[0];
        states.RemoveAt(0);
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
    private void Chase()
    {
        if (player != null)
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
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
    private void StopAndShoot()
    {
        if (currentMissileCooldown > 0)
        {
            currentMissileCooldown -= Time.deltaTime;
        }
        else
        {
            currentMissileCooldown = missileCooldown;
            gameObject.GetComponent<Animator>().SetTrigger("sAttack");
            ammo--;
        }

        if (ammo == 0)
        {
            SwitchToNextState();
        }
    }
    private void Launch()
    {
        missileLauncherPos = this.transform.GetChild(0).transform.position;
        //Debug.Log(missileLauncherPos);
        Instantiate(missile, missileLauncherPos, Quaternion.identity);
    }

}
