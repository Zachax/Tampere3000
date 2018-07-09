using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggeriMaa3 : MonoBehaviour {
    GameManager GM;
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GM.SetPlayerHealth(0f);
        }
            
    }
}
