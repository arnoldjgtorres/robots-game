using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
   // Vector3 movement;
    Animator anim;
    private Rigidbody playerRigidbody;
    int floorMask;
    //float camRayLength = 100f;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float timeBetweenBullets = 0.05f;
    private float timeAcc = 0f;

  
    private void Start()
    {

        //floorMask = LayerMask.GetMask("Ground");
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        float v = Input.GetAxisRaw("Vertical")* Time.deltaTime * speed;
        //float h = Input.GetAxisRaw("Horizontal");
        //float v = Input.GetAxisRaw("Vertical");

        //transform.Rotate(0, h, 0);
        //transform.Translate(h, 0, v);

        Move(h, v);
        Turning();
        Animating(h,v);

        if (Input.GetMouseButton(0))
        {
            Fire();
        }
    }




    void Move(float h, float v)
    {
        //Vector3 movement = new Vector3(h, 0.0f, v);
        //playerRigidbody.AddForce(movement * speed);

        Vector3 movement = new Vector3(h, 0.0f, v);
        playerRigidbody.AddForce(movement * speed);
        movement.Set(h,0,v);
        movement = movement.normalized * speed * Time.deltaTime;

        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Turning()
    {
         Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

         RaycastHit floorHit;


        if(Physics.Raycast(camRay, out floorHit))
        {
            Vector3 targetPosition = new Vector3(floorHit.point.x, transform.position.y,
                floorHit.point.z);
            transform.LookAt(targetPosition);

        }


         /*if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
         {
             Vector3 playerToMouse = floorHit.point - transform.position;
             playerToMouse.y = 0f;

             Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
             playerRigidbody.MoveRotation(newRotation);

         }*/
      

    }

    void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsRunning", walking);


        if (Input.GetMouseButton(0))
        {
            anim.SetBool("IsShooting", true);
        }
        else
        {
            anim.SetBool("IsShooting", false);
        }   
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
