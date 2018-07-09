using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantFireArea : MonoBehaviour {

	GameManager GM;
	public GameObject[] togglableChildren;

	// Use this for initialization
	void Start () {
		GM = GameObject.Find("GameManager").GetComponent<GameManager>();
	}


	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other) {
		if(other.CompareTag("Player")) {
			GM.EnteredPlantFireArea ();
		}
	}
}
