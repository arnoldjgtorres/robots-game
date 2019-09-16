using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntered : MonoBehaviour {


    public CyborgMovement cm;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("COLLISION DETECTED");
        if(other.gameObject.name == "HeroOrangeHelmet")
        {
            cm.enabled = true;
        }
    }
}
