using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeScript : MonoBehaviour {
    /// <summary>
    /// Doba než nepřítel vybuchne
    /// </summary>
    public float explosionCountdown;
    /// <summary>
    /// Radius, ve kterém se nepřítel "aktivuje"
    /// </summary>
    public float ignitionRadius;
    /// <summary>
    /// Rychlost nepřítele po zažehnutí
    /// </summary>
    private float ignitionSpeed;
    /// <summary>
    /// Byl nepřítel zažehnut?
    /// </summary>
    public bool ignited = false;
    /// <summary>
    /// Explodoval už nepřítel?
    /// </summary>
    public bool exploded = false;
    private Vector3 originalScale;
    /// <summary>
    /// Reference na commonAI script
    /// </summary>
    CommonAI commonAIRef; 

    void Start()
    {
        originalScale = transform.localScale;
        commonAIRef = GetComponent<CommonAI>();
        ignitionSpeed = commonAIRef.movementSpeed + 1;       
         
    }

    void Update()
    {
        if (commonAIRef.currentState == AIStates.kamikaze)
        {
            if (commonAIRef.turns)
            {
                commonAIRef.TurnAtPlayer();
            }
            Kamikaze();
        }
    }

    /// <summary>
    /// AI leti za hracem, kdyz je blizko, zazehne a vybouchne
    /// </summary>
    private void Kamikaze()
    {    
        if (commonAIRef.player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, commonAIRef.player.transform.position, commonAIRef.movementSpeed * Time.deltaTime);

            float playerDistance = Vector2.Distance(transform.position, commonAIRef.player.transform.position);
            //Debug.Log(playerDistance);

            if (playerDistance < ignitionRadius && !ignited)
            {
                ignited = true;
                commonAIRef.movementSpeed = ignitionSpeed;
            }

            if (ignited)
            {
                //countdown
                explosionCountdown -= Time.deltaTime;

                //blink
                float sin = Mathf.Sin(Time.time * 10) * 0.3f + 0.7f;
                Color newColor = new Color(255, sin, sin);
                this.GetComponent<SpriteRenderer>().color = newColor;

                //pulse
                float pulse = Mathf.Sin(Time.time * 30) * 0.05f;
                if (commonAIRef.facingLeft)
                {
                    transform.localScale = new Vector3(originalScale.x - pulse, originalScale.y + pulse);
                }
                else
                {
                    transform.localScale = new Vector3((originalScale.x * -1) - pulse, originalScale.y - pulse);
                }
            }

            if (explosionCountdown < 0 && !exploded)
            {
                exploded = true;
                commonAIRef.EnemyDamage(100);
            }
        }
    }
}
