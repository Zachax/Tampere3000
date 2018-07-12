using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitCarryableSeries : MonoBehaviour {

    public string ObjectNameStartsWith;
    public bool debugNote;
    public string ItemName;
    public string newTag;

    // If massByVolume ticked, the item mass will be calculated by its size.
    // Otherwise the mass is set to each item by fixed value.
    // Not implemented for now
    //public bool massByVolume;
    public float mass;

	// Initializes a series of similarly named objects to be carryables
	void Start () {
        int quota = 0;
        foreach(GameObject go in GameObject.FindObjectsOfType(typeof(GameObject))) {
            if (go.name.StartsWith(ObjectNameStartsWith)) {
                //Debug.Log(go.name);
                go.AddComponent<Carryable>();
                if (ItemName != null) {
                    go.GetComponent<Carryable>().name = ItemName;
                }
                if (mass > 0) {
                    go.GetComponent<Rigidbody>().mass = mass;
                }
                if (newTag != null && newTag.Length > 0) {
                    go.tag = newTag;
                }
                ++quota;
            }
        }
        if (ObjectNameStartsWith != null && debugNote) {
            Debug.Log(quota + " \"" + ObjectNameStartsWith + 
                "\"'s initialized as carryables by name: \"" + ItemName + "\".");
        }
	}
}
