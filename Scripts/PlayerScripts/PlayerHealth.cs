using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float currentHealth = 100;
    private float initialHealth = 100;
    public float currentShield = 100;
    private float initialShield = 100;

    public Image healthBar;
    public float sinkSpeed = 2.5f;
    MeshCollider meshCollider;
    CapsuleCollider capsuleCollider;
    public float regenShield = 5.0f;
    private float shieldTimer = 0f;

    bool isDead = false;
    bool isDisabled = false;
    bool isSinking;

   

    Transform shield;
    Renderer rend;


    // Use this for initialization
    void Start()
    {
        currentHealth = initialHealth;
        currentShield = initialShield;
        meshCollider = GetComponent<MeshCollider>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        capsuleCollider.enabled = false;

    
    }
    
    // Update is called once per frame
    void Update()
    {

        CheckShield();


        if (isDead)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
        RegenerateShield();

        //UpdateUI();
    }
    void UpdateUI()
    {

        healthBar.fillAmount = currentHealth / initialHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        string bulletType = collision.collider.tag;

        Debug.Log(bulletType);

        if (capsuleCollider.enabled == false || currentShield <=0)
        {
            TakeDamage(bulletType);
        }

        if (capsuleCollider.enabled == true && currentShield > 0)
        {
            ShieldTakeDamage(bulletType);
        }
    }

    private void TakeDamage(string bullet)
    {
        //based on string, can change how much damage the enemy takes
        //based on which type of bullet i.e. stronger weapon 
        //will have different bullets
        
        if (bullet == "EnemyBullet")
        {
            currentHealth -= 10;
        }

        if(bullet == "Prowler")
        {
            currentHealth -= 5;
        }

        if (currentHealth <= 0)
        {
            isDead = true;
        }
    }

    private void ShieldTakeDamage(string bullet)
    {

        if (bullet == "EnemyBullet" || bullet == "Prowler")
        {
            currentShield -= 10;
            //Debug.Log(currentHealth);
        }
      

        if (currentShield <= 0)
        {
            meshCollider.enabled = true;
            capsuleCollider.enabled = false;
            isDisabled = true;
        }

    }

    //Checks if shield is activated to invoke the proper collider
   private void CheckShield()
    {
        //Space bar for some reason wont let you move at 45 degr and
        //225 deg, so switched to shift.
        if (Input.GetKey(KeyCode.LeftShift))
        {
            meshCollider.enabled = false;
            capsuleCollider.enabled = true;
      
        }
        else
        {
           
            meshCollider.enabled = true;
            capsuleCollider.enabled = false;
        }
    }

    private void RegenerateShield()
    {
        if(currentShield < 100)
        {
            shieldTimer += Time.deltaTime;

        }

        if(shieldTimer > 5)
        {
            currentShield += 10;
            shieldTimer = 0;
        }
            

    }

}
