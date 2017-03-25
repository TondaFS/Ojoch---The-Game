using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Typy všech bossů
/// </summary>
public enum BossType
{
    none,
    goldenstein,
    sojka,
    prepepr,
    zebirko,
    rudak,
    rakosnik,
    alexander,
    kovar,
    fakir
}
public enum BossStates
{
    flyOnScreen,
    basicAttack,
    specialAttack,
    deatchAttack
}


public class BossAI : MonoBehaviour {
    /// <summary>
    /// Typ Bosse
    /// </summary>
    public BossType bossType = BossType.none;

    /// <summary>
    /// Rychlost pohybu
    /// </summary>
    public float movementSpeed;
    /// <summary>
    /// Aktuální stav AI
    /// </summary>
    public BossStates currentState;
    /// <summary>
    /// Stav do kterého se enemák přepne, jen co vletí na obrazovku
    /// </summary>
    public BossStates startingState;

    //facing player
    /// <summary>
    /// Je nepritel otoceny k hraci?
    /// </summary>
    public bool turns = true;
    /// <summary>
    /// reference na gameObject Ojocha
    /// </summary>
    private GameObject player;
    /// <summary>
    /// Diva se nepritel vlevo?
    /// </summary>
    private bool facingLeft = true;

    //flyOnScreen
    [Header("Fly on screen", order = 1)]
    public float flyOnScreenPosX; //hodnota je v procentech 0 vlevo, 1 vlevo
    private Vector3 flyOnScreenPos;

    void Start()
    {
        currentState = BossStates.flyOnScreen;
        player = OjochManager.instance.gameObject;

        flyOnScreenPosX = Camera.main.ViewportToWorldPoint(new Vector3(flyOnScreenPosX, 0)).x;
        flyOnScreenPos = new Vector3(flyOnScreenPosX, transform.position.y);

        SessionController.instance.numberOfEnemies += 1;
        SessionController.instance.bossInScene = this.gameObject;
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
    public void SwitchToNextState(BossStates state)
    {
        currentState = state;
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
    /// Destroys this Game object and remove the enemy from list in session controller
    /// </summary>
    private void DestroyThis()
    {
        SessionController.instance.numberOfEnemies -= 1;
        Destroy(gameObject);
    }
}
