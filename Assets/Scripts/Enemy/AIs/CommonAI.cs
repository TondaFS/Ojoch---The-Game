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
    public bool turns = true;
    private GameObject player;
    private bool facingLeft = true;

    //destroy off screeners
    private float leftBoundary;
    private float topBoundary;
    private float botBoundary;
}
