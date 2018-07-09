using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carryable : MonoBehaviour {

	public string name;
	public Vector3 rotation;
	public Vector3 offset;

	private bool isGrabbed;
	private Rigidbody rb;
	private Quaternion rotationOffset;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rotationOffset = Quaternion.Euler (rotation);
	}
	
	// Update is called once per frame
	void Update () {
		if (isGrabbed) {
			transform.position = GetCarryPosition ();
			transform.rotation = GetCarryRotation ();
		}
	}

	private Vector3 GetCarryPosition() {
		return Camera.main.transform.position + (Camera.main.transform.forward * 0.5f) + (Camera.main.transform.rotation * offset);
	}

	private Quaternion GetCarryRotation() {
		return Camera.main.transform.rotation * rotationOffset;
	}

	private void toggleColliders(bool enabled) {
		foreach (Collider c in GetComponentsInChildren<Collider>()) {
			c.enabled = enabled;
		}
	}

	public void Grab () {
		rb.isKinematic = true;
		rb.detectCollisions = false;
		rb.useGravity = false;
		toggleColliders (false);
		LeanTween.move (gameObject, GetCarryPosition (), 0.2f).setOnComplete (GrabFinished);
	}

	private void GrabFinished() {
		isGrabbed = true;
	}

	public void Release () {
		isGrabbed = false;
		rb.isKinematic = false;
		rb.detectCollisions = true;
		rb.useGravity = true;
		toggleColliders (true);
		rb.AddForce (Camera.main.transform.forward * 10f);
		rb.AddTorque (new Vector3(Random.value - 0.5f, Random.value - 0.5f, Random.value - 0.5f));
	}
}
