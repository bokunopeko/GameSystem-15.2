using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
  

    public float speed;
    public float jumpPower;
    //how much faster the player can sprint for
    public float sprintMultiplier;

    //making Rigidbody into a variables
    //and asigned Capsule(game object) into rb in gameobject component 
    public Rigidbody rb;

   


    // Start is called before the first frame update
    void Start()
    {
        // assigning rb(variable) into rigidbody incase rb(in component) forget to set as none
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //putting sprintMultiplier at firt so all the code will applying into them 
        if (Input.GetKey("left shift"))
        {
            //(how?) if didnt set multiplier to a value, it can be set to any inside the game component
            sprintMultiplier = 1.3f;
        }
        //if shift key not been held
        else 
        {
            sprintMultiplier = 1.0f;
        }


        //adding rigidboy(component) applying physic to the game object
        //applying (constrains) in rigidbody->component freeze (x,y,z) position of the game object
        //dealt time meanning (01;26;00)
        //if (key) is press
        if (Input.GetKey("w"))
        {
            //getcompenent from the game object -> <rid> .(fullstops mean look for) (15:00) 
            //vector 3 is the position of the object (x,y,z)
            //                                                                              //dealt time meanning (01;26;00)
            GetComponent<Rigidbody>().AddForce(Vector3.forward * speed * sprintMultiplier);
        }

       if(Input.GetKey("s"))
        {
            //we replace Getcomponent with "rb" (the variables) to create a shortcut
            rb.AddForce(Vector3.back * speed * sprintMultiplier);
        }

       if(Input.GetKey("a"))
        {
            rb.AddForce(Vector3.left * speed * sprintMultiplier);
        }

       if(Input.GetKey("d"))
        {
            rb.AddForce(Vector3.right * speed * sprintMultiplier);
        }

       //getkeydown is a one time action(?)
       if(Input.GetKeyDown("space"))
        {
            //adding (ForceMode. impulse) changing how the physics work of the action, which create a better movement
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }

    }
}
