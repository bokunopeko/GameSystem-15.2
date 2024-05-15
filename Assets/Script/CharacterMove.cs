using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public CharacterController controller;
    public float speed;
    public float jumpPower;
    public float gravity;

    public Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 240;

        //get the conponent of the gameobject <CharacterController> an save it in a var.
        controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        //moveDirection = Vector3.zero; isnt use because it lock .y
        //refresh our  input
        moveDirection.x = 0;
        moveDirection.z = 0;

        //check character  movement front/back
        if(Input.GetKey("w"))
        {
            // the z value of movediretion to +1
            moveDirection.z += 1;
        }
        if(Input.GetKey("s"))
        {
            moveDirection.z -= 1;
        }

        //check character  movement left/right
        if (Input.GetKey("a"))
        {
            moveDirection.x -= 1;
        }
        if(Input.GetKey("d"))
        {
            moveDirection.x += 1;
        }

        //the direction of moving * speed (x) *  delta time meanning (01;26;00)
        //multipy horizontal movement by speed and fix it to time(delta time)
        moveDirection.x = moveDirection.x * speed; //* Time.deltaTime; //we simplfily the code by adding * Time.deltaTime to code84
        moveDirection.z = moveDirection.z * speed; //* Time.deltaTime; //we simplfily the code by adding * Time.deltaTime to code84


        //CHECK 01;59;00
        //apply gravty to our vertical movement
        moveDirection.y -= gravity * Time.deltaTime;// * Time.deltaTime; //we simplfily the code by adding * Time.deltaTime to code84

        //the character is not consistence on the ground
        //changing the frame rate adding the consistence of the chance 
        //if we're on the ground 
        if (controller.isGrounded)
        {
            // - o.1f is the minimum so we dont fall through the floor, flloat.PositiveInfinit is the maximum
            //clamp our vertical speed so we dont fall through the floor
            moveDirection.y = Mathf.Clamp(moveDirection.y,-0.1f,float.PositiveInfinity);
   
            //if the player presses space while on the """"""ground""""""
            if (Input.GetKeyDown("space")) 
            {
                //.y (jump/down)
                //set vertical to our jump power
                moveDirection.y = jumpPower;
            }
        }

       
        //tell the controller to move 
        controller .Move(moveDirection * Time.deltaTime);
    }
}
