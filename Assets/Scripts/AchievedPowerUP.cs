using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AchievedPowerUP : MonoBehaviour {

    public Sprite bublinace;
    public Sprite prdak;
    public Sprite panCasu;
    public Sprite duseni;
    public Sprite zmatek;
    public Sprite napojLasky;
    public Sprite akacko;
    public Sprite smradostit;
    public Sprite nitro;
    public Sprite soufl;

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
        if(time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            opacity -= 0.01f;
            GetComponent<Image>().color = new Color(1, 1, 1, opacity);
            if (opacity <= 0)
            {
                GameObject.Find("Ojoch").GetComponent<CollectingScript>().occupied[row] = false;
                Destroy(gameObject);
            }
        }
    }

    public Sprite PowerImage(int id)
    {
        switch (id)
        {
            case 2:
                return bublinace;
            case 6:
                return zmatek;
            case 9:
                return smradostit;
            case 21:
                return prdak;
            case 23:
                return nitro;
            case 28:
                return akacko;
            case 4:
                return napojLasky;
            case 11:
                return panCasu;
            case 16:
                return soufl;
            case 40:
                return duseni;
        }
        return null;
    }
}
