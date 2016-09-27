using UnityEngine;
using System.Collections;

public class CoinsSpawner : MonoBehaviour {
    float timeCoin;
    public Transform goldCoin;
    public Transform silverCoin;
    public Transform bronzeCoin;
    
	void Start () {
        timeCoin = Time.time;
	}
	
	// Každých 15 vteřin zavolá funkci SpawnCoin
	void Update () {
	    if(Time.time - timeCoin > 15)
        {
            SpawnCoin();
            timeCoin = Time.time;
        }
	}

    /// <summary>
    /// Vytvoří za obrazovkou minci. Hondota mince se odvíjí dle šance. Nemusí se objevit vůbec nic.
    /// </summary>
    void SpawnCoin()
    {
        int chance = Random.Range(0, 100);
        //Debug.Log(chance);

        if (chance >= 90)
        {
            var newCoin = Instantiate(goldCoin) as Transform;
            newCoin.position = new Vector3(12, Random.Range(-3,5), 0);
        }

        else if(chance > 75)
        {
            var newCoin = Instantiate(silverCoin) as Transform;
            newCoin.position = new Vector3(12, Random.Range(-3, 5), 0);
        } else if(chance > 20)
        {
            var newCoin = Instantiate(bronzeCoin) as Transform;
            newCoin.position = new Vector3(12, Random.Range(-3, 5), 0);
        }
    }
}
