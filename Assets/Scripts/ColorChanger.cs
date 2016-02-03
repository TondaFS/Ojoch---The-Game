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

            /*
            if (change)
            {
                c -= 0.04f;
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, c);
                if (c <= .5f)
                {
                    change = false;
                }

            }
            else
            {
                c += 0.04f;
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, c);
                if (c >= 1)
                {
                    change = true;
                }
            }*/
        }
        else
        {
            /*if(c <= 1)
            {
                c += 0.1f;
                if (c > 1)
                {
                    c = 1;
                }
            }*/
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        }
    }
}
