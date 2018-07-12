using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitShipComponents : MonoBehaviour {

    public string WantedTag;
    public bool debugNote;
    public float damagingForce;
    public string[] ignoredTags;

	// Initializes a series of similarly named objects to be carryables
	void Start () {
        int quota = 0;
        foreach(GameObject go in GameObject.FindGameObjectsWithTag(WantedTag)) {
            CollideDamageReporter script = go.GetComponent<CollideDamageReporter>();
            script.damagingForce = this.damagingForce;
            int iLen = ignoredTags.Length;
            if (iLen > 0) {
                script.ignoreTags = new string[iLen];
                for(int i = 0; i < iLen; ++i) {
                    script.ignoreTags[i] = ignoredTags[i];
                }
            }
            ++quota;
        }
        if (debugNote) {
            Debug.Log(quota + " \"" + WantedTag + 
                "\" tagged objects initialized by 'damagingForce' variable to " + damagingForce + ".");
        }
	}
}
