using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    //this will let us follow the player
    [SerializeField] private Transform playerTransform;
        //this will let us choose how fast the camera rotates
    [SerializeField] private float sensitivity;
    //these will preven the camera from rotating
    [SerializeField] private float verticalRotationMin,  verticalRotationMax;


    [SerializeField] private float cameraZoom;

    [SerializeField] private LayerMask avoidLayer;

    //what the camera should try to zoom to right now
    private float idealCameraZoom;

    //the actual current zoom 
    private float currentZoom;

    

    private float currentHorizontalRoation, currentVerticalRotation;
    private Transform cameraTranform, boomTransform;


    void Start()
    {
        //get the transform of the boom and camera
        boomTransform = transform.GetChild(0);
        cameraTranform = boomTransform.GetChild(0);

        


        //get the current rotation values and save them in variables
        currentHorizontalRoation = transform.localEulerAngles.y;
        currentVerticalRotation = boomTransform.localEulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        //increase rotation based on x and y mouse movements
        currentHorizontalRoation += Input.GetAxis("Mouse X") * sensitivity;
        currentVerticalRotation -= Input.GetAxis("Mouse Y") * sensitivity;

        currentVerticalRotation = Mathf.Clamp(currentVerticalRotation, verticalRotationMin, verticalRotationMax);

        //set our transforms new roation based on that 
        transform.localEulerAngles = new Vector3(0, currentHorizontalRoation);
        boomTransform.localEulerAngles = new Vector3(currentVerticalRotation, 0, 0);


        //snap our current position to the player
        transform.position = playerTransform.position;

        //direction from A9player0 to B(camera) is "B - A"
        Vector3 directionToCamera = cameraTranform.position - playerTransform.position;
        directionToCamera.Normalize();
        if(Physics.Raycast(playerTransform.position, directionToCamera, out RaycastHit hit,cameraZoom, avoidLayer))
        {
            //zoom the camera in to be on the other side of the wall
            currentZoom = hit.distance; 

        }
        else 
        {
            //return to our default zoom
            currentZoom = cameraZoom;

        }
        
        cameraTranform.localPosition = new Vector3(0, 0, -currentZoom);

    }
}
