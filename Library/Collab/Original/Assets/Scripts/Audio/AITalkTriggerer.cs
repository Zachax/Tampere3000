using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITalkTriggerer : MonoBehaviour {

	private GameManager GM;
	private TalkPlayer talker;

	private string juku;
	private bool oxygenBroken;
	private int unsaidOxygen;
	private bool hurryUp;
	private float timer;

	// Delay time since last talk before AI starts to hurry up again.
	private int urgeDelay;
	public const int baseDelay = 20;

	void Start () {
		GM = GameObject.Find("GameManager").GetComponent<GameManager>();
		talker = FindObjectOfType<TalkPlayer> ();
		GM.OnStateChange += OnStateChange;
		oxygenBroken = GM.IsShipOxygenDeviceBroken ();
		unsaidOxygen = 1;
		hurryUp = true;
		timer = Time.time;
		urgeDelay = baseDelay;
	}
	
	// Update is called once per frame
	void Update () {
		if (unsaidOxygen > 1) {
			if (oxygenBroken) {
				print ("Should trigger comment for oxygen leak.");
				WarnPlayer ("random");
				unsaidOxygen--;
			} else {
				print ("Should congratulate player");
				CongratulatePlayer ();
				unsaidOxygen--;
			}
		}

		if (oxygenBroken) {
			CheckTime ();
		}
	}

	void OnStateChange () {
		if (GM.IsShipOxygenDeviceBroken () != oxygenBroken) {
			oxygenBroken = GM.IsShipOxygenDeviceBroken();
			unsaidOxygen++;
		}
	}


	// Calls for AI audio track for successful job
	public void CongratulatePlayer() {
		talker.Play("Problem_Solved", 7);
		//Debug.Log ("Should have played");
	}

	public void WarnPlayer(string type) {
		if (type != null) {
			if (type.Equals ("random")) {
				talker.Play ("Problem_NewUndef", 4);
			}
		}
	}

	// Notifies the player to hurry up, if there's been sufficient delay since the last call.
	private void CheckTime() {
		if (talker.isPlaying () && talker.currentLength () > urgeDelay) {
			timer = Time.time;
			urgeDelay = baseDelay / 2;
		} else if (!talker.isPlaying () && Time.time - timer > urgeDelay) {
			talker.Play ("Problem_Unfinished", 8);
			urgeDelay = baseDelay;
		} else if (false) {
		} else if (talker.isPlaying()) {
			timer = Time.time;
		}
	}
}