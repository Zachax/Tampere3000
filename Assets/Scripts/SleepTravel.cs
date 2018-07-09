using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepTravel : MonoBehaviour {

    private GameManager GM;
    private bool slept;

    // Use this for initialization
    void Start () {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        slept = false;
	}

    public void GoSleep() {
        if (!slept) {
            slept = true;
            float condition = GM.GetShipIntegrity();
            float random = (float) new System.Random().NextDouble();
            float result = condition + random;
            //Debug.Log("cond:" + condition + " rand:" + random);
            Debug.Log("result:" + result);
            GM.StopWhaleEvent();

            if (result > 1.6f) {
                Debug.Log("Earth Station Jump event starting, hopefully.");
                GM.StartEarthJumpEvent();
            } else if (result > 1.2f) {
                Debug.Log("Earth Crash event starting, hopefully.");
                GM.StartEarthCrashEvent();
            } else {
                Debug.Log("Earth Missed event starting, hopefully.");
                GM.StartEarthMissedEvent();
            }
        }
    }
}