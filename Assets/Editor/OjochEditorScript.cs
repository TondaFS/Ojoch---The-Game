using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class OjochEditorScript : MonoBehaviour {
    static GameObject Enemies;
    static GameObject Powerups;
        
    static OjochEditorScript()
    {
        Enemies = GameObject.Find("Enemies");
        Powerups = GameObject.Find("PowerUps");
    }

    #region Přidávání Power Upů
    [MenuItem("GameObject/Create Other/Ojoch/Předměty/Mýdlo")]
    [MenuItem("Ojoch/Předměty/Mýdlo")]
    static void AddBubbles()
    {
        Debug.Log("Adding Soap");
        GameObject o = Instantiate(Resources.Load("Powerups/Soap")) as GameObject;
        o.transform.SetParent(Powerups.transform);
    }

    [MenuItem("GameObject/Create Other/Ojoch/Předměty/Ponožka")]
    [MenuItem("Ojoch/Předměty/Ponožka")]
    static void AddSock()
    {
        Debug.Log("Adding Sock");
        GameObject o = Instantiate(Resources.Load("Powerups/Sock")) as GameObject;
        o.transform.SetParent(Powerups.transform);
    }

    [MenuItem("GameObject/Create Other/Ojoch/Předměty/Koření")]
    [MenuItem("Ojoch/Předměty/Koření")]
    static void AddSpice()
    {
        Debug.Log("Adding Spice");
        GameObject o = Instantiate(Resources.Load("Powerups/Spice")) as GameObject;
        o.transform.SetParent(Powerups.transform);
    }
    #endregion
    #region Přidávání nepřátel
    [MenuItem("GameObject/Create Other/Ojoch/Nepřátelé/Pták")]
    [MenuItem("Ojoch/Nepřátelé/Pták")]
    static void AddBird()
    {
        Debug.Log("Adding Bird");
        GameObject o = Instantiate(Resources.Load("Enemies/Bird")) as GameObject;
        o.transform.SetParent(Enemies.transform);
    }

    [MenuItem("GameObject/Create Other/Ojoch/Nepřátelé/Prase")]
    [MenuItem("Ojoch/Nepřátelé/Prase")]
    static void AddPig()
    {
        Debug.Log("Adding Pig");
        GameObject o = Instantiate(Resources.Load("Enemies/Pig")) as GameObject;
        o.transform.SetParent(Enemies.transform);
    }

    [MenuItem("GameObject/Create Other/Ojoch/Nepřátelé/Pokoutnik")]
    [MenuItem("Ojoch/Nepřátelé/Pokoutnik")]
    static void AddPokoutnik()
    {
        Debug.Log("Adding Pokoutnik");
        GameObject o = Instantiate(Resources.Load("Enemies/Pokoutnik")) as GameObject;
        o.transform.SetParent(Enemies.transform);
    }

    [MenuItem("GameObject/Create Other/Ojoch/Nepřátelé/Krysa")]
    [MenuItem("Ojoch/Nepřátelé/Krysa")]
    static void AddRat()
    {
        Debug.Log("Adding Rat");
        GameObject o = Instantiate(Resources.Load("Enemies/Rat")) as GameObject;
        o.transform.SetParent(Enemies.transform);
    }

    [MenuItem("GameObject/Create Other/Ojoch/Nepřátelé/Veverka")]
    [MenuItem("Ojoch/Nepřátelé/Veverka")]
    static void AddSquirrel()
    {
        Debug.Log("Adding Squirrel");
        GameObject o = Instantiate(Resources.Load("Enemies/Squirrel")) as GameObject;
        o.transform.SetParent(Enemies.transform);
    }
    #endregion
}
