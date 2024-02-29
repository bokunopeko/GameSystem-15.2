using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : CustomController
{
    [SerializeField] private Transform cameraTransform;


    protected override void Move(Vector3 direction)
    {
        //transform the direction vased on where the camera is looking 
        direction = cameraTransform.TransformDirection(direction);

        //if we are inputting a direction 
        if(direction.magnitude > 0.5f)
        {
            //determin our forward direction based on our inputs
            Vector3 facingDirection = new Vector3(direction.x, 0, direction.z);

            //set our forward direction to that
            transform.forward = facingDirection;
        }
        //run our existing Move code with our new direction 
        base.Move(direction);


    }



}
