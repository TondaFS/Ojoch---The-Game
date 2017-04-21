using UnityEngine;
using System.Collections;

/// <summary>
/// Stavy AI, ve kterých mohou nepřátelé být
/// </summary>
public enum AIStates
{
    flyOnScreen,
    flyOnCurve,
    kamikaze,
    chase,
    stopAndShoot,
    chargeAttack,
    chaseAndShoot,
    wait,
    shootLaser,
    laserActive,
    laserCharging
}
/// <summary>
/// Typy všech nepřátel a bossů ve hře.
/// </summary>
public enum EnemyType
{
    bird,
    rat,
    squirrel,
    sputnik,
    pig,
    none
}


public class CommonAI : MonoBehaviour {
    /// <summary>
    /// Typ nepřítele
    /// </summary>
    public EnemyType enemyType = EnemyType.none;
    
    /// <summary>
    /// Rychlost pohybu
    /// </summary>
    public float movementSpeed;
    /// <summary>
    /// Aktuální stav AI
    /// </summary>
    public AIStates currentState;
    /// <summary>
    /// Stav do kterého se enemák přepne, jen co vletí na obrazovku
    /// </summary>
    public AIStates startingState;

    //facing player
    /// <summary>
    /// Je nepritel otoceny k hraci?
    /// </summary>
    public bool turns = true;
    /// <summary>
    /// reference na gameObject Ojocha
    /// </summary>
    public GameObject player;
    /// <summary>
    /// Diva se nepritel vlevo?
    /// </summary>
    public bool facingLeft = true;

    //destroy off screeners
    private float leftBoundary;
    private float topBoundary;
    private float botBoundary;

    //flyOnScreen
    [Header("Fly on screen", order = 1)]
    public float flyOnScreenPosX; //hodnota je v procentech 0 vlevo, 1 vlevo
    private Vector3 flyOnScreenPos;

    //Charge Attack
    private float chargeUpTime = 2;

    void Start()
    {
        currentState = AIStates.flyOnScreen;

        leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(-0.5f, 0)).x;
        topBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 1.5f)).y;
        botBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, -0.5f)).y;

        flyOnScreenPosX = Camera.main.ViewportToWorldPoint(new Vector3(flyOnScreenPosX, 0)).x;
        flyOnScreenPos = new Vector3(flyOnScreenPosX, transform.position.y);

        player = OjochManager.instance.gameObject;
        SessionController.instance.numberOfEnemies += 1;
        
    }
    void Update()
    {
        DestroyOffScreeners();

        switch(currentState){
            case AIStates.flyOnScreen:
                FlyOnScreen();
            break;
            case AIStates.chase:
                Chase();
            break;
            case AIStates.chargeAttack:
                ChargeAttack();
                break;            
        }        
    }

    /// <summary>
    /// Odstraní nepřítele, pokud je mimo obrazovku
    /// </summary>
    private void DestroyOffScreeners()
    {
        if (transform.position.x < leftBoundary ||
           transform.position.y < botBoundary ||
           transform.position.y > topBoundary)
        {
            DestroyThis();
        }
    }

    /// <summary>
    /// AI leti na stanovene misto na obrazovku, kdyz tam doleti, prepne so dalsiho stavu
    /// </summary>
    public void FlyOnScreen()
    {
        transform.position = Vector3.MoveTowards(transform.position, flyOnScreenPos, movementSpeed * Time.deltaTime);

        if (transform.position.x == flyOnScreenPosX)
        {
            SwitchToNextState(startingState);
        }
    }
    /// <summary>
    /// AI se prepme do dalsiho stavu
    /// </summary>
    /// <param name="state">Stav do ktereho se mam prepnout</param>
    public void SwitchToNextState(AIStates state)
    {
        currentState = state;
    }
    /// <summary>
    /// Pronásledování Ojocha
    /// </summary>
    public void Chase()
    {
        if (player != null)
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
    }
    /// <summary>
    /// AI se nabije a leti rovne az mimo obrazovku
    /// </summary>
    public void ChargeAttack()
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
    /// <summary>
    /// Sprite se otoci na stranu kde je hrac
    /// </summary>
    public void TurnAtPlayer()
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

    /// <summary>
    /// Změní rychlost pohybu o danou hodnotu
    /// </summary>
    /// <param name="change">Velikost změny</param>
    public void ChangeMovementSpeed(float change)
    {
        movementSpeed += change;
    }

    /// <summary>
    /// Zničí nepřítele a odebere jej ze seznamu nepřátel stejného typu
    /// </summary>
    private void DestroyThis()
    {
        SessionController.instance.numberOfEnemies -= 1;
        switch (enemyType)
        {
            case EnemyType.squirrel:
                SessionController.instance.squirrelsInScene.Remove(this.gameObject);
                break;
            case EnemyType.rat:
                SessionController.instance.ratsInScene.Remove(this.gameObject);
                break;
            case EnemyType.sputnik:
                SessionController.instance.sputniksInScene.Remove(this.gameObject);
                break;
            case EnemyType.pig:
                SessionController.instance.pigsInScene.Remove(this.gameObject);
                break;
            case EnemyType.bird:
                SessionController.instance.birdsInScene.Remove(this.gameObject);
                break;
        }
        Destroy(gameObject);
    }

}
