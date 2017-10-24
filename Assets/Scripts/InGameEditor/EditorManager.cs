using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.UI;

/// <summary>
/// Třída má na starost všechny důležité věci spojené s editorem
/// Ukládání, nahrávání vytváření vln. Spuštění a správa testování vln... A další...
/// </summary>
public class EditorManager : MonoBehaviour {
    public static EditorManager Instance;
    /// <summary>
    /// Slozka, kde se budou ukladat vlny
    /// </summary>
    public string directory = "CustomWaves";
    /// <summary>
    /// Cesta k editor prefabum
    /// </summary>
    public string PathToPrefab = "InGameEditor/";
    /// <summary>
    /// String prefabu, co se bude nacitat pro kazdou vlnu
    /// </summary>
    public string XMLFilePrefab = "InGameEditor/Wave";


    /// <summary>
    /// Reference na go ve scene, kde se objevi seznam custom vln
    /// </summary>
    [Header("Reference na GO")]
    public GameObject LoadMenuRef;
    /// <summary>
    /// Grid kde se objevi vsechny ulozene vlny nepratel
    /// </summary>
    public GameObject LoadGrid;
    /// <summary>
    /// Input na zadavani jmena vlny
    /// </summary>
    public InputField NameInput;
    /// <summary>
    /// Menu s hlaskou o chybe ulozeni
    /// </summary>
    public WarningScript WarningMenu;

    /// <summary>
    /// Reference na objekt aktuálně vytvářené vlny nepřátel
    /// </summary>
    [Header("Ostatní")]    
    public WaveReference WaveReferencePoint;
    /// <summary>
    /// Referenční bod, ve kterém se objeví vždy nově vytvořený referenční objekt nepřátleské vlny
    /// </summary>
    public Vector2 WaveRefPointPosition = new Vector2(-9f, 0);

