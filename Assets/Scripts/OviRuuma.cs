using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OviRuuma : MonoBehaviour {
    GameObject thedoor;
    private GameManager GM;

    public float ajastin;
    public bool broken;

    private bool open;

    private bool oviLippu = true;
    private bool oviLippu2 = true;
    private bool oviLippu3 = true;
    bool timerReached = false;
    float timer = 0;

    void Start() {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    IEnumerator wait()
    {

        yield return new WaitForSeconds(1);
        oviLippu2 = true;
    }
    void Timer()
    {
        oviLippu2 = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!broken) {
            thedoor = GameObject.FindWithTag("oruuma");
            thedoor.GetComponent<Animation>().Play("open");
            open = true;
            if (!GM.IsTutorialDone()) {
                broken = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && !broken)
        {
            thedoor = GameObject.FindWithTag("oruuma");
            thedoor.GetComponent<Animation>().Play("close");
            open = false;
        }
    }
}
