using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICanvasHandler : MonoBehaviour {

	private float fadeInTime = 5f;

	public Text healthValueUI;
	public Text staminaValueUI;
	public Text hintTextUI;
    public Text suitOxygenValueUI;
	public Image darkOverlay;
	public GameObject gameOverMenu;
	public GameObject escMenu;

    public GameObject spaceSuitOxygenValue;
    public GameObject spaceSuitOxygenLabel;

	GameManager GM;

	// Use this for initialization
	void Start () {
		GM = GameObject.Find("GameManager").GetComponent<GameManager>();
		GM.OnStateChange += OnStateChange;
	}

	void setStaminaValue(float value) {
		staminaValueUI.text = Mathf.RoundToInt(value*100).ToString() + "%";
	}
		
	void setHealthValue(float value) {
		healthValueUI.text = Mathf.RoundToInt(value*100).ToString() + "%";
	}

    void setSuitOxygenValue(float value) {
        suitOxygenValueUI.text = Mathf.RoundToInt(value*100).ToString() + "%";
    }

	void updateDarkOverlayAlpha(float healthValue) {
		float opacity;
		if (Time.timeSinceLevelLoad < fadeInTime) {
			opacity = 1f - (Time.timeSinceLevelLoad / fadeInTime);
		} else {
			opacity = 1f - healthValue;
		}
		darkOverlay.GetComponent<CanvasRenderer> ().SetAlpha (opacity);
	}

	void updateGameOverMenuVisibility(float healthValue) {
		bool visible = healthValue < 0.0001f;
		if (gameOverMenu.activeSelf != visible) {
			// State has changed
			GM.SetCursorLock (!visible);
		}
		gameOverMenu.SetActive (visible);
	}

	void updateHintText() {
		string text = GM.GetHintText ();
		Carryable objectOnSight = GM.GetObjectOnSight ();
		Carryable objectOnCarry = GM.GetObjectOnCarry ();
		if (objectOnCarry == null && objectOnSight != null) {
			text = "Click to grab " + objectOnSight.name;
		}
		if (hintTextUI.text != text) {
			hintTextUI.text = text;
		}
	}

	void OnStateChange () {
		float staminaValue = GM.GetPlayerStamina ();
		setStaminaValue (staminaValue);

		float healthValue = GM.GetPlayerHealth ();
        setHealthValue (healthValue);

        if (GM.IsSpaceSuitOn()) {
            spaceSuitOxygenValue.SetActive(true);
            spaceSuitOxygenLabel.SetActive(true);
            float suitOxygen = GM.GetSuitOxygen();
            setSuitOxygenValue(suitOxygen);
        } else {
            spaceSuitOxygenValue.SetActive(false);
            spaceSuitOxygenLabel.SetActive(false);
        }

		updateDarkOverlayAlpha (healthValue);
		updateGameOverMenuVisibility (healthValue);

		updateHintText ();
	}

	public void OnRestartButtonClick () {
		GM.RestartGame ();
	}

	public void OnMenuButtonClick () {
		GM.ExitToMenu ();
	}

	public void OnResumeButtonClick () {
		ToggleEscMenu ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			ToggleEscMenu ();
		}
	}

	void ToggleEscMenu () {
		bool visible = !escMenu.activeSelf;
		GM.SetCursorLock (!visible);
		escMenu.SetActive (visible);
	}
}
