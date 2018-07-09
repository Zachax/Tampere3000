using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantFireController : MonoBehaviour {

	GameManager GM;
	GameObject fire;
	bool isBurning = false;

	// Use this for initialization
	void Start () {
		GM = GameObject.Find("GameManager").GetComponent<GameManager>();
		fire = GameObject.Find ("PlantFire");
		isBurning = GM.IsPlantOnFire ();
		fire.SetActive (isBurning);
	}
	
	// Update is called once per frame
	void Update () {
		if (isBurning != GM.IsPlantOnFire ()) {
			// changed
			isBurning = GM.IsPlantOnFire ();
			fire.SetActive (isBurning);
		}
	}
}
