using UnityEngine;
using System.Collections;

#region Enumy nepřátel
/// <summary>
/// Stavy AI, ve kterých mohou nepřátelé být
/// </summary>
public enum AIStates
{
    flyOnScreen,
    flyForward,
    flyUpOrDown,
    kamikaze,
    chase,
    stopAndShoot,
    chargeAttack,
    chaseAndShoot,
    wait,
    shootLaser,
    laserActive,
    laserCharging,
    protectPig
}
/// <summary>
/// Typy všech nepřátel ve hře.
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
#endregion

/// <summary>
/// Skript udržující téměř všechnu logiku týkající se nepřátelské AI
/// Nepřítelovy životy, jejich ztráta a kontrola, zda by neměl umřít.
/// Nepřítelovo chování a přepínání AI stavů.
/// </summary>
public class CommonAI : MonoBehaviour {
    #region Proměnné
    /// <summary>
    /// Typ nepřítele
    /// </summary>
    [HideInInspector]
    public EnemyType enemyType = EnemyType.none;

    /// <summary>
    /// Životy
    /// </summary>
    [Header("Životy")]
    public int hp = 1;
    public int damagedHP = 1;
    protected bool halfDamageEffectDone;
    /// <summary>
    /// Kolik hráč dostane skóre za zabití
    /// </summary>
    public int score = 50;
    /// <summary>
    /// Dostal nepřítel zásah?
    /// </summary>
    /// 
    bool isHit;
    /// <summary>
    /// Jakou rychle se bude měnit barva na původní
    /// </summary>
    public float speedOfChange = 1f;

    /// <summary>
    /// Rychlost pohybu
    /// </summary>
    [Header("Pohyb stuff")]
    public float movementSpeed;
    /// <summary>
    /// Aktuální stav AI
    /// </summary>
    public AIStates currentState;
    /// <summary>
    /// Stav do kterého se nepřítel přepne, poté, co vletí na obrazovku.
    /// </summary>
    [Tooltip("Stav do kterého se nepřítel přepne, poté, co vletí na obrazovku.")]
    public AIStates startingState;
    /// <summary>
    /// O kolik rychleji se bude ptak pohybovar pri zmene pohybu
    /// </summary>
    public float movementChange = 0.25f;

    /// <summary>
    /// Je nepritel otoceny k hraci?
    /// </summary>
    public bool turns = true;
    /// <summary>
    /// reference na gameObject Ojocha
    /// </summary>
    [HideInInspector]
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
    public float flyOnScreenPosX; //hodnota je v procentech 0 vlevo, 1 vpravo
    protected Vector3 flyOnScreenPos;

    //Charge Attack
    private float chargeUpTime = 2;
        
    /// <summary>
    /// zvuk smrti
    /// </summary>
    [Header("SoundClips")]    
    public AudioClip deathSound;
    /// <summary>
    /// zvuk při kolizi s nepřítelem nebo sochou
    /// </summary>
    public AudioClip ojochCollisionClip;
    #endregion

