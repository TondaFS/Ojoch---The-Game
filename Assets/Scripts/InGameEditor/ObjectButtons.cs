using UnityEngine;

public class ObjectButtons : MonoBehaviour {
    public string PrefabName = "";
    public string PathToPrefab = "InGameEditor/";
    public GameObject WavePointRef;

    private GameObject newObj;
    
    void Start()
    {
        WavePointRef = GameObject.Find("WaveReferencePosition");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f);

            if (hit.transform == null)
                return;

            if (hit.transform.gameObject.Equals(this.gameObject))
            {
                Debug.Log("Instantiate " + PrefabName);
                string name = PathToPrefab + PrefabName;
                newObj = Instantiate(Resources.Load(name)) as GameObject;
                newObj.transform.position = new Vector3(0,0,0);
                newObj.transform.SetParent(WavePointRef.transform);               
                Debug.Log("Done");
            }
            
        }
    }
}
