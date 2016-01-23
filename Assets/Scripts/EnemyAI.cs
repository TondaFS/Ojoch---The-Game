using UnityEngine;
using System.Collections;
using System;

public enum AIStates
{
    let,
    kamikaze,
    diagonalUD

}

public class EnemyAI : MonoBehaviour {

    public AIStates current;
    public float movementSpeed;
	/*
	void Update ()
    {
        switch (current)
        {
            case AIStates.diagonalUD:
                diagonalUD();
                break;                
        }
	
	}
*/
    private void diagonalUD()
    {
        throw new NotImplementedException();
    }
}
