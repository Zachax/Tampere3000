using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirlockOxygenDetector : MonoBehaviour {

    private GameManager GM;
    private IlmaLukkoOviUlompi airlock;

    // Use this for initialization
    void Start () {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        airlock = GameObject.Find("IlmalukkoUlompi").transform.GetChild(0).GetComponent<IlmaLukkoOviUlompi>();
    }

    /*
    void OnTriggerEnter(Collider obj) {
        if (obj.gameObject.tag == "Player") {
            Debug.Log("Player in airlock.");
            GM.SetPlayerOutside(false);
        }
    }
    */

    void OnTriggerStay(Collider obj) {
        if (obj.gameObject.tag == "Player") {
            if (airlock.IsAirlockOpen()) {
                //Debug.Log("airlock has no oxygen!");
                GM.SetPlayerOutside(true);
            } else {
                GM.SetPlayerOutside(false);
            }
        }
    }

    void OnTriggerExit(Collider obj) {
        if (obj.gameObject.tag == "Player") {
            //Debug.Log("Player leaving airlock.");
            GM.SetPlayerOutside(true);
        }
    }
}
