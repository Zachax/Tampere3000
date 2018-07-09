using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenEffectBehavior : MonoBehaviour {

	GameManager GM;
	public GameObject[] togglableChildren;
	AudioSource audio;
	bool wasBroken;

	// Use this for initialization
	void Start () {
		GM = GameObject.Find("GameManager").GetComponent<GameManager>();
		GM.OnStateChange += OnStateChange;
		audio = GetComponent<AudioSource> ();
		wasBroken = GM.IsShipOxygenDeviceBroken ();
	}

	void OnStateChange() {
	 	bool isBroken = GM.IsShipOxygenDeviceBroken ();
		foreach(GameObject c in togglableChildren) {
			c.SetActive (!isBroken);
		}
		if (wasBroken && !isBroken && audio != null) {
			audio.PlayOneShot (audio.clip);
		}
		wasBroken = isBroken;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
