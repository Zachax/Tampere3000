using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGravityDetector : MonoBehaviour {

    private GameManager GM;
    public string warning;
    public bool loseControlWhenOutside;
    public bool slowingForceActive;
    public string slowedObjectType;
    public float slowingForce;

	// Use this for initialization
	void Awake () {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
    void OnTriggerEnter(Collider obj) {
        if (obj.gameObject.tag == "Player") {
            //Debug.Log("Entering Sphere");
            GM.SetHintText("");
            GM.SetPlayerControllable(true);
        } 
        if (slowingForceActive && obj.tag.Equals(slowedObjectType)) {
            obj.attachedRigidbody.velocity /= slowingForce;
        }
    }

    void OnTriggerStay(Collider obj) {
        //Debug.Log("Within sphere");
    }

    void OnTriggerExit(Collider obj) {
        if (obj.gameObject.tag == "Player") {
            //Debug.Log("Outside sphere.");
            if (warning != null) {
                GM.SetHintText(warning);
            }
            if (loseControlWhenOutside) {
                GM.SetPlayerControllable(false);
            }
        }
    }
}
