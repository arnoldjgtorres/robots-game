using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CyborgMovement : MonoBehaviour {

    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;

    Animator anim;

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        int arraySize = points.Length;
        


        /*raises the walls, not the boss
        foreach(Transform child in points)
        {
            child.position += Vector3.up * 5;
        }
        */

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = true;
        GotoNextPoint();
        enabled = false;
        if (Input.GetKeyDown("space"))
        {
            enabled = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();

       

        anim.SetTrigger("CyberStart");
    }

    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }

}
