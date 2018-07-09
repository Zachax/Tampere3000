using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerminalCanvasHandler : MonoBehaviour {

	GameManager GM;
	Text textComponent;

    // Possible data types requireable for screen:
    // status, oxygen, integrity
    public string dataReq;

	// Use this for initialization
	void Start () {
		GM = GameObject.Find("GameManager").GetComponent<GameManager>();
		this.textComponent = this.GetComponentInChildren<Text> ();
		GM.OnStateChange += OnStateChange;
	}

	void setTextContent(string content) {
		textComponent.text = content;
	}

	void OnStateChange () {
		setTextContent(GM.GetTerminalText (dataReq));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
