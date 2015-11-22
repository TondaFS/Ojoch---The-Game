using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public int numberOfClicks;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Application.LoadLevel("menu");
        }

        if (Input.GetMouseButtonDown(0))
        {
            ++numberOfClicks;
            Debug.Log(numberOfClicks);
        }
    }

    /*
       public void AdjustScore()
       {

       }*/
}
