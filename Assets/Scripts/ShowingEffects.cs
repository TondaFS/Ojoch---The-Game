using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowingEffects : MonoBehaviour {
    
    //Game Effects   
    public GameObject rambouch;
    public GameObject smradostit;
    public GameObject duseni;
    public GameObject zmatek;

    //Odpocitadlo
    public Text rText;
    public Text sText;
    public Text dText;
    public Text zText;
    
    //public GameObject prdak;
    //public GameObject slowtime;
    public GameObject soufl;   

    // Use this for initialization
    void Start () {

        rambouch = GameObject.Find("rambouchui");
        smradostit = GameObject.Find("smradostitui");
        duseni = GameObject.Find("duseniui");
        zmatek = GameObject.Find("zmatekui");
        soufl = GameObject.Find("souflui");

        rText = GameObject.Find("rText").GetComponent<Text>();
        sText = GameObject.Find("sText").GetComponent<Text>();
        dText = GameObject.Find("dText").GetComponent<Text>();
        zText = GameObject.Find("zText").GetComponent<Text>();

        rambouch.SetActive(false);
        smradostit.SetActive(false);
        duseni.SetActive(false);
        zmatek.SetActive(false);
        soufl.SetActive(false);


        //prdak = GameObject.Find("prdakui");
        //slowtime = GameObject.Find("slowtimeui");
        //soufl = GameObject.Find("souflui");
        //prdak.SetActive(false);
        //slowtime.SetActive(false);    
        //soufl.SetActive(false);
    }
}
