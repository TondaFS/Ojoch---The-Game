using UnityEngine;
using System.Collections;

public enum TypeOfMovement
{
    straightLine,
    upAndDown,
    bottomToTop,
    topToBottom,
    forwardBackAndForward
}

public class BirdAI : MonoBehaviour { 
    public CommonAI commonAIScript;
    public TypeOfMovement myMovement; 

    private Vector3 offScreenPoint = new Vector3(-10, 0, 0);
        
    void Start()
    {
        commonAIScript = GetComponent<CommonAI>();
        offScreenPoint.y = transform.position.y;
    }

    void Update()
    {

        if (commonAIScript.currentState.Equals(AIStates.flyOnCurve))
        {
            Movement();
        }
        
    }

    /// <summary>
    /// Nastaví správný pohyb 
    /// </summary>
    private void Movement()
    {
        switch (myMovement)
        {
            case TypeOfMovement.straightLine:
                StraightLineMovement();
                break;
            case TypeOfMovement.upAndDown:
                UpAndDownMovement();
                break;
            case TypeOfMovement.bottomToTop:
                break;
            case TypeOfMovement.topToBottom:
                break;
            case TypeOfMovement.forwardBackAndForward:
                break;
        }
    }

    /// <summary>
    /// Pohybuj se přímo doleva za obrazovku
    /// </summary>
    private void StraightLineMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, offScreenPoint, commonAIScript.movementSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Pohybuj se nahoru a dolu
    /// </summary>
    private void UpAndDownMovement()
    {
        //transform.position +=  new Vector3(-commonAIScript.movementSpeed * Time.deltaTime, Mathf.Sin(random)/8, 0.0f);
    }





}
