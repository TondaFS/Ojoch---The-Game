using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HS_Script : MonoBehaviour {
    public Animator animator;

    void OnEnable()
    {
        animator = GetComponent<Animator>();
    }

    public void DestroyThis()
    {
        Destroy(this.gameObject);
    }
}
