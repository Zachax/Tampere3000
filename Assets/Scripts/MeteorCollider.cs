using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorCollider : MonoBehaviour {

    void OnCollisionEnter(Collision col) {
        
        GameObject.Destroy(this);
    }
}
