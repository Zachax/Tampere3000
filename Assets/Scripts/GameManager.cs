using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public delegate void OnStateChangeHandler();

public class GameManager : MonoBehaviour {

	// Handle GameManager instance & other meta stuff
	public event OnStateChangeHandler OnStateChange;
	//public State gameState = new State();
    public State gameState;
	public bool hasStateChangedSinceLastUpdate = false;
	public void StateChanged(){
		hasStateChangedSinceLastUpdate = true;
	}
	public void NotifyListenersOnUpdateIfNeeded() {
		if(hasStateChangedSinceLastUpdate) {
			if (OnStateChange != null) {
				OnStateChange ();
			}
		}
		hasStateChangedSinceLastUpdate = false;
	}
	private FirstPersonController fpsController;
    private SpaceSuit spaceSuit;
    private ShipInsideDetector voidSensor;
    private ShipDamageManager sDamMan;
    private string textHint;
    public const string statusOk = "Status OK";

    // Can lose normal control
    public bool controlCanBeLost;

    // Meta ends


	//////////////////////////////////////////
	// ACTUAL GAME LOGIC STUFF STARTS HERE: //
	//////////////////////////////////////////

	// Player health

	public void SetPlayerHealth(float health){
		gameState.playerHealth = health;
		StateChanged();
	}

	public void DecreasePlayerHealthBy(float decrement){
		// Decrease but only to 0
		gameState.playerHealth = Mathf.Max(0f, gameState.playerHealth - decrement);
		StateChanged();
	}

	public void IncreasePlayerHealthBy(float increment){
		// Only if the player is alive
		if (gameState.playerHealth > 0.001f) {
			// Increase but only to 1
			gameState.playerHealth = Mathf.Min (1f, gameState.playerHealth + increment);
			StateChanged ();
		}
	}

	public float GetPlayerHealth(){
		return gameState.playerHealth;
	}

    public void SetPlayerControllable(bool control) {
        if (controlCanBeLost) {
            fpsController.setControllable(control);
            StateChanged();
        }
    }


	// Player stamina

	public void SetPlayerStamina(float stamina){
		gameState.playerStamina = stamina;
		StateChanged();
	}

	public void DecreasePlayerStaminaBy(float decrement){
		// Decrease but only to 0
		gameState.playerStamina = Mathf.Max(0f, gameState.playerStamina - decrement);
		StateChanged();
	}

	public void IncreasePlayerStaminaBy(float increment){
		// Increase but only to 1
		gameState.playerStamina = Mathf.Min(1f, gameState.playerStamina + increment);
		StateChanged();
	}

	public float GetPlayerStamina(){
		return gameState.playerStamina;
	}


	// Oxygen

	public void SetOxygenLevel(float level){
		gameState.oxygenLevel = level;
		StateChanged();
	}

	public void DecreaseOxygenLevelBy(float decrement){
		// Decrease but only to 0
		gameState.oxygenLevel = Mathf.Max(0f, gameState.oxygenLevel - decrement);
		StateChanged();
	}

	public void IncreaseOxygenLevelBy(float increment){
		// Increase but only to 1
		gameState.oxygenLevel = Mathf.Min(1f, gameState.oxygenLevel + increment);
		StateChanged();
	}

    public void DecreaseSuitOxygenLevelBy(float decrement){
        // Decrease but only to 0
        gameState.suitOxygen = Mathf.Max(0f, gameState.suitOxygen - decrement);
        StateChanged();
    }

    public float GetSuitOxygen() {
        if (gameState != null) {
            return gameState.suitOxygen;
        } else {
            return 0f;
        }
    }

    public void IncreaseSuitOxygenLevelBy(float increment){
        if (gameState != null) {
            // Decrease but only to 1
            gameState.suitOxygen = Mathf.Max(1f, gameState.suitOxygen + increment);
            StateChanged();
        }
    }

	public bool IsOxygenLevelDangerous() {
        if (gameState != null) {
		    return gameState.oxygenLevel < 0.2f;
        } else {
            return false;
        }
	}

	public bool IsShipOxygenDeviceBroken() {
        if (gameState != null) {
		    return gameState.isShipOxygenDeviceBroken;
        } else {
            return false;
        }
	}

