using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningBeaconScript : MonoBehaviour {

	private Light light;
	private float originalLightIntensity;
	private AudioSource audio;
	private bool isOn = false;

	GameManager GM;

	void Awake () {
		GM = GameObject.Find("GameManager").GetComponent<GameManager>();
		GM.OnStateChange += OnStateChange;
		this.isOn = GM.AreWarningBeaconsOn ();
	}

	void OnStateChange () {
		this.isOn = GM.AreWarningBeaconsOn ();
	}

	// Use this for initialization
	void Start () {
		light = GetComponentInChildren<Light> ();
		audio = GetComponentInChildren<AudioSource> ();
		originalLightIntensity = light.intensity;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.isOn) {
			light.intensity -= (light.intensity * 1.5f * Time.deltaTime);
			if (light.intensity < 0.1) {
				light.intensity = originalLightIntensity;
				audio.PlayOneShot (audio.clip);
			}
		} else {
			light.intensity = 0f;
			audio.Stop ();
		}
	}
}
