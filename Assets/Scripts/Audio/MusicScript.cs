/* This class plays and changes background music.
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour {

	public Sound[] tracks;
	public bool musicOn;

	public AudioSource musicSource;

	public static MusicScript instance;

	void Awake() {
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
			return;
		}

		DontDestroyOnLoad (gameObject);

		if (tracks != null) {
			foreach(Sound s in tracks) {
				s.source = gameObject.AddComponent<AudioSource> ();
				s.source.clip = s.clip;

				s.source.volume = s.volume;
				s.source.pitch = s.pitch;
				s.source.loop = true;
			}
		}
	}

	void Start () {
		if (tracks != null && tracks.Length > 0) {
			musicSource.clip = tracks[0].clip;
		}
	}

	// If key M is pressed, music track is changed, or turned off if the last track.
	// Disabled for now
	void Update () {
		if (Input.GetKeyDown (KeyCode.M) && false) {
			if (musicSource.clip == null) {
				//musicSource.clip = musicClip1;
				musicSource.Play ();
			} else if (false) {
				//musicSource.clip = musicClip2;
				musicSource.Play ();
			} else {
				musicSource.Stop ();
				musicSource.clip = null;
			}
		}

	}

	public void Play() {
		if (musicSource != null) {
			musicSource.Play ();
		}
	}

	public void Stop() {
		if (musicSource != null) {
			musicSource.Stop ();
		}
	}

	// Not in use for now
	public void NextTrack() {
		if (tracks != null) {

		}
	}

	public void ChangeToTrack(string name) {
		if (tracks != null && name != null) {
			Sound s = Array.Find (tracks, sound => sound.name == name);
			if (s != null) {
				musicSource.clip = s.clip;
				musicSource.Play ();
			} else {
				Debug.LogWarning ("Sound '" + name + "' not found!");
			}
		} else {
			Debug.LogWarning ("Sound list not found!");
		}
	}
}