	public void RepairOxygenDevice() {
		if (GetObjectOnCarryName() == "Wrench") {
			gameState.isShipOxygenDeviceBroken = false;
			StateChanged ();
		}
	}

	public bool IsShipAntennaBroken() {
		return gameState.isShipAntennaBroken;
	}

	public void BreakAntenna() {
		gameState.isShipAntennaBroken = true;
		StateChanged ();
	}

	public void RepairAntenna() {
		if (GetObjectOnCarryName() == "Wrench") {
			gameState.isShipAntennaBroken = false;
			StateChanged ();
		}
	}

    public bool IsPlayerInVoid() {
        return gameState.playerInVoid;
    }

    public void SetPlayerOutside(bool outside) {
        gameState.playerInVoid = outside;
        StateChanged();
    }

    // Ship

    public void DecreaseShipIntegrityBy(float decrement) {
        // Decrease but only to 0
        gameState.shipIntegrity = Mathf.Max(0f, gameState.shipIntegrity - decrement);
        StateChanged();
    }

    public float GetShipIntegrity() {
        return gameState.shipIntegrity;
    }

    public void DamageToShip(string unit, float force, string tag) {
        if (unit != null && sDamMan != null) {
            sDamMan.DamageShipPart(unit, force, tag);
            StateChanged();
        }
    }

	// Should the warning beacons be on?
	public bool AreWarningBeaconsOn() {
		// Yes if oxygen level is too low
		return IsOxygenLevelDangerous () || IsPlantOnFire();
	}

    // Returns text to terminal according to required data type.
	public string GetTerminalText(string data) {
        if (data != null) {
            if (data.Equals("status")) {
                string situation = "";
                int issues = 0; 
                if (IsPlantOnFire ()) {
                    situation += "\nFire alarm!";
                    issues++;
                }
        		if (IsOxygenLevelDangerous ()) {
        			situation += "\nLow Oxygen";
                    issues++;
        		}
                if (IsShipOxygenDeviceBroken ()) {
                    situation += "\nOxygen device broken!";
                    issues++;
				}
				if (IsShipAntennaBroken ()) {
					situation += "\nAntenna broken!";
					issues++;
				}
                if (issues > 1) {
                    situation = issues + " Warnings!!!\n" + situation;
                } else if (issues > 0) {
                    situation = "Warning!\n" + situation;
                } else {
                    situation = statusOk;
                }
                return situation;
            } else if (data.Equals("oxygen")) {
    		    return "Oxygen level " + Mathf.Round(gameState.oxygenLevel * 100) + "%";
            } else if (data.Equals("integrity")) {
                return "Ship integrity " + Mathf.Round(gameState.shipIntegrity * 100) + "%";
            } else {
                return "Status: Unknown";
            }
        } else {
            return "";
        }
	}

	// Carrying stuff

	public void SetObjectOnSight(Carryable c) {
		gameState.objectOnSight = c;
		StateChanged ();
	}

	public Carryable GetObjectOnSight() {
		return gameState.objectOnSight;
	}

	public void SetObjectOnCarry(Carryable c) {
		gameState.objectOnCarry = c;

		if (c != null && c.name == "Wrench") {
			gameState.wrenchFound = true;
		}

		StateChanged ();
	}

	public Carryable GetObjectOnCarry() {
		return gameState.objectOnCarry;
	}

	public string GetObjectOnCarryName() {
		string name = "";
		if (gameState.objectOnCarry != null) {
			name = gameState.objectOnCarry.name;
		}
		return name;
	}

    public bool IsSpaceSuitOn() {
        return gameState.spaceSuitOn;
    }

    public void SetSpaceSuitOn(bool wear) {
        gameState.spaceSuitOn = wear;
        StateChanged ();
    }


	// Hint text

	public string GetHintText() {
		return gameState.hintText;
	}

    public void SetHintText(string text) {
        if (text != null) {
            textHint = text;
        }
    }

    public string GetHintTextHigh() {
        return gameState.hintTextHigh;
    }

    public void SetHintTextHigh(string text) {
        if (text != null) {
            gameState.hintTextHigh = text;
        }
    }