    /// <summary>
    /// Základní nastavení nepřítele při inicializaci ve scéně
    /// </summary>
    public virtual void Start()
    {
        currentState = AIStates.flyOnScreen;
        halfDamageEffectDone = false;
        leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(-0.5f, 0)).x;
        topBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 1)).y;
        botBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0)).y;
        
        flyOnScreenPosX = Camera.main.ViewportToWorldPoint(new Vector3(flyOnScreenPosX, 0)).x;
        flyOnScreenPos = new Vector3(flyOnScreenPosX, transform.position.y);

        player = OjochManager.instance.gameObject;
        SessionController.instance.numberOfEnemies += 1;

        isHit = false;
        damagedHP = (int)(hp / 2);
    }
    public virtual void Update()
    {
        DestroyOffScreeners();

        if (isHit)
        {
            ChangeColor();
        }

        switch (currentState) {

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
    /// Dá nepříteli zranění. Pokud následkem toho přijde o svůj zbývající život,
    /// spustí animaci a zvuk smrti, přidá Ojochovi skóre a zajistí, aby nepřítel zemřel.
    /// V opačném případě zajistí, aby nepřítel červeně blikl.
    /// </summary>
    /// <param name="damage">Jaké zranění něpřítel dostane</param>
    public virtual void EnemyDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Debug.Log("Enemy dies");
            EnemyDeathSound();
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Animator>().SetTrigger("bDeath");
            SessionController.instance.GetComponent<ScoreScript>().UpdateScoreStuff(score, 0, 1, true);
            Debug.Log("End enemy damage");
        }
        else
        {
            if (hp <= damagedHP)
                HalfHealth();

            SetRedColor();
            isHit = true;
        }
    }

    public virtual void HalfHealth()
    {
        Debug.Log("Common AI Half Health...");     
    }

    public virtual void AK47()
    {
        Debug.Log("Ojoch sebral AK47 comon AI");
    }

    /// <summary>
    /// Přehraje odpovídající zvuk smrti daného nepřítele
    /// </summary>
    /// <param name="enemy">Typ nepřítele</param>
    public void EnemyDeathSound()
    {
        GameManager.instance.GetComponent<SoundManager>().PlaySound(deathSound);
    }

    /// <summary>
    /// Mění postupně barvu z červené na původní
    /// </summary>
    public void ChangeColor()
    {
        float actualColor = this.GetComponent<SpriteRenderer>().color.g + (speedOfChange * Time.deltaTime);
        if (actualColor >= 1)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
            isHit = false;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().color = new Color(1, actualColor, actualColor);
        }
    }

    /// <summary>
    /// Nastaví barvu na červenou
    /// </summary>
    public void SetRedColor()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
    }

    /// <summary>
    /// Odstraní nepřítele, pokud je mimo obrazovku
    /// </summary>
    private void DestroyOffScreeners()
    {
        if (transform.position.x < leftBoundary || transform.position.y < botBoundary ||
            transform.position.y > topBoundary)
        {
            Debug.Log("Destroying out of screen");
            DestroyThis();
        }
    }

    /// <summary>
    /// AI leti na stanovene misto na obrazovku, kdyz tam doleti, prepne so dalsiho stavu
    /// </summary>
    public void FlyOnScreen()
    {
        transform.position = Vector3.MoveTowards(transform.position, flyOnScreenPos, movementSpeed * Time.deltaTime);
        if (transform.position.x <= flyOnScreenPosX)
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
    /// Sníží počet nepřátel ve hře a zničí daný objekt
    /// </summary>
    public virtual void DestroyThis()
    {
        SessionController.instance.numberOfEnemies -= 1;
        Destroy(gameObject);
    }

    
    #region Enemy Collisions
    
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Socha"))
        {
            EnemyDamage(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Projectile")
        {
            ProjectileEnemyCollision(col);
        }
    }

    /// <summary>
    /// Funkce zajistí všechny náležitosti, když nepřítel umře.
    /// <para>Vypnutí Collideru nepřítele, spuštění animace smrti, přehrání zvuku smrti a nakonec zničení nepřítele.</para>
    /// </summary>
    /// <param name="col">Objekt, co způsobí nepříteli smrt.</param>
    public void EnemyDamage(GameObject col)
    {
        col.GetComponent<Collider2D>().enabled = false;
        col.GetComponent<Animator>().SetTrigger("bDeath");
        GameManager.instance.GetComponent<SoundManager>().PlaySound(ojochCollisionClip);

    }

    /// <summary>
    /// Nepřítelova kolize s Ojochovou (ne nepřátelskou) střelou.
    /// <para>Dá nepříteli zranění a zničí střelu.</para>
    /// </summary>
    /// <param name="col">Střela</param>
    public void ProjectileEnemyCollision(Collider2D col)
    {
        if (!col.gameObject.GetComponent<ShotScript>().isEnemyShot)
        {
            EnemyDamage(col.gameObject.GetComponent<ShotScript>().damage);
            Destroy(col.gameObject);
        }
    }
    #endregion
    
}
