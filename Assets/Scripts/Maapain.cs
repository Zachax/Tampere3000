using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maapain : MonoBehaviour {
    public Transform SpawnPoint;
    public Rigidbody Prefab;
    public bool lippu;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("maa3") && lippu)
        {
            StartEarthCrashEvent();
        }
    }

    public void StartEarthCrashEvent() {
        Debug.Log("maa3 event started");
        Rigidbody RigiPre;
        RigiPre = Instantiate(Prefab, SpawnPoint.position, SpawnPoint.rotation) as Rigidbody;

        RigiPre.AddForce(0, 0, -10000);
    }
}
