using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SputnikAI : MonoBehaviour {
    CommonAI commonAI;

    void Start()
    {
        commonAI = GetComponent<CommonAI>();
        commonAI.startingState = AIStates.stopAndShoot;
        SessionController.instance.sputniksInScene.Add(this.gameObject);
    }


}
