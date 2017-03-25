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
    
    void Start()
    {
        commonAIScript = GetComponent<CommonAI>();
        SetMovement();
    }

    /// <summary>
    /// Nastaví správný pohyb 
    /// </summary>
    private void SetMovement()
    {
        switch (myMovement)
        {
            case TypeOfMovement.straightLine:
                break;
            case TypeOfMovement.upAndDown:
                break;
            case TypeOfMovement.bottomToTop:
                break;
            case TypeOfMovement.topToBottom:
                break;
            case TypeOfMovement.forwardBackAndForward:
                break;
        }
    }



}
