using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CustomController : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float crouchSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float gravity;
    [SerializeField] private LayerMask groundedMask;

    [SerializeField] private float maxGrappleSpeed;

    //how close to the ground the base of the player must be to the grounded 
    [SerializeField] private float groundedAllowance = 0.05f;

    //how steep (in degrees) before the player starts to slide down 
    [SerializeField] private float walkAngle = 40f;

    private Rigidbody rb;

    //this will hold the player's input during an update loop 
    private Vector2 inputThisFrame = new Vector2();
    //this will hold our calculated movement during an update loop
    private Vector3 movementThisFrame = new Vector3();

    //hold a reference to our CameraSwapper Scripts in the scene
    private CameraSwapper cameraSwapper;

    //store out momentum horizontally and vertically
    private float horizontalSpeed, verticalSpeed;

    //to track we;re currently grappling
    private bool isGrappling;

    private float currentGrappleSpeed;

    private Vector3 grapplePoint = new Vector3();



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraSwapper = GetComponent<CameraSwapper>();
    }

   
    void Update()
    {
        inputThisFrame = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        inputThisFrame.Normalize();

        if(Input.GetMouseButtonDown(0))
        {
           TryToGrapple();
        }

        if (Input.GetMouseButtonUp(0))
        {
            EndGrapple();
        }

        if ( isGrappling  )
        {
            manageActiveGrapple();
        }
        else
        {
            ManageNormalMovement();
        }

        ManageNormalMovement();
    }

    private void TryToGrapple()
    {
        //set a new grapple point using our grapple scripty
        grapplePoint = GetComponent<Grapple>().ActivateGrapple();

        //if the point not 0,0,0
        if (grapplePoint != Vector3.zero )
        {
            StartGrapple();
        }
    }

    private void StartGrapple()
    {
        isGrappling = true; 
    }

    private void manageActiveGrapple()
    {
        //move up towards our max speed 
        currentGrappleSpeed += maxGrappleSpeed * Time.deltaTime;

        //clamp our grapple speed 
        currentGrappleSpeed = Mathf.Clamp( currentGrappleSpeed, 0, maxGrappleSpeed );

        //figure out the direction to the grapple point 
        //direction from point A to point B, is (B - A)
        Vector3 grappleDirection = (grapplePoint - transform.position);
        grappleDirection.Normalize();
        //if we arrive at the destination...
        if ( Vector3.Distance(transform.position, grapplePoint) < 2f )
        {
            //end te grappple 
            EndGrapple();
        }
        else
        {
            //we want to move 
            Move(grappleDirection * currentGrappleSpeed);

        }
    }

    private void EndGrapple()
    {
        isGrappling = false;
        grapplePoint = Vector3 .zero ;
        currentGrappleSpeed = 0;
        rb.velocity = Vector3.zero ;
    }

    virtual protected void Move(Vector3 direction)
    {
        rb.velocity = direction;
    }

    private void ManageNormalMovement()
    {
        movementThisFrame = new();

        movementThisFrame.x = inputThisFrame.x;
        movementThisFrame.z = inputThisFrame.y;

        float speedThisFrame = walkSpeed;

        if (IsGrounded())
        {
            if (Input.GetButton("Sprint"))
            {
                speedThisFrame = runSpeed;
            }
            else
                if (Input.GetButton("Crouch"))
            {
                speedThisFrame = crouchSpeed;
            }
        }

        //convert movement from global to local
        movementThisFrame = TransformDirection(movementThisFrame);

        //check if we're trying to moce, and if theat move is valid
        if (inputThisFrame.magnitude > 0 && ValidateDirection(movementThisFrame))
        {
            //if so, increase  hhorziontal speed 
            horizontalSpeed = Mathf.MoveTowards(horizontalSpeed, speedThisFrame, runSpeed * Time.deltaTime);
        }


        else
        { //decrease horizontal speed 
            horizontalSpeed = Mathf.MoveTowards(horizontalSpeed, 0, runSpeed * Time.deltaTime);
        }

        //multiply the direction by our horizontal speed
        movementThisFrame *= horizontalSpeed;

        //maintain current vertical speed, and apply gravity
        verticalSpeed -= gravity * Time.deltaTime;

        //check if we;re on the ground
        if (IsGrounded())  // <- remember, isGrounded() will result in true or false 
        {
            verticalSpeed = Mathf.Clamp(verticalSpeed, 0, float.PositiveInfinity);

            if (Input.GetButton("Jump"))
            {
                //moce upwards based on our jump power
                verticalSpeed = jumpPower;
            }
        }
        movementThisFrame.y = verticalSpeed;


        Move(movementThisFrame);
    }


    private bool IsGrounded()
    {
        //old code
        // return Physics.Raycast(transform.position, Vector3.down, 1.001f, groundedMask);

        //check if we're standing on solid ground
        //cast the sphere down with .5f radius and see if we hit anything
        if (Physics.SphereCast(transform.position, 0.5f, Vector3.down, out RaycastHit hit, (1 + groundedAllowance) / 2f, groundedMask))
        {
            //if we do...
            //check if it's flat, if it  is,  we must be on solid ground 
            return ValidateGroundAngle(hit.normal);//hit.normal is the normal of the face we hit 

        }
        //raycast down and see uf we hit anything <- this part need imoroving
        //if we do, we must be on solid ground 
        return false;
    }



    //face the direction we're moving
    private void FaceDirection(Vector3 directionToFace)
    {
        //if we're in first person mode...
        if (cameraSwapper.GetCameraMode() == CameraSwapper.CameraMode.FirstPerson)
        {
            //snap to face the forward of the camera
            transform.localEulerAngles = new Vector3 (0, directionToFace.y, 0);
            
        }

        else
        {
            
            //rotate towards our input direction 
            transform. forward = new Vector3 (directionToFace.x , 0 , directionToFace.z);
        }
    }


    

    private bool ValidateDirection(Vector3 direction)
    {
        //check if we're trying to move onto solid ground
        //raycast where we're about to be?
        if (Physics.SphereCast(transform.position + Vector3.down * .5f, .5f , direction, out RaycastHit hit, .5f, groundedMask))
        {
            //if we find gorund
            //check if it's flat
            return ValidateGroundAngle(hit.normal);
        }

        //if it's. we're on solid ground
        // if not, we're not 
        //else, we're not moving to solid ground
        return true;
    }

    private Vector3 TransformDirection(Vector3 direction)
    {
        //translate our input into the correct direction 
        //check what camera mode we're in
        if (cameraSwapper.GetCameraMode() == CameraSwapper.CameraMode.FirstPerson)
        {
            //if we're in first person...

            //make sure we're looking tge same wat as the camera 
            FaceDirection(cameraSwapper.GetCurrentCamera().transform.localEulerAngles);
            //transform the direction based on our forward direction (this tranform)
            return transform.TransformDirection(direction);
        }
        //else we nyst be in third person
        //transfrom the direction
        return cameraSwapper.GetCurrentCamera().transform.root.TransformDirection(direction);
    }

    private bool ValidateGroundAngle(Vector3 groundNormal)
    {
        //validating if the ground we're on is sloped or not
        // check the angle of the ground
        if (Vector3.Angle(Vector3.up, groundNormal) < walkAngle)
        {
            //if it's too steep, it's not valid
            return true;
        }
        //else it is valid
        return false;
    }
}
