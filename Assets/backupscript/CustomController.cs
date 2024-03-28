using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
//                v  need a copy of the compenent
[RequireComponent(typeof(Rigidbody))]
 
public class CustomControllerBackUp : MonoBehaviour
{
    //[serializefield] allow unity display the variable in the component inspector
    //these ae responiible for our movement speed
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float crouchSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float gravity;

    //this will track the camera anchor object and let it follow the plater
    [SerializeField] private GameObject cameraAnchor;

    
    [SerializeField] private LayerMask groundMask;

    //this will hold the player objects rigidbody
    private Rigidbody rb;

    //this will hold the players inputs this Update loop
    private Vector2 inputThisFrame = new Vector2();

    //this will hold our calculated movement this Update loop
    private Vector3 movementThisFrame = new Vector3();


    // Start is called before the first frame update
    void Start()
    {
        // get the rigidbody component from the player
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        //check what input we have this frame
        inputThisFrame = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));

        //reset our movement to (0,0,0) by default
        movementThisFrame = new Vector3();

        //map out horizontal movement based on out inputs
        movementThisFrame.x = inputThisFrame.x;
        movementThisFrame.z = inputThisFrame.y;

        //get a temporary float, just for theis frame, to figure out oour speed 
        float speedThisFrame = walkSpeed;

        if (IsGrounded())
        {
            if (Input.GetButton("Sprint"))
            {
                Debug.Log("is working");
                speedThisFrame = runSpeed;
            }
            else
                if (Input.GetButton("Crouch"))
            {
                speedThisFrame = crouchSpeed;
            }
        }
       

        //Multiplty that direction by our speed
        movementThisFrame *= walkSpeed;

        //get our y movement from the last frame, and pull down gravity
        movementThisFrame.y = rb.velocity.y - gravity * Time.deltaTime;

        //check if we're on the ground
        if(IsGrounded())//isGrounded()will result in "ture' or "false"
        {
            //if we are check if the jump button is presed
            
            if (Input.GetButtonDown("Jump"))//Getbuttondown will only triger once per press
            {
                //if it is, set our y direction to our jump power
                movementThisFrame.y = jumpPower;
            }

          

        }

        //Move out player object
        Move(movementThisFrame);

        //move the camera anchoer to snap the player position
        cameraAnchor.transform.position = transform.position;



    }

    private void Move(Vector3 moveDirection)
    {
        rb.velocity = moveDirection;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.001f, groundMask);
    }
}
