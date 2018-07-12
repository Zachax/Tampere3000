using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour {

    void HandleCollision (GameObject go, Collision col) {
        Debug.Log("Handling collision! Got this from: \n" + go.name + "\n" + col.collider.name);
    }
}
