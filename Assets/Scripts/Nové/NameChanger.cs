using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NameChanger : MonoBehaviour {


    void OnMouseEnter()
    {
        GetComponent<Text>().color = Color.red;
    }

    void OnMouseExit()
    {
        GetComponent<Text>().color = Color.black;
    }
}
