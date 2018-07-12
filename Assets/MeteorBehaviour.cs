using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorBehaviour : MonoBehaviour {

	public GameObject explosionPrefab;

	private float createTime;

	// Use this for initialization
	void Start () {
		createTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - createTime > 15f) {
			GameObject.Destroy (gameObject);
		}
	}

	public void OnCollisionEnter(Collision col)
    {
        GameObject explosion = GameObject.Instantiate (explosionPrefab, transform.position, Random.rotation);
        explosion.GetComponent<ParticleSystem>().Play();
        GetComponent<AudioSource>().Play();
        //Debug.Log("Partic: " + explosion.GetComponent<ParticleSystem>() + " Audio: " + GetComponent<AudioSource>());
		GameObject.Destroy (gameObject);
	}
}
