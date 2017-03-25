using UnityEngine;
using System.Collections;

/// <summary>
/// Stavy AI, ve kterých mohou nepřátelé být
/// </summary>
public enum AIStates
{
    flyOnScreen,
    flyToPoints,
    kamikaze,
    chase,
    stopAndShoot,
    chargeAttack,
    wait
}

public class CommonAI : MonoBehaviour {
    /// <summary>
    /// Rychlost pohybu
    /// </summary>
    public float movementSpeed;
    /// <summary>
    /// Aktuální stav AI
    /// </summary>
    private AIStates currentState;

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
        
    }

    void Update()
    {
        DestroyOffScreeners();
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
            Destroy(this.gameObject);
        }
    }
}
