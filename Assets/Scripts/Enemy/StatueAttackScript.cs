using UnityEngine;
using System.Collections;

public class StatueAttackScript : MonoBehaviour {

    public bool heartAttack = false;
    private WeaponScript weapon;

    void Start() {
        weapon = GetComponent<WeaponScript>();
    }    

    // Update is called once per frame
    void Update () {
        if (heartAttack) {
            weapon.StatueAttack(true, new Vector2(1, 0), 0);             
            weapon.StatueAttack(true, new Vector2(1, -0.5f), -8);
            weapon.StatueAttack(true, new Vector2(1, -1f), -18);
            weapon.StatueAttack(true, new Vector2(1, -1.5f), -28);
            heartAttack = false;            
        }
	}
}
