using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAnimScript : MonoBehaviour {

    Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.Space) || Input.GetKey("up") || Input.GetKey("down")
            || Input.GetKey("left") || Input.GetKey("right"))
        {
            anim.SetTrigger("CharRun");


        }
            

	}
}
