using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCyborg : MonoBehaviour
{

    public float currentHealth = 100;
    private float initialHealth = 100;
    public float currentShield = 100;
    private float initialShield = 100;

    public Image healthBar;
    public float sinkSpeed = 2.5f;
    private BoxCollider boxCollider;

    public float regenShield = 5.0f;

    private bool isDead = false;
    private bool isDisabled = false;
    private bool isSinking;

    public bool leftBox { get; private set; } // flags taken by metaldamager to determine elec
    public bool frontBox { get; private set; }
    public bool rightBox { get; private set; }

    public GameObject frontSpot;
    public GameObject leftSpot;
    public GameObject rightSpot;

    public MetalDamager metalScript;

    // Use this for initialization
    void Start()
    {
       
        leftBox = false;
        frontBox = false;
        rightBox = false;
        currentHealth = initialHealth;
        currentShield = initialShield;
        boxCollider = GetComponent<BoxCollider>();

        frontSpot = GameObject.FindGameObjectWithTag("FrontTarget");
        leftSpot = GameObject.FindGameObjectWithTag("RightTarget");
        rightSpot = GameObject.FindGameObjectWithTag("LeftTarget");
    }

    // Update is called once per frame
    void Update()
    {

        CheckDead();
        DoDamage();
    }

    void OnTriggerEnter(Collider collision)
    {
        //RGBA(1.000, 0.922, 0.016, 1.000)

        if (collision.gameObject.name == "Target Front")
        {
            GameObject box = GameObject.FindWithTag("FrontWall");           
            Renderer rend = box.gameObject.GetComponent<Renderer>();          
            string str = rend.material.GetColor("_Color").ToString();

            if(str == "RGBA(1.000, 0.922, 0.016, 1.000)")
            {
                frontBox = true;
             
            }
        }

        if(collision.gameObject.name == "Target Right")
        {
            GameObject box = GameObject.FindWithTag("RightWall");
            Renderer rend = box.gameObject.GetComponent<Renderer>();
            string str = rend.material.GetColor("_Color").ToString();

            if (str == "RGBA(1.000, 0.922, 0.016, 1.000)")
            {
                rightBox = true;
              
            }
        }

        if(collision.gameObject.name == "Target Left")
        {
            GameObject box = GameObject.FindWithTag("LeftWall");
            Renderer rend = box.gameObject.GetComponent<Renderer>();
            string str = rend.material.GetColor("_Color").ToString();

            if (str == "RGBA(1.000, 0.922, 0.016, 1.000)")
            {
                leftBox = true;
               
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.name == "Target Front")
        {
            frontBox = false;
        }

        if (collision.gameObject.name == "Target Right")
        {
            rightBox = false;
        }

        if (collision.gameObject.name == "Target Left")
        {
            leftBox = false;
        }

    }
    

    private void DoDamage()
    {
        if(metalScript.lightningFlag == true)
        {
            currentHealth -= 10;
        }
    }

    private void CheckDead()
    {
        if(currentHealth <= 0)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }
}
