using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraIsometric : MonoBehaviour {

    public Transform target;
    public float smoothing = 5f;
    
    Vector3 offset;
 

    float curZoomPos, zoomTo;
    float zoomFrom = 20f;

    private void Start()
    {
        offset = transform.position - target.position;
        
        //offset = offset + (new Vector3(30, 0, 10));
    }

    private void FixedUpdate()
    {
        float y = Input.mouseScrollDelta.y;

        if (y == 0f)
        {
            //Do nothing
        }

        else if (y <= -1)
        {
            zoomTo += 5f;
        }
        else if (y >= 1)
        {
            zoomTo -= 5F;
        }

        // creates a value to raise and lower the camera's field of view
        curZoomPos = zoomFrom + zoomTo;

        curZoomPos = Mathf.Clamp(curZoomPos, 5f, 35f);

        zoomTo = Mathf.Clamp(zoomTo, -15f, 30f);

        // Makes the actual change to Field Of View
        Camera.main.fieldOfView = curZoomPos;


       

    }

    private void LateUpdate()
    {
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos,
            smoothing * Time.deltaTime);
    }


}
