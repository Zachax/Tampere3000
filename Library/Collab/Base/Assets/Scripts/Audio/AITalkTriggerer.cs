using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITalkTriggerer : MonoBehaviour {

	private GameManager GM;
	private TalkPlayer talker;

	private string juku;
	private bool oxygenBroken;
	private int unsaidOxygen;

	void Start () {
		GM = GameObject.Find("GameManager").GetComponent<GameManager>();
		talker = FindObjectOfType<TalkPlayer> ();
		GM.OnStateChange += OnStateChange;
		oxygenBroken = GM.IsShipOxygenDeviceBroken ();
		unsaidOxygen = 1;
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
}