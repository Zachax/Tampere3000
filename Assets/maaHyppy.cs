using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maaHyppy : MonoBehaviour {
    Rigidbody rb;
    public Transform SpawnPoint;
    public Transform SpawnPoint2;
    
    public Rigidbody Prefab;
    public Rigidbody Prefab2;

    public bool lippu;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("maaHyppy") && lippu)
        {
            StartEarthJumpEvent();
        }
        
    }

    public void StartEarthJumpEvent() {
        Debug.Log("Start Earth Jump event (maahyppy) started");
        Rigidbody RigiPre;
        Rigidbody RigiPre2;
        Rigidbody RigiPre3;
        RigiPre = Instantiate(Prefab, SpawnPoint.position, SpawnPoint.rotation) as Rigidbody;
        RigiPre2 = Instantiate(Prefab2, SpawnPoint2.position, SpawnPoint2.rotation) as Rigidbody;
       

        RigiPre.AddForce(0, 0, -4000);
        RigiPre2.AddForce(0, 0, -4000);
    }
}
