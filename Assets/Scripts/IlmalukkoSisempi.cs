using UnityEngine;
using System.Collections;

public class IlmalukkoSisempi : MonoBehaviour
{
    GameObject thedoor;
    
    public float ajastin;
    private bool oviLippu = true;
    private bool oviLippu2 = true;
    private bool oviLippu3 = true;
    bool timerReached = false;
    float timer = 0;

    private IlmaLukkoOviUlompi airlock;
    private GameManager GM;

    IEnumerator wait()
    {

        yield return new WaitForSeconds(1);
        oviLippu2 = true;
    }
    void Timer()
    {
        oviLippu2 = true;
    }

    void Start() {
        airlock = GameObject.Find("IlmalukkoUlompi").transform.GetChild(0).GetComponent<IlmaLukkoOviUlompi>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
  
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player" && !airlock.IsAirlockOpen())
        {
            thedoor = GameObject.FindWithTag("SF_Door");
            thedoor.GetComponent<Animation>().Play("open");
        } else if (other.gameObject.tag == "Player" && airlock.IsAirlockOpen()) {
            GM.SetHintText("External airlock door is still open!");
        }


    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && !airlock.IsAirlockOpen()) {
            thedoor = GameObject.FindWithTag("SF_Door");
            thedoor.GetComponent<Animation>().Play("close");
        }
    }
}