    private List<GameObject> customListOfXMLS;

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
        customListOfXMLS = new List<GameObject>();
    }

    /// <summary>
    /// Serializuje vlnu a ulozi do xml souboru pojmenovanem dle nazvu vlny
    /// </summary>
    public void SaveWave()
    {
        //vytvoreni nove serializovatelne vlny a ulozeni zakl informaci
        WaveXML newWave = new WaveXML();
        newWave.WaveName = WaveReferencePoint.nameOfWave;
        Debug.Log(newWave.WaveName);

        if (newWave.WaveName.Equals(""))
            newWave.WaveName = WaveReferencePoint.name;

        Debug.Log(newWave.WaveName);
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

        //Kontrola jestli existuje složka, kde budeme vlny ukládat
        if (!Directory.Exists(Application.persistentDataPath + "/" + directory))
        {
            //vytvoření složky
            Directory.CreateDirectory(Application.persistentDataPath + "/" + directory);
        }
        
        //příprava cesty, kde se bude ukládat
        string saveDirectory = Application.persistentDataPath + "/" + directory + "/"+ newWave.WaveName + ".xml";

        if (File.Exists(saveDirectory))
            SetUpSaveWarning(directory, newWave);
        else
            Serialize(saveDirectory, newWave);        
    }

    /// <summary>
    /// Zobrazi save error menu a ulozi reference na vlnu a cestu k ulozeni
    /// </summary>
    /// <param name="p">cesta k ulozeni</param>
    /// <param name="xml">vlna, co hrac chce ulozit</param>
    void SetUpSaveWarning(string p, WaveXML xml)
    {
        WarningMenu.gameObject.SetActive(true);
        WarningMenu.path = p;
        WarningMenu.xml = xml;        
    }

    /// <summary>
    /// Vztvori novy serializer a ulozi vlnu do souboru
    /// </summary>
    /// <param name="path">Cesta kde ulozit vlnu</param>
    /// <param name="newWave">Vlna k ulozeni</param>
    public void Serialize(string path, WaveXML newWave)
    {        
        XmlSerializer serializer = new XmlSerializer(typeof(WaveXML));
        StreamWriter writer = new StreamWriter(path);
        serializer.Serialize(writer.BaseStream, newWave);
        writer.Close();
        Debug.Log("Waver saved!");
    }

    /// <summary>
    /// Načte vlnu dle zadaného názvu a poté zavolá fci, která ve scéně vytvoří objekty a uschová o vlně informace.
    /// </summary>
    /// <param name="name"></param>
    public void LoadWave (string name){
        Debug.Log("Loading wave");

        string loadDirectory = Application.persistentDataPath + "/" + directory + "/" + name + ".xml";

        XmlSerializer serializer = new XmlSerializer(typeof(WaveXML));
        StreamReader reader = new StreamReader(loadDirectory);
        WaveXML deserialized = (WaveXML)serializer.Deserialize(reader.BaseStream);
        reader.Close();

        CreateNew(deserialized);
        TriggerLoadMenu();
    }

    /// <summary>
    /// Znici puvodni vlnu nepratel a pote vytvori novou a prázdnou
    /// </summary>
    public void CreateNew()
    {
        Destroy(WaveReferencePoint.gameObject);

        //novy objekt
        GameObject newWaveRef = new GameObject();
        newWaveRef.name = "WaveReferencePosition";
        newWaveRef.transform.SetParent(this.transform);

        //pridam mu skript
        WaveReference wr = newWaveRef.AddComponent(typeof(WaveReference)) as WaveReference;
        WaveReferencePoint = wr;
        wr.Enemies = new List<EditorObject>();
        wr.PowerUps = new List<EditorObject>();
        newWaveRef.transform.position = WaveRefPointPosition;
    }
    /// <summary>
    /// Znici puvodni vlnu nepratel a vytvori novou dle ulozeneho schematu
    /// </summary>
    /// <param name="wave"></param>
    public void CreateNew(WaveXML wave)
    {
        Destroy(WaveReferencePoint.gameObject);

        //vytvorim novy go
        GameObject newWaveRef = new GameObject();
        newWaveRef.name = wave.WaveName;
        newWaveRef.transform.SetParent(this.transform);

        //pridam mu WaveRef skript
        newWaveRef.AddComponent(typeof(WaveReference));
        WaveReference wr = newWaveRef.GetComponent<WaveReference>();
        WaveReferencePoint = wr;
        wr.Enemies = new List<EditorObject>();
        wr.PowerUps = new List<EditorObject>();
        newWaveRef.transform.position = WaveRefPointPosition;

        //nastavim obtiznost
        wr.difficulty = wave.Difficulty;
        wr.nameOfWave = wave.WaveName;

        //vytvorim a umistim vsechny enemaky
        foreach (EditorObjectXML enemy in wave.Enemies)
        {
            GameObject e = NewEditorItem("Ed_" + enemy.PrefabName, enemy.Position, true);
            EditorObject eo = e.GetComponent<EditorObject>();
            eo.position = enemy.Position;
            wr.Enemies.Add(eo);
        }

        //vytvorim a umistim vsechny powerUPy
        foreach (EditorObjectXML powerUP in wave.PowerUps)
        {
            GameObject p = NewEditorItem("Ed_" + powerUP.PrefabName, powerUP.Position, true);
            EditorObject eo = p.GetComponent<EditorObject>();
            eo.position = powerUP.Position;
            wr.PowerUps.Add(eo);
        }

        Debug.Log("Load complete");
    }

    /// <summary>
    /// Vytvoří nový GO dle jména prefabu a nastaví jeho pozici. Dále objekt přidá pod referenční point vlny. 
    /// </summary>
    /// <param name="prefabName">Jméno prefabu, co vytvořit</param>
    /// <param name="position">Pozice, kde nový objekt umístit</param>
    /// <returns></returns>
    public GameObject NewEditorItem(string prefabName, Vector3 position, bool isLoad)
    {        
        GameObject newObj;
        string name = PathToPrefab + prefabName;

        newObj = Instantiate(Resources.Load(name)) as GameObject;
        newObj.transform.SetParent(WaveReferencePoint.transform);

        if(isLoad)
            newObj.transform.localPosition = position;
        else
            newObj.transform.position = position;
        
        return newObj;
    }

    /// <summary>
    /// Vypina a zapina seznam s ulozenymi vlnami nepratel
    /// </summary>
    public void TriggerLoadMenu()
    {
        LoadMenuRef.SetActive(!LoadMenuRef.activeSelf);
        if (LoadMenuRef.activeSelf)
        {
            LoadFilesFromDirectory();
        }
        else
        {
            foreach (GameObject o in customListOfXMLS) {
                Destroy(o);
            }
            customListOfXMLS.Clear();
        }
    }

    /// <summary>
    /// Zjisti seznam vsech xml vln v adresari custom vln a pro kazdou vztvori polozku v menu
    /// </summary>
    void LoadFilesFromDirectory()
    {
        Debug.Log("Loading Files from Directory");
        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath + "/" + directory + "/");        
        FileInfo[] fileInfo = dir.GetFiles("*.xml");
        
        foreach (FileInfo f in fileInfo)
        {
            CreateWaveMenuItem(f.Name);
        }
    }

    /// <summary>
    /// vytvori novou polozku v load menu s nazvem dostupne custom vlny
    /// </summary>
    /// <param name="nameOfFile">jmeno xml souboru</param>
    void CreateWaveMenuItem(string nameOfFile)
    {
        GameObject o = Instantiate(Resources.Load(XMLFilePrefab)) as GameObject;
        WaveButtonScript wbs = o.GetComponent<WaveButtonScript>();

        //chceme pouze string bez .xml
        nameOfFile = nameOfFile.Substring(0, nameOfFile.Length - 4);        
        wbs.nameOfWave = nameOfFile;
        wbs.GetComponentInChildren<Text>().text = nameOfFile;
        o.transform.SetParent(LoadGrid.transform, false);
        customListOfXMLS.Add(o);
    }
    
}
