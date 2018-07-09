using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipEncapsulator : MonoBehaviour {

    public MeshCollider ship;
    public Bounds bigBounds;

	// Use this for initialization
	void Start () {
        //Bounds bigBounds = this.GetComponent<Bounds>();
        foreach(var r in this.GetComponentsInChildren<Renderer>())
        {
            bigBounds.Encapsulate(r.bounds);
            Debug.Log(r.bounds);
        }
        Debug.Log(bigBounds + " ja " + ship.bounds);
        //ship.bounds = bigBounds;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col) {
        Debug.Log("Entering ship");
    }

    void OnTriggerStay(Collider col) {
        Debug.Log("Inside ship");
    }

    void OnTriggerExit(Collider col) {
        Debug.Log("Outside ship");
    }
}
