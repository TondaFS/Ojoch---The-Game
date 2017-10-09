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

public class BirdAI : CommonAI { 
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
        
    /// <summary>
    /// Vzuyiti base Start z common AI doplneny o pohzbove informace a 
    /// kontroly jinych nepratel
    /// </summary>
    public override void Start()
    {
        base.Start();
        
        offScreenPoint.y = transform.position.y;        
        sinPosition = CalculatePositionForSinMovement();

        SessionController.instance.birdsInScene.Add(this.gameObject);

        if(SessionController.instance.squirrelsInScene.Count > 0)
        {
            startingState = AIStates.chargeAttack;
        } 

        if(SessionController.instance.pigsInScene.Count > 0)
        {
            startingState = AIStates.protectPig;
        }

        if (SessionController.instance.bossInScene != null && SessionController.instance.bossInScene.GetComponent<BossAI>().bossType.Equals(BossType.rudak))
        {
            startingState = AIStates.chase;
        }
    }

    public override void Update()
    {
        base.Update();

        if (currentState.Equals(AIStates.flyOnCurve))
            Movement();

        if (currentState.Equals(AIStates.protectPig))
        {
            if (pigReference == null)
            {
                ChoosePig();
            }
            ProtectPig();
        }

    }

    /*
    public override void EnemyDeathSound()
    {
        GameManager.instance.GetComponent<SoundManager>().PlaySoundPitchShift(GameManager.instance.GetComponent<SoundManager>().birdDeath);
    }
    */

    /// <summary>
    /// Zjisti, jestli dany ptak chranil nejake prase. Pokud ano, prenastavi danemu praseti info o ochraně na false a pro vsechny ptaky 
    /// znovu vyřeší, jestli by dané prase neměli chránit...
    /// </summary>
    public override void DestroyThis()
    {        
        if (pigReference != null)
        {
            pigReference.GetComponent<PigAI>().isProtected = false;
            if (SessionController.instance.pigsInScene.Count > 0)
            {
                foreach (GameObject bird in SessionController.instance.birdsInScene)
                {
                    bird.GetComponent<CommonAI>().SwitchToNextState(AIStates.protectPig);
                }
            }
        }
        SessionController.instance.birdsInScene.Remove(this.gameObject);

        base.DestroyThis();
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
    
    /// <summary>
    /// Jak se objeví ve hře vevrka, provede Charge Attack
    /// </summary>
    public void SquirrelAppears()
    {
        SwitchToNextState(AIStates.chargeAttack);
    }
    /// <summary>
    /// Jak se objeví prase ve hře, začnu jedno prase chránit
    /// </summary>
    public void PigAppear()
    {
        SwitchToNextState(AIStates.protectPig);
    }
    /// <summary>
    /// Jak se ve hře objeví Rudý rudák anebo Ojoch sebere AK47 - začnu Ojocha pronásledovat
    /// </summary>
    public void AkOrRudakAppears()
    {
        SwitchToNextState(AIStates.chase);
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
        Debug.Log("move");
        transform.position = Vector3.MoveTowards(transform.position, offScreenPoint, movementSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Pohybuj se nahoru a dolu
    /// </summary>
    private void UpAndDownMovement()
    {        
        transform.position = new Vector3(transform.position.x - movementSpeed * Time.deltaTime, Mathf.Sin(sinPosition*sinSpeed) * 4 + posunuti, 0);
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
            SwitchToNextState(AIStates.chase);
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
                SwitchToNextState(AIStates.chase);
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
            transform.position = Vector3.MoveTowards(transform.position, pigReference.transform.position + new Vector3(-1.5f, 0, 0), movementSpeed * Time.deltaTime);
        }
        
    }
    
}
