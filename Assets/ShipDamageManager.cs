using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ShipDamageManager : MonoBehaviour {

    private StringBuilder damageReport;
    private GameManager GM;
    public State gameState;
    // The higher the value, the more impact damage gets reduced.
    // Recommended to have maybe +100f.
    public float divider;
    public float minIntegrity;

	// Use this for initialization
	void Start () {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameState = gameObject.transform.parent.gameObject.GetComponent<State>();
        GM.OnStateChange += OnStateChange;
        damageReport = new StringBuilder();

        // Initialize ship modules and components into arrays
        GameObject[] mods = GameObject.FindGameObjectsWithTag("Module");
        gameState.shipModules = new string[mods.Length];
        gameState.shipModuleIntegrity = new float[mods.Length];
        for(int i = 0; i < mods.Length; ++i) {
            gameState.shipModules[i] = mods[i].name;
            gameState.shipModuleIntegrity[i] = 1f;
        }

        GameObject[] comps = GameObject.FindGameObjectsWithTag("ModuleComponent");
        gameState.shipComponents = new string[comps.Length];
        gameState.shipComponentIntegrity = new float[comps.Length];
        for(int i = 0; i < comps.Length; ++i) {
            //Debug.Log("Found!");
            gameState.shipComponents[i] = comps[i].name;
            gameState.shipComponentIntegrity[i] = 1f;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Parses new damagereport to be written
    void OnStateChange () {
        damageReport.Clear();
        for(int i = -1; i < gameState.shipModules.Length; ++i) {
            if (i < 0) {
                damageReport.Append("Modules:\n");
            } else {
                damageReport.Append(gameState.shipModules[i] + " " + 
                    Mathf.RoundToInt(gameState.shipModuleIntegrity[i]*100) + "%\n");
            }
        }

        float averageIntegrity = 0f;
        foreach(float val in gameState.shipModuleIntegrity) {
            averageIntegrity += val;
        }
        averageIntegrity /= gameState.shipModuleIntegrity.Length;
        gameState.shipIntegrity = Mathf.Min(gameState.shipIntegrity, averageIntegrity);
        //Debug.Log("Ave int: " + averageIntegrity);

        for(int i = -1; i < gameState.shipComponents.Length; ++i) {
            if (i < 0) {
                damageReport.Append("\nComponents:\n");
            } else {
                damageReport.Append(gameState.shipComponents[i] + " " + 
                    Mathf.RoundToInt(gameState.shipComponentIntegrity[i]*100) + "%\n");
            }
        }
        gameState.hintTextHigh = damageReport.ToString();
    }

    // Calculates and writes the new integrity after damage gotten
    public void DamageShipPart(string partName, float force, string tag) {
        if (tag.Equals("Module")) {
            for(int idx = 0; idx < gameState.shipModules.Length; ++idx) {
                if (gameState.shipModules[idx].Equals(partName)) {
                    float damage = gameState.shipModuleIntegrity[idx] - force / divider;
                    if (damage < 0) {
                        damage /= 2;
                    }
                    gameState.shipModuleIntegrity[idx] = Mathf.Max(minIntegrity, damage);
                    break;
                }
            }
        } else if (tag.Equals("ModuleComponent")) {
            for(int idx = 0; idx < gameState.shipComponents.Length; ++idx) {
                if (gameState.shipComponents[idx].Equals(partName)) {
                    float damage = gameState.shipComponentIntegrity[idx] - force / divider;
                    if (damage < 0) {
                        damage /= 2;
                    }
                    gameState.shipComponentIntegrity[idx] = Mathf.Max(minIntegrity, damage);
                    break;
                }
            }
        }
    }
}
