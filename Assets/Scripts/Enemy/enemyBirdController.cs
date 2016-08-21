using UnityEngine;
using System.Collections;

public class enemyBirdController : MonoBehaviour {

    public Animator animator;
    public bool upDown;
    public bool upDownMore;

	void Awake () {
        animator = gameObject.GetComponent<Animator>();
        animator.SetBool("upDown", upDown);
        animator.SetBool("upDownMore", upDownMore);
	}
}
