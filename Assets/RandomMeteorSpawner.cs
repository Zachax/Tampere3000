using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMeteorSpawner : MonoBehaviour {

	public GameObject MeteorPrefab;
    public float spawnFrequency; // 0.002f originally
    public float startVelocity; // 10000f originally


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Random.value < spawnFrequency) {
			Vector3 initialPosition = Camera.main.transform.position + (Random.insideUnitSphere * 100f);
			GameObject meteor = GameObject.Instantiate (MeteorPrefab, initialPosition, Random.rotation);

			float scale = 0.2f + Random.value * 2f;
			meteor.transform.localScale = new Vector3 (scale, scale, scale);

			Vector3 direction = Camera.main.transform.position + (Random.insideUnitSphere * 30f) - meteor.transform.position;
            meteor.GetComponent<Rigidbody> ().AddForce (direction * startVelocity * (Random.value + 0.5f));


		}
	}
}
