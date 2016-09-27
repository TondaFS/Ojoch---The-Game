using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AchievedPowerUP : MonoBehaviour {

    public Sprite bublinace;
   
    public Sprite duseni;
    public Sprite zmatek;
    public Sprite napojLasky;
    public Sprite rambouch;    
    public Sprite nitro;
    //public Sprite soufl;
    //public Sprite prdak;
    //public Sprite panCasu;
    //public Sprite smradostit;

    public float opacity;
    public float time;
    public int row;  

    void Start()
    {
        opacity = 1;
        time = 0.5f;
    }
    
    void Update()
    {
        //Dokud nevyprsi cas, nic se nedeje
        if(time > 0)
        {
            time -= Time.deltaTime;
        }
        //Pak se zacne snizovat pruhlednost
        else
        {
            opacity -= 0.01f;
            GetComponent<Image>().color = new Color(1, 1, 1, opacity);

            //Az se obrazek nakonec znici
            if (opacity <= 0)
            {
                OjochManager.instance.ojochCollect.occupied[row] = false;
                Destroy(gameObject);
            }
        }
    }
    
    /// <summary>
    /// Vybere obrázek PowerUpu
    /// </summary>
    /// <param name="id">ID PowerUpu</param>
    /// <returns></returns>
    public Sprite PowerImage(int id)
    {
        switch (id)
        {
            case 2:
                return zmatek;            
            case 9:
                return nitro;            
            case 16:
                return duseni;
            case 21:
                return bublinace;
            case 28:
                return rambouch;
            case 40:
                return napojLasky;
        }
        return null;
    }
}
