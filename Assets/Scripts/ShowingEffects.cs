using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowingEffects : MonoBehaviour {

  
    //public GameObject prdak;
    public GameObject rambouch;
    public GameObject smradostit;
    public GameObject duseni;
    public GameObject zmatek;

    public Text rText;
    public Text sText;
    public Text dText;
    public Text zText;
    //public GameObject slowtime;
    //public GameObject soufl;   

    // Use this for initialization
    void Start () {        
        //prdak = GameObject.Find("prdakui");
        rambouch = GameObject.Find("rambouchui");
        smradostit = GameObject.Find("smradostitui");
        duseni = GameObject.Find("duseniui");
        zmatek = GameObject.Find("zmatekui");

        rText = GameObject.Find("rText").GetComponent<Text>();
        sText = GameObject.Find("sText").GetComponent<Text>();
        dText = GameObject.Find("dText").GetComponent<Text>();
        zText = GameObject.Find("zText").GetComponent<Text>();

        //slowtime = GameObject.Find("slowtimeui");
        //soufl = GameObject.Find("souflui");
        
        //prdak.SetActive(false);
        rambouch.SetActive(false);
        smradostit.SetActive(false);
        duseni.SetActive(false);
        zmatek.SetActive(false);
        //slowtime.SetActive(false);    
        //soufl.SetActive(false);
    }
}
