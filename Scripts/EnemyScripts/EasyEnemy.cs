using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EasyEnemy : MonoBehaviour
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

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    
    private float timeBetweenBullets = 0.5f;
    private float timeAcc = 0f;

    private const int ENEMY_PATROL = 0;
    private const int ENEMY_TRACK = 1;
    private const int ENEMY_BRACK = 2;
    private const int ENEMY_DIE = 3;

    private int currentStatus;




    // Use this for initialization
    void Start()
    {
        myRender = GetComponent<Renderer>();
        enemyRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Distance from the player = targetDistance
        targetDistance = Vector3.Distance(fpsTarget.position, transform.position);
    
        if (targetDistance < lookDistance)
        {
            myRender.material.color = Color.red;
            LookAtPlayer();
            AttackPlayer();
        }
        

    }

    private void OnCollisonEnter(Collision collision)
    {
        Debug.Log("HIT");

    }

    void LookAtPlayer()
    {
        Quaternion rotation = Quaternion.LookRotation(fpsTarget.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
    }

    void AttackPlayer()
    {
        enemyRB.AddForce(transform.forward * enemyMovementSpeed);
        Fire();
    }

    void Fire()
    {
        //Create the bullet from the Bullet Prefab

        if (Time.time - timeAcc > timeBetweenBullets)
        {
            var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

            //Add velocity to the bullet
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 20;
            timeAcc = Time.time;
            Destroy(bullet, 2.0f);
        }


    }







}




