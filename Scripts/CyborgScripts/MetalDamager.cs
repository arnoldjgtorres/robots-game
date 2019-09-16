using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalDamager : MonoBehaviour {

    private float timerChange1=0f; //Timer to time color change
    private float timerStayChanged1=0f;//determines how long will be changed to be able to damage cyborg (yellow)
    private int convertStayChanged1=0; //simply converts from float to int

    private float timerChange2 = 0f;
    private float timerStayChanged2 = 0f;
    private int convertStayChanged2 = 0;

    private float timerChange3 = 0f;
    private float timerStayChanged3 = 0f;
    private int convertStayChanged3 = 0;

    private float lightningTimer = 0f;
    public bool lightningFlag = false;//will be taken by healthcyborg to lower health


    private Color baseColor; //the color to change back to 


    private bool frontChanged; //helper flags  
    private bool leftChanged;
    private bool rightChanged;

    private Renderer rendFront; //renderers to grab color from object ahd change color back
    private Renderer rendLeft;
    private Renderer rendRight;


    private GameObject cubeFront = null; //electric walls in the editor
    private GameObject cubeLeft = null;
    private GameObject cubeRight = null;

    private GameObject lightningFront = null; //lightning to hit boss 
    private GameObject lightningRight = null;
    private GameObject lightningLeft = null;
    private GameObject lightningPoleL = null;
    private GameObject lightningPoleR = null;

    private bool leftWall = false;//booleans from healthcyborg to determine collision
    private bool rightWall = false;//between cyborg and the invisible collider 
    private bool frontWall = false;//that are on top  of the electric walls

    private GameObject cyborg = null;

    public HealthCyborg otherScript;
    
    private void Awake()
    {
       

        cubeFront = GameObject.FindGameObjectWithTag("FrontWall");
        cubeLeft = GameObject.FindGameObjectWithTag("LeftWall");
        cubeRight = GameObject.FindGameObjectWithTag("RightWall");

        lightningFront = GameObject.Find("Lightning Front");
        lightningRight = GameObject.Find("Lightning Right");
        lightningLeft = GameObject.Find("Lightning Left");
        lightningPoleL = GameObject.Find("Lightning RPole");
        lightningPoleR = GameObject.Find("Lightning LPole");
    }

    void Start () {

        lightningFront.SetActive(false);
        lightningRight.SetActive(false);
        lightningLeft.SetActive(false);
        lightningPoleL.SetActive(false);
        lightningPoleR.SetActive(false);


        //Debug.Log(cubeRight.name);
        //Debug.Log(cubeFront.name);

        rendFront = cubeFront.GetComponent<Renderer>();
        rendLeft = cubeLeft.GetComponent<Renderer>();
        rendRight = cubeRight.GetComponent<Renderer>();

        baseColor = rendFront.material.GetColor("_Color");
 
        frontChanged = false;
        leftChanged = false;
        rightChanged = false;
    }
	
	// Update is called once per frame
	void Update () {
        //leftWall = GameObject.Find("CyberSoldier").GetComponent<HealthCyborg>().leftBox;
        //rightBox = GameObject.Find("CyberSoldier").GetComponent<HealthCyborg>().rightBox;
        //frontBox = GameObject.Find("CyberSoldier").GetComponent<HealthCyborg>().frontBox;

        FrontBox();
        LeftBox();
        RightBox();

        CheckLightning();

        if(lightningFlag == true)
        {
            Debug.Log("HIT THAT BOSS MAN");
        }
        
    }

    private void LateUpdate()
    {
        
    }

    private void OnCollisionEnter(Collision collider)
    {
        if(collider.gameObject.tag == "Bullet")
        {
            
            if(name == "Electric Wall")
            {
                if(frontChanged == true)
                {
                  
                    Electricity();
                }
            }
            if (name == "Electric Wall (1)")
            {
                if(leftChanged == true)
                {
                   
                    Electricity();
                }

            }
            if (name == "Electric Wall (2)")
            {
                if(rightChanged == true)
                {
                   
                    Electricity();
                }

            }
        }
    }




    //In these functions, CyborgHealth will grab boolean 'frontChanged' leftChanged'
    //or rightChanged'  and that will determine whether it is damaged
    private void FrontBox()
    {   
        timerChange1 += Time.deltaTime;

        //need to convert time because always has many decimal digits in Debug.Log
        int convertTime = (int)timerChange1 % 60;

        if (convertTime == 5 && frontChanged != true)
        {
            frontChanged = true;
            rendFront.material.SetColor("_Color", Color.yellow);

        }
        if (frontChanged == true)
        {
            timerStayChanged1 += Time.deltaTime;
            convertStayChanged1 = (int)timerStayChanged1 % 60;
        }
        if (convertStayChanged1 == 5)
        {     
            frontChanged = false;
            timerChange1 = 0f;
            timerStayChanged1 = 0f;
            convertStayChanged1 = 0;
            convertTime = 0;
            rendFront.material.SetColor("_Color", baseColor);
        }
    }


    private void LeftBox()
    {
        timerChange2 += Time.deltaTime;

        //need to convert the time because always has many decimal digits
        int convertTime = (int)timerChange2 % 60;

        if (convertTime == 9 && leftChanged != true)
        {
            leftChanged = true;
           
            //Debug.Log(convertTime);
            rendLeft.material.SetColor("_Color", Color.yellow);
        }
        if (leftChanged == true)
        {
            timerStayChanged2 += Time.deltaTime;
            convertStayChanged2 = (int)timerStayChanged2 % 60;
            //Debug.Log(convertStayChanged);
        }
        if (convertStayChanged1 == 5)
        {
            leftChanged = false;
            timerChange2 = 0f;
            timerStayChanged2 = 0f;
            convertStayChanged2 = 0;
            convertTime = 0;
            rendLeft.material.SetColor("_Color", baseColor);
        }
    }

    private void RightBox()
    {
        timerChange3 += Time.deltaTime;

        //need to convert the time because always has many decimal digits
        int convertTime = (int)timerChange3 % 60;

        if (convertTime == 3 && rightChanged != true)
        {
            rightChanged = true;
           
            //Debug.Log(convertTime);
            rendRight.material.SetColor("_Color", Color.yellow);
        }
        if (rightChanged == true)
        {
            timerStayChanged3 += Time.deltaTime;
            convertStayChanged3 = (int)timerStayChanged3 % 60;
            //Debug.Log(convertStayChanged);
        }
        if (convertStayChanged3 == 5)
        {
          
            rightChanged = false;
            timerChange3 = 0f;
            timerStayChanged3 = 0f;
            convertStayChanged3 = 0;
            convertTime = 0;
            rendRight.material.SetColor("_Color", baseColor);
        }
    }
    
    //checks if the appropriate box that is 'yellow' is stood on by the cyborg. 
    //if is then elecricity runs
    private void Electricity()
    {
        leftWall = otherScript.leftBox;
        rightWall = otherScript.rightBox;
        frontWall = otherScript.frontBox;

        if(leftWall == true)
        {
            SetLightningTrue();
            lightningFlag = true;
        }

        if (rightWall == true)
        {
            SetLightningTrue();
            lightningFlag = true;
        }

        if (frontWall == true)
        {
            SetLightningTrue();
            lightningFlag = true;
        }


       
    }


    //does a timer of one second and then turns lightning off
    private void CheckLightning()
    {
        if(lightningFlag == true)
        {
            Debug.Log("HIT CYBORG");
            lightningTimer += Time.deltaTime;
        }

        if(lightningTimer >= 1f)
        {
            lightningTimer = 0;
            lightningFlag = false;
            lightningRight.SetActive(false);
            lightningLeft.SetActive(false);
            lightningFront.SetActive(false);
        }
        
    }

    private void SetLightningTrue()
    {

        lightningLeft.SetActive(true);
        lightningRight.SetActive(true);
        lightningFront.SetActive(true);
        lightningPoleR.SetActive(true);
        lightningPoleL.SetActive(true);
    }
    
}
