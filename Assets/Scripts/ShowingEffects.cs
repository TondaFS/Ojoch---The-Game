using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowingEffects : MonoBehaviour {

    public GameObject slowtime;
    public GameObject prdak;
    public GameObject ak47;
    public GameObject smradostit;
    public GameObject duseni;
    public GameObject zmatek;   
    public GameObject soufl;   

    // Use this for initialization
    void Start () {
        slowtime = GameObject.Find("slowtimeui");
        prdak = GameObject.Find("prdakui");
        ak47 = GameObject.Find("ak47ui");
        smradostit = GameObject.Find("smradostitui");
        duseni = GameObject.Find("duseniui");
        zmatek = GameObject.Find("zmatekui");
        soufl = GameObject.Find("souflui");

        slowtime.SetActive(false);
        prdak.SetActive(false);
        ak47.SetActive(false);
        smradostit.SetActive(false);
        duseni.SetActive(false);
        zmatek.SetActive(false);       
        soufl.SetActive(false);
	}
}
