using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whaleMove : MonoBehaviour {
    Rigidbody rb;
    bool lippu = true;
	GameManager GM;

	AudioSource audio;
	bool audioPlayed = false;
	float startTime;

    // Use this for initialization
    void Start () {
       
		rb = GetComponent<Rigidbody>();
		GM = GameObject.Find("GameManager").GetComponent<GameManager>();
		audio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {

		if (GM.IsWhaleEventStarted())
        {
			if (lippu) {
				rb.AddForce (0, 0, Random.Range (-2500f, -2000f));
				lippu = false;
				startTime = Time.time;
			} else if (Time.time - startTime > 60f) {
				// Destroy after 1 minute
				GameObject.Destroy (gameObject);
			}
		}
    }

	public void OnCollisionEnter(Collision col)
	{
		// Play audio once when colliding something else than another whale (hopefully the ship)
		if (!col.gameObject.CompareTag ("Whale") && !audioPlayed) {
			audio.PlayOneShot (audio.clip);
			audioPlayed = true;
		}
	}
}
