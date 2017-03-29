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

    void Start()
    {
        originalScale = transform.localScale;
        ignitionSpeed = GetComponent<CommonAI>().movementSpeed + 1;        
    }

    void Update()
    {
        if (GetComponent<CommonAI>().currentState == AIStates.kamikaze)
        {
            if (GetComponent<CommonAI>().turns)
            {
                GetComponent<CommonAI>().TurnAtPlayer();
            }
            Kamikaze();
        }
    }

    /// <summary>
    /// AI leti za hracem, kdyz je blizko, zazehne a vybouchne
    /// </summary>
    private void Kamikaze()
    {
        if (GetComponent<CommonAI>().player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, GetComponent<CommonAI>().player.transform.position, GetComponent<CommonAI>().movementSpeed * Time.deltaTime);

            float playerDistance = Vector2.Distance(transform.position, GetComponent<CommonAI>().player.transform.position);
            //Debug.Log(playerDistance);

            if (playerDistance < ignitionRadius)
            {
                ignited = true;
                GetComponent<CommonAI>().movementSpeed = ignitionSpeed;
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
                if (GetComponent<CommonAI>().facingLeft)
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
                GetComponent<EnemyHealth>().EnemyDamage(100);
            }
        }
    }
}
