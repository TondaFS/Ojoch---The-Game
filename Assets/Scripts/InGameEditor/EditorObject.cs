using UnityEngine;
using System;

[Serializable]
public class EditorObject : MonoBehaviour {
    public string PrefabName = "";
    
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
                isMouseDrag = true;
                screenPosition = Camera.main.WorldToScreenPoint(transform.position);
                offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z));
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMouseDrag = false;
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
                Destroy(this.gameObject);
            }
        }
    }
}
