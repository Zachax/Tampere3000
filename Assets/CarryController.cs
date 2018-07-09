using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryController : MonoBehaviour {

	public Carryable objectOnCarry = null;
	private GameManager GM;
	private SpaceSuit spaceSuit;

	// Use this for initialization
	void Start () {
		GM = GameObject.Find("GameManager").GetComponent<GameManager>();
		spaceSuit = GameObject.Find("IlmaOsa").GetComponent<SpaceSuit>();
	}
	
	// Update is called once per frame
	void Update () {

		// Set object on sight
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 1.5f)) {
			Carryable c = hit.collider.gameObject.GetComponent<Carryable> ();
			if (c != null) {
				GM.SetObjectOnSight(c);
			}
		} else {
			GM.SetObjectOnSight(null);
		}

		// Set object on carry
		Carryable objectOnSight = GM.GetObjectOnSight ();
		Carryable objectOnCarry = GM.GetObjectOnCarry ();
		if (Input.GetMouseButtonDown (0) && !spaceSuit.IsSpaceSuitReadyToWear()) {
			if (objectOnCarry != null) {
				objectOnCarry.Release ();
				GM.SetObjectOnCarry(null);
			} else if (objectOnSight != null) {
				objectOnSight.Grab ();
				GM.SetObjectOnCarry(objectOnSight);
			}
		}
	}
}
