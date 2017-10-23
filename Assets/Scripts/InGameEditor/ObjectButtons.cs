using UnityEngine;

public class ObjectButtons : MonoBehaviour {
    public string PrefabName = "";    
    public bool isEnemy = true;
    
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
                GameObject newObj = EditorManager.Instance.NewEditorItem(PrefabName, this.transform.position, false);
                
                if (isEnemy)
                    EditorManager.Instance.WaveReferencePoint.Enemies.Add(newObj.GetComponent<EditorObject>());
                else
                    EditorManager.Instance.WaveReferencePoint.PowerUps.Add(newObj.GetComponent<EditorObject>());

                newObj.GetComponent<EditorObject>().SetDrag(); 
            }            
        }
    }
}
