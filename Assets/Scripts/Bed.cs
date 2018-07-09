using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour {

    private GameManager GM;
    private UICanvasHandler UI;
    private SleepTravel ST;
    private bool canSleep;

	// Use this for initialization
	void Start () {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        UI = GameObject.Find("UICanvas").GetComponent<UICanvasHandler>();
        ST = GameObject.Find("SleepTravelToEnd").GetComponent<SleepTravel>();
        canSleep = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!GM.IsSpaceSuitOn() && GM.IsTutorialDone() && GM.GetObjectOnCarry() == null) {
                canSleep = true;
                GM.SetHintText("Press Enter to sleep...");
            }
        }
    }

    void OnTriggerStay(Collider other) {
        if (Input.GetKeyDown(KeyCode.Return)) {
            if (canSleep) {
                if (GM.GetTerminalText("status").Equals(GameManager.statusOk)) {
                    //Debug.Log("Now could sleep");
                    ST.GoSleep();
                    GM.SetHintText("You took some rest.");
                } else {
                    GM.SetHintText("Can't sleep now!\nShip status is not OK.");
                }
            } else {

            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GM.SetHintText("");
            canSleep = false;
        }
    }
}
