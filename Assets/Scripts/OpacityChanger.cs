using UnityEngine;
using System.Collections;

public class OpacityChanger : MonoBehaviour {
    public float c = 1;
    bool change = true;
    public bool active = false;

    void Update() {
        if (active)
        {
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
            }
        }
        else
        {
            if(c <= 1)
            {
                c += 0.1f;
                if (c > 1)
                {
                    c = 1;
                }
            }
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, c);
        }
    }
}
