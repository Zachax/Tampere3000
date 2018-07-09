using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class graf : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        Physics.gravity = new Vector3(0, 0, 0);
        GetComponent<Rigidbody>().drag = .5f;
    }


    
}
