using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CollItemScript : MonoBehaviour {
    public Transform first;

    public bool created;
    public bool move;
    public bool second = false;
    public float difference;
    public int row;
    public float fast = 0f;
    public int combo = 0;

    private GameObject firstOne;

    public Image powerUp;


    void Start() {        
        first = GameObject.Find("prvniPozice").GetComponent<Transform>();
        move = false;
        difference = 0.2f;


        foreach (GameObject objekt in GameObject.FindGameObjectsWithTag("imageOne"))
        {
            if(objekt.GetComponent<CollItemScript>().row == row)
            {
                firstOne = objekt;
            } 
        }            
    }

    void Update()
    {
        //Pokud byl objekt prave vyvtoren, bude se jeho opacita menit z 0 na 1
        if (created)
        {            
            GetComponent<Image>().color += new Color(0, 0, 0, 0.1f);
            //az se tak stane, prestane se
            if (GetComponent<Image>().color.a >= 1)
            {
                created = false;
                //a pokud se jedna o druhy sebrany predmet, zacne se pohybovat na pozici prveho
                if (second)
                {
                    move = true;
                }
            }
        }
        if (move)
        {            
            MoveToFirst();
        }
    }
    
    /// <summary>
    /// Posune obrázek na pozici prvního předmětu
    /// </summary>
    public void MoveToFirst()
    {
        fast += 2f;
        Vector3 posunuti = new Vector3(0, row * 120, 0); 
        transform.position = Vector3.MoveTowards(transform.position, first.transform.position - posunuti, fast * Time.deltaTime);
        if (((first.transform.position - posunuti) - transform.position).sqrMagnitude <= (difference*difference))
        {
            move = false;
            RemoveImages();
        }
    }
    
    /// <summary>
    /// Odstraní obrázky předmětů a na jejich pozici vytvoří odpovídající obrázek PowerUpu
    /// </summary>
    public void RemoveImages()
    {
        Create(transform.position, combo); 
        Destroy(firstOne);
        Destroy(gameObject);
    }
   
    
    /// <summary>
    /// Vytvoří PowerUp
    /// </summary>
    /// <param name="position">Pozice vytvoření</param>
    /// <param name="id">Id powerUpu</param>
    public void Create(Vector3 position, int id)
    {
        var poowerUp = Instantiate(powerUp) as Image;
        poowerUp.transform.position = position;
        poowerUp.sprite = poowerUp.GetComponent<AchievedPowerUP>().PowerImage(id);
        poowerUp.transform.SetParent(GameObject.Find("COLLECTED ITEMS").transform);
        poowerUp.GetComponent<AchievedPowerUP>().row = row;
    }
}