	// Events

    public bool IsTutorialDone() {
        return !gameState.tutorialRunning;
    }

	public void EnteredControlRoom() {
		if (!IsShipOxygenDeviceBroken ()) {
			if (!gameState.whaleEventStarted) {
				gameState.whaleEventStarted = true;
				StartCoroutine(DelayedStartFire ());
				StartCoroutine(DelayedBreakAntenna ());
                StartCoroutine(EndTutorial());
				StateChanged ();
			}
		}
	}

	public bool IsWhaleEventStarted() {
		return gameState.whaleEventStarted;
	}

    public bool IsWhaleEventStopped() {
        return gameState.whaleEventStopped;
    }

    public void StopWhaleEvent() {
        gameState.whaleEventStopped = true;
        GameObject.Find("whales").SetActive(false);
        StateChanged ();
    }

	public bool IsPlantOnFire() {
        if (gameState != null) {
    		return gameState.fireStarted && !gameState.fireStopped;
        } else {
            return false;
        }
	}

	public bool IsFireStopped() {
		return gameState.fireStopped;
	}

	public void EnteredPlantFireArea() {
		if (IsPlantOnFire () && GetObjectOnCarryName () == "Fire Extinguisher") {
			gameState.fireStopped = true;
			StateChanged ();
		}
	}

	public void EnteredAirlockArea() {
		gameState.playerNearAirlock = true;
		StateChanged ();
	}

	public void LeftAirlockArea() {
		gameState.playerNearAirlock = false;
		StateChanged ();
	}

	IEnumerator DelayedStartFire()
	{
		yield return new WaitForSeconds (30f);
		gameState.fireStarted = true;
		StateChanged ();
	}

	IEnumerator DelayedBreakAntenna()
	{
		yield return new WaitForSeconds (30f);
		gameState.isShipAntennaBroken = true;
		StateChanged ();
	}

    IEnumerator EndTutorial()
    {
        Debug.Log ("Tutorial ending 1");
        yield return new WaitForSeconds (15f);
        Debug.Log ("Tutorial ended");
        gameState.tutorialRunning = false;
        StateChanged ();
    }

    public void StartEarthMissedEvent() {
        gameState.earthMissedEventStarted = true;
        GameObject.Find("Maa2").GetComponent<Maa>().StartEarthMissedEvent();
        StateChanged ();
    }

    public void StartEarthCrashEvent() {
        gameState.earthCrashEventStarted = true;
        GameObject.Find("Maa3").GetComponent<Maapain>().StartEarthCrashEvent();
        StateChanged ();
    }

    public void StartEarthJumpEvent() {
        gameState.earthJumpEventStarted = true;
        GameObject.Find("maaHyppy").GetComponent<maaHyppy>().StartEarthJumpEvent();
        StateChanged ();
    }

    public bool IsEarthMissedEventStarted() {
        return gameState.earthMissedEventStarted;
    }

    public bool IsEarthCrashEventStarted() {
        return gameState.earthCrashEventStarted;
    }

    public bool IsEarthJumpEventStarted() {
        return gameState.earthJumpEventStarted;
    }

    public bool IsEarthOnSight() {
        if (gameState.earthMissedEventStarted || gameState.earthCrashEventStarted || gameState.earthJumpEventStarted) {
            return true;
        } else {
            return false;
        }
    }

    // Clears hint text after every 5 seconds. Never ends.
    void StartHintTextClearer() {
        StartCoroutine(DelayedHintTextClear());
    }

    IEnumerator DelayedHintTextClear()
    {
        yield return new WaitForSeconds (5f);
        UpdateHint("");
        StateChanged ();
        StartHintTextClearer();
    }

