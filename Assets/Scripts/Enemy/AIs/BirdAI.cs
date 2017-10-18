using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BirdAI : CommonAI { 
    /// <summary>
    /// x-ová souřadnice bodu, kam se bude pták pohybovat
    /// </summary>
    private int offScreenPoint = -20;

    /// <summary>
    /// Jak dlouho trva, nez se ptak pohne nahoru nebo dolu
    /// </summary>
    [Header("Up and Down movement")]
    public float changeOfYTimer = 1.5f;
    /// <summary>
    /// Jak moc doleva se ptak musi posunotu v ramci pohybu nahoru nebo dolu
    /// </summary>
    public float changeOfXPosition = 1;
    /// <summary>
    /// Jak moc nahoru nebo dolu se ptak posune
    /// </summary>
    public float changeOfYPosition = 1f;
    /// <summary>
    /// Vektor pro ulozeni cilove destinace pri pohybu nahoru nebo dolu
    /// </summary>
    Vector3 yDestination;
    /// <summary>
    /// casovac pro spusteni pohybu nahoru nebo dolu
    /// </summary>
    Coroutine movementTimer;
    /// <summary>
    /// Kolikrat se ptak pohl v ramci jednoho smeru
    /// </summary>
    [SerializeField]
    int movementInDirection;
    /// <summary>
    /// Smery, kterymi se muze ptak pohybovat
    /// </summary>
    enum Direction { up, down, none }
    /// <summary>
    /// Jakym smerem se ptak naposledy pohl
    /// </summary>
    Direction lastDirection;

    /// <summary>
    /// Reference na prase, které budu chránit;
    /// </summary>
    [Header("Ochrana prasete")]    
    private GameObject pigReference;
    /// <summary>
    /// Posuniti ptaka vzhledem k praseti
    /// </summary>
    public float pigXPosition = 1.7f;
    /// <summary>
    /// Posutuni ptaka v y ose vzhledem k praseti
    /// </summary>
    public float pigYPosition = 0.25f;
    
    /// <summary>
    /// Vyuziti base Start z common AI doplneny o pohzbove informace a 
    /// kontroly jinych nepratel
    /// </summary>
    public override void Start()
    {
        base.Start();

        enemyType = EnemyType.bird;
        movementTimer = null;
        movementInDirection = 0;
        lastDirection = Direction.none;

        SessionController.instance.birdsInScene.Add(this.gameObject); 
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

        if (currentState.Equals(AIStates.flyForward))
        {
            if (movementTimer == null)
                movementTimer = StartCoroutine(ChangeOfY(changeOfYTimer));
            
            MovementForward();
        }

        if (currentState.Equals(AIStates.flyUpOrDown))
            MovementUpOrDown();

        if (currentState.Equals(AIStates.protectPig))
        {
            if (pigReference == null)
            {
                ChoosePig();
            }
            ProtectPig();
        }
    }

    /// <summary>
    /// Zastavi movementTimer coroutine, prepne se do stavu pronasledovani Ojocha a zrzchli
    /// </summary>
    public override void AK47()
    {
        StopCoroutine(movementTimer);
        SwitchToNextState(AIStates.chase);
        ChangeMovementSpeed(movementChange);
    }
    /// <summary>
    /// Ptakovi se zrychli pohyb
    /// </summary>
    public override void HalfHealth()
    {
        if (!halfDamageEffectDone)
        {
            movementSpeed += movementChange;
            halfDamageEffectDone = true;
            Debug.Log("Ptak ma polovinu sveho zivota... jeho pohyb je zrychlen.");
        }
        
    }
    /// <summary>
    /// Zjistí, jestli dany ptak chranil nejake prase. Pokud ano, prenastavi danemu praseti info o ochraně na false a pro vsechny ptaky 
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
    /// Jak se objeví prase ve hře, začnu jedno prase chránit
    /// </summary>
    public void PigAppear()
    {
        SwitchToNextState(AIStates.protectPig);
        StopCoroutine(movementTimer);
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
    private void MovementForward()
    {
        Vector3 towardVector = new Vector3(offScreenPoint, transform.position.y);
        transform.position = Vector3.MoveTowards(transform.position, towardVector, movementSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Ptak se hybe nahoru nebo dolu
    /// </summary>
    private void MovementUpOrDown()
    {
        transform.position = Vector3.MoveTowards(transform.position, yDestination, movementSpeed * Time.deltaTime);

        if (transform.position.Equals(yDestination))
            SwitchToNextState(AIStates.flyForward);
    }
        
    /// <summary>
    /// Náhodně vybere nějaké prase, které bude chránit
    /// </summary>
    private void ChoosePig()
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
    private void ProtectPig()
    {  
        if(pigReference != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, pigReference.transform.position + new Vector3(-pigXPosition, pigYPosition, 0), movementSpeed * Time.deltaTime);
        }        
    }
    
    /// <summary>
    /// Timer, ktery po urcite dobe spocita smer, kterym se bude muset ptak pohnout
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator ChangeOfY(float time)
    {
        lastDirection = CalculateDirection();
        
        //nastav smer nahoru
        if (lastDirection.Equals(Direction.up))
            yDestination = new Vector3(transform.position.x - changeOfXPosition, transform.position.y + changeOfYPosition);
        
        //nastav smer dolu
        else
            yDestination = new Vector3(transform.position.x - changeOfXPosition, transform.position.y - changeOfYPosition);
            

        SwitchToNextState(AIStates.flyUpOrDown);

        yield return new WaitForSeconds(time);
        movementTimer = StartCoroutine(ChangeOfY(changeOfYTimer));        
    }
    
    /// <summary>
    /// Metoda, ktera nam vybere spravny smer pohybu bud nahoru nebo dolu, kontroluje, jestli jsme neprekrocili pocet
    /// posunu v jednom smeru o 2 a pak jestli se ptak nahodou uz neobjevuje u okraje obrazovky a ze by jej nasledujici pohyb neposunul mimo
    /// </summary>
    /// <returns>vraci spravny smer</returns>
    private Direction CalculateDirection()
    {
        int propability = Random.Range(0, 100);
        Direction newDirection;

        //chceme se pohnout dolu
        if (propability < 50)
        {
            // nepohybovali jsme se dolu uz nahodou 2x?
            if (lastDirection.Equals(Direction.down) && movementInDirection == 2)
            {
                movementInDirection = 1;
                newDirection = Direction.up;
            }
            else
            {
                //nejsme nahodou u okraje obrazovky?
                if ((transform.position.y - changeOfYPosition) < -5)
                {
                    newDirection = Direction.up;
                    movementInDirection = 1;
                }
                else
                {
                    newDirection = Direction.down;
                    movementInDirection += 1;
                }    
            }
        }
        //chceme nahoru
        else
        {
            //nehybali jsme se nahoru uz dvakrat?
            if (lastDirection.Equals(Direction.up) && movementInDirection == 2)
            {
                movementInDirection = 1;
                newDirection = Direction.down;
            }
            else
            {
                //nejsme nahodou u okraje obrazovky?
                if ((transform.position.y + changeOfYPosition) > 5)
                {
                    newDirection = Direction.down;
                    movementInDirection = 1;
                }
                else {   
                    newDirection = Direction.up;
                    movementInDirection += 1;
                }
            }
        }
        
        return newDirection;
    }

}
