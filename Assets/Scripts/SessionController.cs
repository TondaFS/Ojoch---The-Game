using UnityEngine;
using System.Collections;

public class SessionController : MonoBehaviour
{
    public float gameSpeed = 1;
    public float speedUpTime = 100;

    public int score;
    public int scoreMultiplier;
    public int highscore;

	void FixedUpdate () {
        //Debug.Log("Game runtime: " + Time.time);

        gameSpeed += Time.deltaTime / speedUpTime;

        if (gameSpeed < 1)
        {
            gameSpeed = 1;
        }
    }


}
