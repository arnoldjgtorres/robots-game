using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProwlerCollision : MonoBehaviour
{
    public float currentHealth = 100;
    private float initialHealth = 100;
    public float currentShield = 100;
    private float initialShield = 100;

    public Image healthBar;
    public float sinkSpeed = 2.5f;
    BoxCollider boxCollider;
    CapsuleCollider capsuleCollider;

    bool isDead = false;
    bool isDisabled = false;
    bool isSinking;

    public float timer = 8.0f;
    Transform shield;
    Renderer rend;

    // Use this for initialization
    void Start()
    {
        currentHealth = initialHealth;
        currentShield = initialShield;
        boxCollider = GetComponent<BoxCollider>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        boxCollider.enabled = false;

        shield = transform.Find("Capsule");
        rend = shield.GetComponent<Renderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDisabled)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            timer = 8.0f;
            isDisabled = false;
            currentShield = initialShield;
            capsuleCollider.enabled = true;
            boxCollider.enabled = false;
            rend.enabled = true;
        }


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
   
    private void OnCollisionEnter(Collision collision)
    {
        string bulletType = collision.collider.tag;
  
        if (collision.gameObject.tag == "Bullet")
        {
        }

        if (capsuleCollider.enabled == false)
        {
            TakeDamage(bulletType);
        }

        if (capsuleCollider.enabled == true)
        {
           
            ShieldTakeDamage(bulletType);
        }

      
    }

    private void TakeDamage(string bullet)
    {
        //based on string, can change how much damage the enemy takes
        //based on which type of bullet i.e. stronger weapon 
        //will have different bullets

        if (bullet == "Bullet")
        {
            currentHealth -= 1;
        }


        if (currentHealth <= 0)
        {
            isDead = true;
        }
    }

    private void ShieldTakeDamage(string bullet)
    {

        if (bullet == "Bullet")
        {
            currentShield -= 5;
           
        }

        if (currentShield <= 0)
        {
            rend.enabled = false;
            boxCollider.enabled = true;
            capsuleCollider.enabled = false;
            isDisabled = true;
        }

    }

}
