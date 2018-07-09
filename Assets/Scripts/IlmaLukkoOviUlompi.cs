using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IlmaLukkoOviUlompi : MonoBehaviour
{

    GameObject thedoor;

    public float ajastin;
    private bool auki;

    private bool oviLippu = true;
    private bool oviLippu2 = true;
    private bool oviLippu3 = true;
    bool timerReached = false;
    float timer = 0;

    private GameManager GM;

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
    void Update()
    {
        if (Input.GetButtonDown("doorOpen") && !oviLippu3)
        {
            if (oviLippu & oviLippu2)
            {
                thedoor = GameObject.FindWithTag("ilu");
                thedoor.GetComponent<Animation>().Play("open");
                oviLippu = false;
                oviLippu2 = false;
                Invoke("Timer", 3);
                auki = true;



            }
            else if (oviLippu2)
            {
                thedoor = GameObject.FindWithTag("ilu");
                thedoor.GetComponent<Animation>().Play("close");
                oviLippu = true;
                oviLippu2 = false;
                Invoke("Timer", 3);
                auki = false;
                GM.DecreaseOxygenLevelBy(0.05f);

            }

        }
    }

    public bool IsAirlockOpen() {
        return auki;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            oviLippu3 = false;
			GM.EnteredAirlockArea ();

        }


    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            oviLippu3 = true;
			GM.LeftAirlockArea ();
        }
    }
}
