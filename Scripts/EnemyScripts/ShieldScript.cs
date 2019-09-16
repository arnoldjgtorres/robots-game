using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour {
    private float currentHealth = 100;
    public float initialHealth = 100;

    bool isDisabled = false;
    float timer = 0;
   

    CapsuleCollider capsuleCollider;
    // Use this for initialization
    void Start () {
        capsuleCollider = GetComponent<CapsuleCollider>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void TakeDamage(string bullet)
    {
        //based on string, can change how much damage the enemy takes
        //based on which type of bullet i.e. stronger weapon 
        //will have different bullets
        if(timer != 0)
        {
            return;
        }

        if (bullet == "Bullet")
        {
            currentHealth -= 10;
           
        }


        if (currentHealth <= 0)
        {
            isDisabled = true;
            capsuleCollider.enabled = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        string bulletType = collision.collider.tag;

        if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("HIT SHIELD");
        }
       
        TakeDamage(bulletType);


    }
}
