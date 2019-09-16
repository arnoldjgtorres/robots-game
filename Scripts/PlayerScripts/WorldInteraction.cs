using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorldInteraction : MonoBehaviour {
    UnityEngine.AI.NavMeshAgent playerAgent;
    private float playerSpeed;

    private void Start()
    {
        playerSpeed = 7f;
        playerAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }


    private void Update()
    {
        //the && is for interacting with UI in game while not interacting with something in the background like a tree
        if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            //If using point and click uncomment this function and comment the 'else' below.
            GetInteraction();
            
        }

       
        else
        {
            transform.Translate(playerSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0f, playerSpeed
                * Input.GetAxis("Vertical") * Time.deltaTime);
        }

        
    }


    void GetInteraction()
    {
        //Camera.main is referencing the main camera in the game. so making a ray from camera to point clicked?
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionInfo;
        if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
        {
            GameObject interactedObject = interactionInfo.collider.gameObject;

            if(interactedObject.tag == "Interactable Object")
            {
                Debug.Log("Interactable interacted");
            }
            else
            {
                playerAgent.destination = interactionInfo.point;
            }

        }
    }



}
