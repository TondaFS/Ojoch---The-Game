using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CollectingScript : MonoBehaviour {
    //Promenne 
    public Sprite lp;
    public Sprite koreni;
    public Sprite bubble;
    public Sprite sock;
    public Sprite uivisible;
    public Sprite tmp;

    public bool occupied = false;

    public Image prvni;
    public Image druhy;

    public float time = 0;

    void Update() {
        if (time > 0)
        {
            time -= Time.deltaTime;
            if(time <= 0)
            {
                RemoveImages();
                if (occupied)
                {
                    prvni.sprite = tmp;
                    occupied = false;
                }
            }
        }

    }

	public void showObject(int id, int number)
    {
        
        Sprite coll;
        switch (number)
        {
            case 1:
                
                if (prvni.sprite != uivisible)
                {
                    occupied = true;
                }
                coll = CollInstance(id);
                if (occupied)
                {
                    tmp = coll;
                }
                else
                {
                    prvni.sprite = coll;
                }                
                break;

            case 2:
                coll = CollInstance(id);
                druhy.sprite = coll;                
                time = 1;
                break;
        }
    }
	
    public Sprite CollInstance(int id)
    {
        switch (id)
        {            
            case 1:
                return bubble;

            case 3:
                return lp;                

            case 8:
                return sock;                

            case 20:
                return koreni;
        }
        return null;    
    }

    public void RemoveImages()
    {
        prvni.sprite = uivisible;
        druhy.sprite = uivisible;
    }
	
}
