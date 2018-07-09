using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("BoostUp"))
        {
            transform.Translate(0f,1f,0f);
        }
        if (Input.GetButton("BoostDown"))
        {
            transform.Translate(0f, -1f, 0f);
        }
    }
}
