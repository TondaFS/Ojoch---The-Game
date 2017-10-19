using UnityEngine;

public class ObjectButtons : MonoBehaviour {
    public string PrefabName = "";
    public string PathToPrefab = "InGameEditor/";
    public bool isEnemy = true;

    private GameObject newObj;
    
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
                string name = PathToPrefab + PrefabName;
                newObj = Instantiate(Resources.Load(name)) as GameObject;
                newObj.transform.position = this.transform.position;
                newObj.transform.SetParent(EditorManager.Instance.WaveReferencePoint.transform);

                if (isEnemy)
                    EditorManager.Instance.WaveReferencePoint.Enemies.Add(newObj.GetComponent<EditorObject>());
                else
                    EditorManager.Instance.WaveReferencePoint.PowerUps.Add(newObj.GetComponent<EditorObject>());

                newObj.GetComponent<EditorObject>().SetDrag(); 
            }            
        }
    }
}
