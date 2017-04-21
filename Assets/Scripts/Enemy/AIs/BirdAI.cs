using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    /// <summary>
    /// Typ křivky, po které se bude pták pohybovat
    /// </summary>
    public TypeOfMovement myMovement;
    /// <summary>
    /// Bod, ke kterému se bude pták pohybovat po přímé lince
    /// Využití ale i u y-ové souřadnice pro další pohyby
    /// </summary>
    private Vector3 offScreenPoint = new Vector3(-10, 0, 0);

    /// <summary>
    /// Rychlost pohybu po sinusoidě
    /// </summary>
    [Tooltip("Rychlost po sinusoidě: dopručeno v intevralu <0.1,0.2>")]
    public float sinSpeed = 1;
    /// <summary>
    /// Pozice odkud se bude počítat sinus pohyb
    /// </summary>
    private float sinPosition;
    /// <summary>
    /// Posunutí, kt se bude postupně zvětšovat do 1, zaručuje, že se bude pták pohybovat k okrajím obrazovky a ne mimo ně. 
    /// Zvětšuje se, aby při přepnutí do stavu Up and Down nedošlo ke skoku.
    /// </summary>            
    private float posunuti = 0;

    /// <summary>
    /// Reference na prase, které budu chránit;
    /// </summary>
    public GameObject pigReference;
        
    void Start()
    {
        commonAIScript = GetComponent<CommonAI>();
        offScreenPoint.y = transform.position.y;        
        sinPosition = CalculatePositionForSinMovement();

        SessionController.instance.birdsInScene.Add(this.gameObject);

        if(SessionController.instance.squirrelsInScene.Count > 0)
        {
            commonAIScript.startingState = AIStates.chargeAttack;
        } 

        if(SessionController.instance.pigsInScene.Count > 0)
        {
            commonAIScript.startingState = AIStates.protectPig;
        }

        if (SessionController.instance.bossInScene != null && SessionController.instance.bossInScene.GetComponent<BossAI>().bossType.Equals(BossType.rudak))
        {
            commonAIScript.startingState = AIStates.chase;
        }
    }

    /// <summary>
    ///  Vypočítá pozici, odkud bude počítat sinusový pohyb
    /// </summary>
    /// <returns></returns>
    float CalculatePositionForSinMovement()
    {
        Debug.Log(offScreenPoint.y);
        float sinPos = 0;
        if(offScreenPoint.y > 4)
        {
            sinPos = 4;
        } else if(offScreenPoint.y < -3)
        {
            sinPos = -3;
        }
        else
        {
            sinPos = offScreenPoint.y;
        }
                
        return Mathf.Asin(sinPos / 4) / sinSpeed;
    }

    void Update()
    {

        if (commonAIScript.currentState.Equals(AIStates.flyOnCurve))
        {
            Movement();
        }

        if (commonAIScript.currentState.Equals(AIStates.protectPig))
        {
            if(pigReference == null)
            {
                ChoosePig();
            }  
                                  
            ProtectPig();
        }
        
    }

    /// <summary>
    /// Jak se objeví ve hře vevrka, provede Charge Attack
    /// </summary>
    public void SquirrelAppears()
    {
        commonAIScript.SwitchToNextState(AIStates.chargeAttack);
    }
    /// <summary>
    /// Jak se objeví prase ve hře, začnu jedno prase chránit
    /// </summary>
    public void PigAppear()
    {
        commonAIScript.SwitchToNextState(AIStates.protectPig);
    }
    /// <summary>
    /// Jak se ve hře objeví Rudý rudák anebo Ojoch sebere AK47 - začnu Ojocha pronásledovat
    /// </summary>
    public void AkOrRudakAppears()
    {
        commonAIScript.SwitchToNextState(AIStates.chase);
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
        transform.position = new Vector3(transform.position.x - commonAIScript.movementSpeed * Time.deltaTime, Mathf.Sin(sinPosition*sinSpeed) * 4 + posunuti, 0);
        if(posunuti < 1)
        {
            posunuti += 0.02f;
        }        
        sinPosition += 0.1f;
    }

    /// <summary>
    /// Náhodně vybere nějaké prase, které bude chránit
    /// </summary>
    public void ChoosePig()
    {
        if(SessionController.instance.pigsInScene.Count < 1)
        {
            commonAIScript.SwitchToNextState(AIStates.chase);
        }
        else
        {
            foreach (GameObject pig in SessionController.instance.pigsInScene)
            {
                if (!pig.GetComponent<PigAI>().isProtected)
                {
                    pigReference = pig;
                    pig.GetComponent<PigAI>().isProtected = true;
                    break;
                }                
            }
            if(pigReference == null)
            {
                commonAIScript.SwitchToNextState(AIStates.chase);
            }
        }
               
    }

    /// <summary>
    /// Letím před prase
    /// </summary>
    public void ProtectPig()
    {  
        if(pigReference != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, pigReference.transform.position + new Vector3(-1.5f, 0, 0), commonAIScript.movementSpeed * Time.deltaTime);
        }
        
    }






}
