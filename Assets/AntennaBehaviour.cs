using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntennaBehaviour : MonoBehaviour {

	public GameObject modelOk;
	public GameObject modelBroken;
	bool isBroken = false;

	GameManager GM;

	// Use this for initialization
	void Start () {
		GM = GameObject.Find("GameManager").GetComponent<GameManager>();
		GM.OnStateChange += OnStateChange;
		OnStateChange ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnStateChange() {
		if (GM.IsShipAntennaBroken () != isBroken) {
			isBroken = GM.IsShipAntennaBroken ();
			modelOk.SetActive (!isBroken);
			modelBroken.SetActive (isBroken);
		}
	}

	void OnTriggerEnter(Collider other) {
		if(other.CompareTag("Player")) {
			GM.RepairAntenna ();
		}
	}
}
