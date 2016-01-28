using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CollectingScript : MonoBehaviour {
    //Sprity predmetu
    public Sprite lp;
    public Sprite koreni;
    public Sprite bubble;
    public Sprite sock;
    public Sprite tmp;

    //Promenna na tvorbu obrazku a pozice, kde se maji vytvorit
    public Image prvni;
    public Image druhy;
    public Transform first;

    public bool[] occupied;

    public int pozice;
    public int umisteni;
    public int tmpUmisteni;

    void Start()
    {
        occupied = new bool[3] { false, false, false };
        umisteni = 0;
        tmpUmisteni = 0;
    }    

	public void showObject(int id, int number, int combo)
    {
        pozice = 0;
        
        Sprite coll;

        for(int i = 0; i < occupied.Length; i++)
        {
            if (occupied[i])
            {
                pozice += 60;
            }
            else
            {
                umisteni = i;
                break;
            }
        }       

        switch (number)
        {
            case 1:  
                var firstOne = Instantiate(prvni) as Image;
                firstOne.GetComponent<Transform>().SetParent(first);
                firstOne.GetComponent<Transform>().position = first.position + new Vector3(0, -pozice, 0);                
                firstOne.color = new Color(1, 1, 1, 0);
                coll = CollInstance(id);
                firstOne.sprite = coll;
                firstOne.GetComponent<CollItemScript>().created = true;
                firstOne.GetComponent<CollItemScript>().row = umisteni;
                tmpUmisteni = umisteni;                             
                break;

            case 2:
                var secondOne = Instantiate(druhy) as Image;
                secondOne.GetComponent<Transform>().position = first.position + new Vector3(80, -60 * tmpUmisteni, 0);
                secondOne.GetComponent<Transform>().SetParent(first);
                secondOne.color = new Color(1, 1, 1, 0);
                occupied[tmpUmisteni] = true;

                secondOne.GetComponent<CollItemScript>().created = true;
                secondOne.GetComponent<CollItemScript>().row = tmpUmisteni;
                secondOne.GetComponent<CollItemScript>().second = true;
                secondOne.GetComponent<CollItemScript>().combo = combo;

                coll = CollInstance(id);
                secondOne.sprite = coll;                                  
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
}
