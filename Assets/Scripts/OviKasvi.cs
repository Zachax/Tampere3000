﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OviKasvi : MonoBehaviour {

    GameObject thedoor;

    public float ajastin;

    private bool oviLippu = true;
    private bool oviLippu2 = true;
    private bool oviLippu3 = true;
    bool timerReached = false;
    float timer = 0;
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

        if (other.gameObject.tag == "Player")
        {
            thedoor = GameObject.FindWithTag("okasvi");
            thedoor.GetComponent<Animation>().Play("open");



        }


    }
    void OnTriggerExit(Collider other)
    {
        thedoor = GameObject.FindWithTag("okasvi");



        thedoor.GetComponent<Animation>().Play("close");
    }
}
