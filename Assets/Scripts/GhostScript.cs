using UnityEngine;
using System.Collections;

public class GhostScript : MonoBehaviour {
    public float ghostOpacity = 0;
    public float up = 0;
    public float osa = 0;
    public float neco = 0.1f;
	
	void Update () {        
        osa += 0.1f;
        //Duch leti vzhuru
        if (ghostOpacity >= 1)
        {
            up += 0.1f;            
            GetComponent<Transform>().position = new Vector3(GetComponent<Transform>().position.x + Mathf.Cos(osa)/10, up, 0);
        }
        //Dokud neni pruhlednost 1, tak se zvetsuje
        else
        {                     
            GetComponent<Transform>().position = new Vector3(GetComponent<Transform>().position.x + Mathf.Cos(osa)/25, 0, 0);
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, ghostOpacity);
            ghostOpacity += 0.005f;
        }
	}

    //Znici se po opusteni obrazovky
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