	// Update loop
	void Update()
	{
		// Notify other scripts that have subscribed to state changes:
		NotifyListenersOnUpdateIfNeeded ();

		// Handle other update loop events:
		if (IsShipOxygenDeviceBroken ()) {
			DecreaseOxygenLevelBy (Time.deltaTime * 0.004f);
		} else {
			IncreaseOxygenLevelBy (Time.deltaTime * 0.005f);
		}

		if (IsOxygenLevelDangerous () || IsPlayerInVoid()) {
            if (!IsSpaceSuitOn()) {
			    DecreasePlayerHealthBy (Time.deltaTime * 0.01f);
            }
		} else {
			IncreasePlayerHealthBy (Time.deltaTime * 0.005f);
		}
  
        if (IsSpaceSuitOn()) {
            DecreaseSuitOxygenLevelBy (Time.deltaTime * 0.005f);
            if (GetSuitOxygen() <= 0f) {
                DecreasePlayerHealthBy (Time.deltaTime * 0.01f);
            } else {
                IncreasePlayerHealthBy (Time.deltaTime * 0.005f);
            }
        } else if (IsPlayerInVoid()) {
            DecreasePlayerHealthBy (Time.deltaTime * 0.03f);
        }

        if (IsPlantOnFire()) {
            DecreaseShipIntegrityBy(Time.deltaTime * 0.001f);
        }

        if (textHint == null) {
            textHint = "";
        }

		UpdateHint (textHint);

        if (Input.GetKeyDown(KeyCode.O)) {
            gameState.tutorialRunning = !gameState.tutorialRunning;
            Debug.Log("Tutorial: " + gameState.tutorialRunning);        
        }
	}

	void UpdateHint(string text)
    {
        //string text = "";
        if (gameState.tutorialRunning) {
            if (Time.timeSinceLevelLoad > 5f && !gameState.wrenchFound) {
                text = "Find a wrench in the storage facility.\nStorage is on the bottom of the ship.";
            }
            else if (GetObjectOnCarryName() == "Wrench" && IsShipOxygenDeviceBroken()) {
                text = "Good! Now locate the oxygen generator.\nEngine room is at the back of the ship.";
            }
            else if (gameState.wrenchFound && GetObjectOnCarryName() != "Wrench" && IsShipOxygenDeviceBroken()) {
                text = "Yes, you can drop stuff by clicking again but you're gonna need that wrench.";
            }
            else if (GetObjectOnCarryName() == "Wrench" && !IsShipOxygenDeviceBroken()) {
                text = "Nice job! Click to drop the wrench.";
            }
            else if (GetObjectOnCarryName() != "Wrench" && !IsShipOxygenDeviceBroken() && !IsWhaleEventStarted()) {
                text = "Great! Go check the oxygen status in the cockpit control room. That is on the front of the ship.";
            } else if (IsWhaleEventStarted()) {
                text = "Awesome! Now you're good to go by yourself.";
            }
        } else {
			if (gameState.playerNearAirlock) {
				text = "Press E to open/close airlock.";
			} else if (IsSpaceSuitOn() && spaceSuit.IsSpaceSuitReadyToWear()) {
				text = "Click to take off space suit.";
			} else if (!IsSpaceSuitOn() && spaceSuit.IsSpaceSuitReadyToWear()) {
				text = "Click to wear space suit.";
			} else if (GetObjectOnCarryName() != null && !GetObjectOnCarryName().Equals("")) {
                text = "Click to drop " + GetObjectOnCarryName() + ".";
            } else if (GetObjectOnSight() != null) {
                text = "Click to pick up " + GetObjectOnSight().name + ".";
            } 
        }


		if (gameState.hintText != text) {
			gameState.hintText = text;
			StateChanged ();
		}
	}

	void Start() {
		fpsController = GameObject.Find ("FPSController").GetComponent<FirstPersonController> ();
        spaceSuit = GameObject.Find("IlmaOsa").GetComponent<SpaceSuit>();
        voidSensor = GameObject.Find("ShipInsides").GetComponent<ShipInsideDetector>();
        sDamMan = GameObject.Find("Ship Damage Manager").GetComponent<ShipDamageManager>();
        // This would clear up hint text as backup every now and then, 
        // but it should not be necessary, if all works otherwise:
        //StartHintTextClearer();
	}

    void Awake() {
        gameState = this.GetComponent<State>();
        StateChanged ();
	}

	public void RestartGame() {
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}

	public void ExitToMenu() {
		SceneManager.LoadScene("Menu");
	}

	public void SetCursorLock (bool locked) {
		fpsController.setCursorLock (locked);
	}

}