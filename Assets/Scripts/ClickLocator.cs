using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClickLocator : MonoBehaviour {
    public List<Vector2> mouseClicks = new List<Vector2>();

    private Vector3 lastPoint = new Vector3(0,0);

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = (Vector2)Input.mousePosition;
            mousePos = Camera.main.ScreenToViewportPoint((Vector3)mousePos);
            mousePos = new Vector2((float) System.Math.Round(mousePos.x, 2), (float)System.Math.Round(mousePos.y, 2));
            mouseClicks.Add(mousePos);
        }

        foreach (Vector2 point in mouseClicks)
        {
            Debug.DrawLine(Camera.main.ViewportToWorldPoint(lastPoint), Camera.main.ViewportToWorldPoint((Vector3) point));
            lastPoint = (Vector3)point;
        }


    }
}
