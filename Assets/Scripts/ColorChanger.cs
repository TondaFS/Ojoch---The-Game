using UnityEngine;
using System.Collections;

public class ColorChanger : MonoBehaviour {
    public bool active = false;

    void Update() {
        if (active)
        {
            float sin = Mathf.Sin(Time.time * 10) * 0.3f + 0.7f;
            Color newColor = new Color(sin, 255, sin);
            this.GetComponent<SpriteRenderer>().color = newColor;            
        }
        else
        {            
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        }
    }
}
