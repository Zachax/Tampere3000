using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maa : MonoBehaviour {
    Rigidbody rb;
    public int frame;
    public Transform SpawnPoint;
    public Rigidbody Prefab;
    public bool lippu;
    GameManager GM;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("maa2") && lippu)
        {
            StartEarthMissedEvent();
        }

        if (Input.GetButtonDown("maa"))
            {
            if (lippu)
                {
                    print("maa");

                    rb.AddForce(0, 0, -2000);
                lippu = false;

                }
            else
            {
                this.gameObject.SetActive(false);
            }
            }
    }

    public void StartEarthMissedEvent() {
        //Debug.Log("Earth Missed (maa2) event started");
        Rigidbody RigiPre;
        RigiPre = Instantiate(Prefab, SpawnPoint.position, SpawnPoint.rotation) as Rigidbody;
    
        RigiPre.AddForce(0, 0, -4000);
        print("maaOhi lopetus");

        StartCoroutine(Lopetus());
    }

    IEnumerator Lopetus()
    {
        print("maaOhi lopetus method");
        yield return new WaitForSeconds(50);
        print("maaOhi lopetus method 5");
        kuolema();

    }

    private void kuolema()
    {
        GM.SetPlayerHealth(0f);
    }
}

