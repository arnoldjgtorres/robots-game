using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCollision : MonoBehaviour {
    private float currentHealth = 100;
    public float initialHealth = 100;
    public Image healthBar;
    public float sinkSpeed = 2.5f;
    BoxCollider boxCollider;
    CapsuleCollider capsuleCollider;
  
    bool isDead = false;
    bool isSinking;


    // Use this for initialization
    void Start () {
        currentHealth = initialHealth;
        boxCollider = GetComponent<BoxCollider>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        
	}
	
	// Update is called once per frame
	void Update () {

        if (isDead)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
       
        //UpdateUI();
    }
    void UpdateUI()
    {

        healthBar.fillAmount = currentHealth / initialHealth;
    }
    private void TakeDamage(string bullet)
    {
        //based on string, can change how much damage the enemy takes
        //based on which type of bullet i.e. stronger weapon 
        //will have different bullets

        if (bullet == "Bullet")
        {
            currentHealth -= 10;
            //Debug.Log(currentHealth);
        }

       
        if(currentHealth <= 0)
        {
            isDead = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        string bulletType = collision.collider.tag;
      
        if(collision.gameObject.tag== "Bullet")
        {
        }
        
        TakeDamage(bulletType);
        
       
    }
}
