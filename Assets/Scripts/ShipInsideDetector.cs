using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipInsideDetector : MonoBehaviour {

    private GameManager GM;

	// Use this for initialization
	void Awake () {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
    void OnTriggerEnter(Collider obj) {
        if (obj.gameObject.tag == "Player") {
            //Debug.Log("Entering ship inside areas.");
            GM.SetPlayerOutside(false);
        }
    }

    void OnTriggerStay(Collider obj) {
        if (obj.gameObject.tag == "Player") {
            GM.SetPlayerOutside(false);
        }
    }

    void OnTriggerExit(Collider obj) {
        if (obj.gameObject.tag == "Player") {
            //Debug.Log("Exiting ship inside areas.");
            GM.SetPlayerOutside(true);
        }
    }
}
