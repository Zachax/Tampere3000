// Data structure to maintain game state

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class State : MonoBehaviour {
	// Player
	public float playerHealth = 1f; // float 0-1
	public float playerStamina = 1f; // float 0-1
	public bool playerInVoid = false;
	public bool playerNearAirlock = false;

	// Ship
	public float oxygenLevel = 0.4f; // float 0-1
    public float shipIntegrity = 1f; // float 0-1
    public string[] shipModules; // module names
    public string[] shipComponents; // component names
    public float[] shipModuleIntegrity; // float 0-1
    public float[] shipComponentIntegrity; // float 0-1
	public bool isShipOxygenDeviceBroken = true;
	public bool isShipAntennaBroken = false;   

	// Carrying stuff
	public Carryable objectOnSight = null;
    public Carryable objectOnCarry = null;
    public bool spaceSuitOn = false;
    public float suitOxygen = 1f; // float 0-1

	// Story events
    public bool tutorialRunning = true;
	public bool wrenchFound = false;
	public bool fireStarted = false;
	public bool fireStopped = false;
	public bool whaleEventStarted = false;
    public bool whaleEventStopped = false;
    public bool earthMissedEventStarted = false;
    public bool earthCrashEventStarted = false;
    public bool earthJumpEventStarted = false;

	// Hints
	public string hintText = "";
    public string hintTextHigh = "";
}
