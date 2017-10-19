using UnityEngine;
using System.Xml.Serialization;

public class EditorObject : MonoBehaviour{
    /// <summary>
    /// Jméno prefabu nepřítele/powerupu, co se pak ve vlně spawne
    /// </summary>
    public string PrefabName = "";
    /// <summary>
    /// pozice objektu relativně k Wave reference
    /// </summary>
    [XmlIgnore]
    public Vector2 position = new Vector2(0, 0);

    public bool isMouseDrag;
    Vector3 screenPosition;
    Vector3 offset;

    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f);

            if (hit.transform == null)
                return;

            if (hit.transform.gameObject.Equals(this.gameObject))
            {
                SetDrag();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMouseDrag = false;
            CheckPosition();
        }

        if (isMouseDrag)
        {
            //track mouse position.
            Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z);

            //convert screen position to world position with offset changes.
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace) + offset;

            //It will update target gameobject's current postion.
            transform.position = currentPosition;
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f);

            if (hit.transform == null)
                return;

            if (hit.transform.gameObject.Equals(this.gameObject))
            {
                DestroyThis();
            }
        }
    }

    /// <summary>
    /// Nastavi dragovani po kliknuti na objekt v editoru
    /// </summary>
    public void SetDrag()
    {
        isMouseDrag = true;
        screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z));
    }
    /// <summary>
    /// Zkontroluje, zda neni objekt mimo hranice sceny, pokud ano, objekt znicim
    /// </summary>
    public void CheckPosition()
    {        
        float x = transform.position.x;
        float y = transform.position.y;

        if (x < -8.75 || x > 8.75)
            DestroyThis();

        if (y < -4.75 || y > 4.75)
            DestroyThis();

        position = this.transform.localPosition;
    }
    /// <summary>
    /// Zničí tento game object, ale nedříve odstraníme z Wave reference odkaz na tento skript
    /// </summary>
    void DestroyThis()
    {
        if (this.tag.Equals("Enemy"))
            EditorManager.Instance.WaveReferencePoint.Enemies.Remove(this);
        else
            EditorManager.Instance.WaveReferencePoint.PowerUps.Remove(this);

        Destroy(this.gameObject);
    }
}
