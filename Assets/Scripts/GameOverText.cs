using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverText : MonoBehaviour {

    private GameManager GM;

    public Text endReport;
    public string basicDeath = "You are dead.";

	// Use this for initialization
	void Start () {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        GM.OnStateChange += OnStateChange;
	}
	
    void OnStateChange() {
        if (GM.GetShipIntegrity() <= 0) {
            endReport.text = "Your ship was destroyed.\n" + basicDeath;
        } else if (GM.IsEarthCrashEventStarted()) {
            endReport.text = "Your space ship has burned on Earth atmosphere.\n" + basicDeath;
        } else if (GM.IsEarthMissedEventStarted()) {
            endReport.text = "You have missed Earth and you are now heading far far away.\nAI is happy you stayed.";
        } else if (GM.IsEarthJumpEventStarted()) {
            endReport.text = "You tried to reach the Earth orbiting space station.\nYou failed.\n" + basicDeath;
        } else {
            endReport.text = basicDeath;
        }
    }
}
