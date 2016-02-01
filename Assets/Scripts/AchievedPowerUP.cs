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
                GameObject.Find("Ojoch").GetComponent<CollectingScript>().occupied[row] = false;
                Destroy(gameObject);
            }
        }
    }

    //Vybere obrazek PoweUpu
    public Sprite PowerImage(int id)
    {
        switch (id)
        {
            case 2:
                return bublinace;            
            case 9:
                return napojLasky;            
            case 16:
                return nitro;
            case 21:
                return zmatek;
            case 28:
                return duseni;
            case 40:
                return rambouch;

            /*
            case 6:
                return zmatek;
            case 23:
                return nitro;
            
            case 4:
                return napojLasky;
            case 11:
                return panCasu;
            */
            
        }
        return null;
    }
}
