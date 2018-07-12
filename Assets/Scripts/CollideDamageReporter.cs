using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is for reporting received impact damage on given module/component.
public class CollideDamageReporter : MonoBehaviour {

    private GameManager GM;

    // The force (mass * velocity) of colliding object which causes damage
    public float damagingForce;
    // Can also be initialized by iterator script
    public string[] ignoreTags;
    // Just for editor note
    public bool DontEditManuallyIteratorInUse;

	// Use this for initialization
	void Start () {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
    void OnCollisionEnter(Collision col) {
        //Debug.Log("I'm " + this.name + " and I was collided by " + col.collider.name + " at speed " + 
            //col.relativeVelocity.magnitude + " and impulse " + col.impulse.magnitude);
    }

    void OnCollisionStay(Collision col) {
        //Debug.Log("I am staying.");
    }

    void OnCollisionExit(Collision col) {
        //Debug.Log("Leaving collision zone.\n" + this.name + "\nHit by: " + col.collider.name);
    }

    void OnTriggerEnter(Collider col) {
        bool ignore = false;

        foreach(string tagi in ignoreTags) {
            if (col.tag == null || col.tag.Equals(tagi)) {
                ignore = true;
                break;
            }
        }

        /*
        if (col.attachedRigidbody != null) {
            Debug.Log("Collider's speed: " + col.attachedRigidbody.velocity.magnitude);
        } else if (col.tag != null && col.tag.Equals("Player")) {
            Debug.Log("Collider thing's speed: " 
            + col.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().getMoveSpeed().magnitude);
        }
        */
        //Debug.Log("I am " + this.name + " and I'm triggered by " + col.name);



        if (!ignore) {
            float colVel = col.attachedRigidbody.velocity.magnitude;
            float colMass = col.attachedRigidbody.mass;
            float force = colVel * colMass;
            if (force >= damagingForce) {
                Debug.Log(this.name + " is hit by " + col.name + " with DANGEROUS force of " + force);
                GM.DamageToShip(this.name, force - damagingForce, this.tag);
            } else {
                Debug.Log(this.name + " is hit by " + col.name + " with force of " + force);
            }
        }
    }
}
