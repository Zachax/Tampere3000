using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITalkTriggerer : MonoBehaviour {

	private GameManager GM;
	private TalkPlayer talker;

	private bool oxygenBroken;
	private int unsaidOxygen;
	private bool oxygenDangerous;
	private bool whaleEventStarted = false;
    private bool whaleEventStopped = false;
	private bool plantOnFire = false;
	private bool fireStopped = false;
    private bool earthSeen = false;
    private bool AIHasPanicked = false;

	private bool hurryUp;
	private float timer;

    // Makes sure there is not *too* many lines commented about a major event,
    // even if the event is left running.
    private int eventSaid;

	// Delay time since last talk before AI starts to hurry up again.
	private float urgeDelay;
	// Delay until AI starts talking if nothing is happening otherwise.
	private float idleDelay;
	// Delay time for a major event extra comment to trigger
	private float eventDelay;
	// Modifier multiplier for base delay to idle delay modifier.
	private float idmod;
	// Base delay value
	public const float baseDelay = 20f;

	void Start () {
		GM = GameObject.Find("GameManager").GetComponent<GameManager>();
		talker = FindObjectOfType<TalkPlayer> ();
		GM.OnStateChange += OnStateChange;
		oxygenBroken = GM.IsShipOxygenDeviceBroken ();
		unsaidOxygen = 1;

		idmod = 1f;
		eventDelay = baseDelay;
		urgeDelay = baseDelay;
		idleDelay = baseDelay * idmod;

        eventSaid = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (unsaidOxygen > 1) {
			if (oxygenBroken) {
				//print ("Should trigger comment for oxygen leak.");
				WarnPlayer ("random");
				unsaidOxygen--;
			} else {
				//print ("Should congratulate player");
				CongratulatePlayer ();
				unsaidOxygen--;
			}
		}

		if (whaleEventStarted && !whaleEventStopped) {
            CheckEventTime("whales");
        } else if (earthSeen) {
            CheckEventTime("earth");
        }


		if (oxygenBroken) {
			CheckHurryTime ();
		} else {
			CheckIdleTime ();
		}
        //Debug.Log(eventDelay);
	}

	void OnStateChange () {
        if (GM.IsEarthJumpEventStarted() && GM.IsSpaceSuitOn() && !AIHasPanicked) {
            talker.Play("Earth_PilotEscaping", 2);
            AIHasPanicked = true;
        }
		if (GM.IsShipOxygenDeviceBroken () != oxygenBroken) {
			oxygenBroken = GM.IsShipOxygenDeviceBroken();
			unsaidOxygen++;
		}
		if (GM.IsOxygenLevelDangerous () != oxygenDangerous) {
			oxygenDangerous = GM.IsOxygenLevelDangerous();
			if (oxygenDangerous) {
				WarnPlayer ("oxygen");
			}
		}
		if (GM.IsWhaleEventStarted () && !whaleEventStarted && !whaleEventStopped) {
			whaleEventStarted = true;
			talker.Play ("Whales_Sighted", 4);
            eventSaid++;
            ResetTimer();
        }
        if (GM.IsWhaleEventStopped()) {
            whaleEventStopped = true;
        }

		if (GM.IsPlantOnFire () && !plantOnFire) {
			plantOnFire = true;
			talker.Play ("Problem_Fire", 3);
		}

		if (GM.IsFireStopped () && !fireStopped) {
			fireStopped = true;
			CongratulatePlayer ();
		}

        if (GM.IsEarthOnSight() && !earthSeen) {
            talker.Play("Earth_Sighted", 4);
            earthSeen = true;
            eventSaid = 0;
            ResetTimer();
            Debug.Log("State noticed");
        }
	}


	// Calls for AI audio track for successful job
	public void CongratulatePlayer() {
		talker.Play("Problem_Solved", 7);
		//Debug.Log ("Should have played");
	}

	// Warns player for a new danger situation.
	// "random" is for a new undefined problem
	public void WarnPlayer(string type) {
		if (type != null) {
			if (type.Equals ("random")) {
				talker.Play ("Problem_NewUndef", 4);
			} else if (type.Equals ("oxygen")) {
				talker.Play ("Problem_Oxygen", 5);
			}
		}
	}

	// Notifies the player to hurry up, if there's been sufficient delay since the last call.
	private void CheckHurryTime() {
		if (talker.isPlaying () && talker.currentLength () > urgeDelay) {
			timer = Time.time;
			urgeDelay = baseDelay / 2;
		} else if (!talker.isPlaying () && Time.time - timer > urgeDelay) {
			talker.Play ("Problem_Unfinished", 8);
			urgeDelay = baseDelay;
			ResetTimer();
		} else if (false) {
		} else if (talker.isPlaying()) {
			ResetTimer();
		}
	}

	// Checks if it's been sufficiently long time of talk since the last idle comment
	private void CheckIdleTime() {
		if (talker.isPlaying () && talker.currentLength () > idleDelay) {
			timer = Time.time;
			idleDelay = baseDelay / 2;
		} else if (!talker.isPlaying () && Time.time - timer > idleDelay) {
			talker.Play ("IdleRnd", 14);
			idleDelay = baseDelay * idmod;
			ResetTimer();
		} else if (talker.isPlaying()) {
			ResetTimer();
		}
	}

    /*
    private void CheckEscapeTime() {
        if (talker.isPlaying () && talker.currentLength () > eventDelay) {
            timer = Time.time;
            eventDelay = baseDelay / 3;
        } else if (!talker.isPlaying () && Time.time - timer > eventDelay) {
            talker.Play("Earth_PilotEscaping", 2);
            ResetTimer();
            eventDelay = baseDelay / 1.5f;
        } else if (talker.isPlaying()) {
            eventDelay = baseDelay / 4;
        }
    }
    */

	// Checks if it's already time to comment more about a major event
	private void CheckEventTime(string eventName) {
        //Debug.Log("Should be called in AITalk: " + eventName);
        if (eventSaid < 6)
        {
            if (talker.isPlaying() && talker.currentLength() > eventDelay && eventSaid < 1)
            {
                timer = Time.time;
                eventDelay = baseDelay / 10;
                Debug.Log("AITalk: 1st event check");
            }
            else if (!talker.isPlaying() && Time.time - timer > eventDelay && eventSaid < 3)
            {
                if (eventName.Equals("whales")) {
                    talker.Play("Whales_Comment", 7);
                } else if (eventName.Equals("earth")) {
                    if (GM.IsEarthMissedEventStarted()) {
                        if (eventSaid < 1) {
                            talker.Play("Earth_MovingTooFast", 1);
                        } else {
                            talker.Play("MadAI", 1);
                        }
                    } else if (GM.IsEarthCrashEventStarted()) {
                        if (eventSaid < 1) {
                            talker.Play("Earth_CrashCourse", 1);
                        } else {
                            talker.Play("Problem_MoreDamage", 4);
                        }
                    } else if (GM.IsEarthJumpEventStarted()) {
                        if (eventSaid < 1) {
                            talker.Play("AIDeny", 3);
                        } else {
                            talker.Play("AIDeny", 4);
                        }
                    }
                }

                eventDelay = baseDelay;
                eventSaid++;
                ResetTimer();
            }
            else if (talker.isPlaying() && eventSaid < 2)
            {
                //Debug.Log("AITalk: 3rd event check");
                eventDelay = baseDelay / 8;
            }
            else if (talker.isPlaying())
            {
                ResetTimer();
            }
        }
	}

	private void ResetTimer() {
        timer = Time.time;
	}
}