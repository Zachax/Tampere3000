using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour {

	public string soundName;
	public int randomValue;

	void Awake() {
		if (randomValue > 0) {
			FindObjectOfType<TalkPlayer> ().Play (soundName, randomValue);
			//Debug.Log ("Here!!!" + FindObjectOfType<SfxPlayer> ());
		} else {
			FindObjectOfType<TalkPlayer> ().Play (soundName);
			//Debug.Log ("Here too!!!");
		}
	}
}
