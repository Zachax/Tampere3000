using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleMenu2 : MonoBehaviour {
    Rigidbody rb;
    public Transform SpawnPoint;
    public Rigidbody Prefab;
    public bool lippu = true;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Lopetus();
        print("valas");
    }

    // Update is called once per frame
    void Update()
    {
        if (lippu)
        {
            WhaleSpawn();
            lippu = false;

        }
    }

    public void WhaleSpawn()
    {
        print("valasSpawn");
        Rigidbody RigiPre;
        RigiPre = Instantiate(Prefab, SpawnPoint.position, SpawnPoint.rotation) as Rigidbody;
        RigiPre.AddForce(-60000, 0, 0);
        StartCoroutine(Lopetus());
    }
    IEnumerator Lopetus()
    {
        print("valas Lopetus");
        print("maaOhi lopetus method");
        yield return new WaitForSeconds(1);
        print("maaOhi lopetus method 5");
        lippu = true;

    }
}
