using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

/// <summary>
/// Třída má na starost všechny důležité věci spojené s editorem
/// Ukládání, nahrávání vytváření vln. Spuštění a správa testování vln... A další...
/// </summary>
public class EditorManager : MonoBehaviour {
    public static EditorManager Instance;

    /// <summary>
    /// Reference na objekt aktuálně vytvářené vlny nepřátel
    /// </summary>
    public WaveReference WaveReferencePoint;
    /// <summary>
    /// Referenční bod, ve kterém se objeví vždy nově vytvořený referenční objekt nepřátleské vlny
    /// </summary>
    public Vector2 WaveRefPointPosition = new Vector2(-9f, 0);
    
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this.gameObject);
    }
    void Start()
    {
        WaveReferencePoint = FindObjectOfType<WaveReference>();
        Debug.Log(WaveReferencePoint);
    }

    /// <summary>
    /// Serializuje vlnu a ulozi do xml souboru pojmenovanem dle nazvu vlny
    /// </summary>
    public void SaveWave()
    {
        //vytvoreni nove serializovatelne vlny a ulozeni zakl informaci
        WaveXML newWave = new WaveXML();
        newWave.WaveName = WaveReferencePoint.name;
        newWave.Difficulty = WaveReferencePoint.difficulty;
        newWave.Enemies = new List<EditorObjectXML>();
        newWave.PowerUps = new List<EditorObjectXML>();

        //projdeme vsechny enemaky, vytvorime pro ne serializovatelny objekt a pridame jej do vlny
        foreach (EditorObject o in WaveReferencePoint.Enemies)
        {
            EditorObjectXML eo = new EditorObjectXML();
            eo.PrefabName = o.PrefabName;
            eo.Position = o.position;
            newWave.Enemies.Add(eo);
        }

        //to same co vyse jen pro powerUpy
        foreach (EditorObject o in WaveReferencePoint.PowerUps)
        {
            EditorObjectXML eo = new EditorObjectXML();
            eo.PrefabName = o.PrefabName;
            eo.Position = o.position;
            newWave.PowerUps.Add(eo);
        }

        //vytvorime novy serializer a ulozime vlnu do souboru
        XmlSerializer serializer = new XmlSerializer(typeof(WaveXML));
        StreamWriter writer = new StreamWriter(WaveReferencePoint.name + ".xml");
        serializer.Serialize(writer.BaseStream, newWave);
        writer.Close();
        Debug.Log("Saved");
    }

    /// <summary>
    /// Znici puvodni vlnu nepratel a pote vytvori novou a prázdnou
    /// </summary>
    public void CreateNew()
    {
        Destroy(WaveReferencePoint.gameObject);

        GameObject newWaveRef = new GameObject();
        newWaveRef.name = "WaveReferencePosition";
        newWaveRef.transform.SetParent(this.transform);
        WaveReference wr = newWaveRef.AddComponent(typeof(WaveReference)) as WaveReference;
        WaveReferencePoint = wr;
        newWaveRef.transform.position = WaveRefPointPosition;
    }
    
}
