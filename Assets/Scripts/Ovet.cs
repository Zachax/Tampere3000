using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ovet : MonoBehaviour {
    private Rigidbody rig;
	// Use this for initialization
	void Start () {
        rig = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("BoostDown"))
        {
            rig.AddForce(0f, -1f, 0);
        }
    }
}
