using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProwlerScript : MonoBehaviour
{

    //Note: if variable is public, can change in Unity Editor
    public float lookDistance;
    public float targetDistance;
    //public float attackDistance;
    public float enemyMovementSpeed;
    public float damping;
    public Transform fpsTarget;
    Rigidbody enemyRB;
    Renderer myRender;

    private float timeAcc = 0f;

    private const int ENEMY_PATROL = 0;
    private const int ENEMY_TRACK = 1;
    private const int ENEMY_BRACK = 2;
    private const int ENEMY_DIE = 3;

    private int currentStatus;


    private Animation anim;
    public ProwlerCollision prowlerHealth;

    // Use this for initialization
    void Start()
    {
        myRender = GetComponent<Renderer>();
        enemyRB = GetComponent<Rigidbody>();
        prowlerHealth.GetComponent<ProwlerCollision>();
        anim = GetComponent<Animation>();
        anim.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Distance from the player = targetDistance
        targetDistance = Vector3.Distance(fpsTarget.position, transform.position);
      

        if (targetDistance < lookDistance)
        {
            anim.enabled = true;
            myRender.material.color = Color.red;
            LookAtPlayer();
            AttackPlayer();
          
        }
        else
        {
            anim.enabled = false;
        }
         


    }

    void LookAtPlayer()
    {
        Quaternion rotation = Quaternion.LookRotation(fpsTarget.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
    }

    void AttackPlayer()
    {
        Debug.Log(prowlerHealth.currentShield);
        if(prowlerHealth.currentShield == 0)
        {
            anim.enabled = false;
        }
        else
        {
            anim.enabled = true;
           enemyRB.AddForce(transform.forward * enemyMovementSpeed);
        }
        
    }

}




