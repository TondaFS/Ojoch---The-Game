using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowingEffects : MonoBehaviour {

    public GameObject ak47;
    public GameObject kosteni;
    public GameObject prdak;
    public GameObject zmatek;
    public GameObject ultrakejch;
    public GameObject slowtime;
    public GameObject soufl;

    public Text ak47Text;
    public Text kosteniText;
    public Text prdakText;
    public Text zmatekText;
    public Text ultrakejchText;
    public Text slowtimeText;
    public Text souflText;

    // Use this for initialization
    void Start () {
        ak47.SetActive(false);
        kosteni.SetActive(false);
        prdak.SetActive(false);
        zmatek.SetActive(false);
        ultrakejch.SetActive(false);
        slowtime.SetActive(false);
        soufl.SetActive(false);
	}
}
