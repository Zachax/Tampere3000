using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMeteorSpawner : MonoBehaviour {

	public GameObject MeteorPrefab;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Random.value < 0.002f) {
			Vector3 initialPosition = Camera.main.transform.position + (Random.insideUnitSphere * 100f);
			GameObject meteor = GameObject.Instantiate (MeteorPrefab, initialPosition, Random.rotation);

			float scale = 0.2f + Random.value * 2f;
			meteor.transform.localScale = new Vector3 (scale, scale, scale);

			Vector3 direction = Camera.main.transform.position + (Random.insideUnitSphere * 30f) - meteor.transform.position;
			meteor.GetComponent<Rigidbody> ().AddForce (direction * 10000f * (Random.value + 0.5f));


		}
	}
}
