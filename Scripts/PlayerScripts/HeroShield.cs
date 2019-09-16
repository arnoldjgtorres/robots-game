using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
//Only used to enable and disable shield renderer, does not do hit detection
//
public class HeroShield: MonoBehaviour {
    public Renderer rend;
    public PlayerHealth healthScript;


	void Start () {
        rend = GetComponent<Renderer>();
        rend.enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
        CheckShield();
	}

    private void CheckShield()
    {

        if(healthScript.currentShield > 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                rend.enabled = true;

            }
            else
            {
                rend.enabled = false;

            }
        }

        else if(healthScript.currentShield <= 0)
        {
            rend.enabled = false;
        }

        
    }
}
