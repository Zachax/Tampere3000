using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceSuit : MonoBehaviour {

    private GameObject spaceSuit;
    private GameManager GM;
    private bool readyToWear;

    // Use this for initialization
    void Start () {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        spaceSuit = GameObject.Find("acesjustforroomshow (1)");
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0) && readyToWear)
        {
            bool suitOn = spaceSuit.activeSelf;
            spaceSuit.SetActive(!suitOn);
            GM.SetSpaceSuitOn(suitOn);
            //Debug.Log("Suit is " + suitOn);
        }
    }

    void OnTriggerEnter(Collider col) {
        //Debug.Log("Collider found!");
        if (col.gameObject.tag == "Player") {
            readyToWear = true;
        }
    }

    void OnTriggerExit(Collider col) {
        //Debug.Log("Leaving");
        if (col.gameObject.tag == "Player") {
            readyToWear = false;
        }
    }

    public bool IsSpaceSuitReadyToWear() {
        return readyToWear;
    }
}
