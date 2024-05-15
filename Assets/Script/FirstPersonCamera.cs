using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] private float sensitivity;
    [SerializeField] private float verticalRotationMin, verticalRotationMax;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float playerEyeLevel = 0.5f;

    private float currentHozizontalRotation, currentVerticalRotation;



    // Start is called before the first frame update
    void Start()
    {
        currentHozizontalRotation = transform.localEulerAngles.y;
        currentVerticalRotation = transform.localEulerAngles.x;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.currentState != GameState.Play)
        {
            return;
        }

        //rotate based on input
        currentHozizontalRotation += Input.GetAxis("Mouse X") * sensitivity;
        currentVerticalRotation -= Input.GetAxis("Mouse Y") * sensitivity;

        //clamp our up and down axis
        currentVerticalRotation = Mathf.Clamp(currentVerticalRotation, verticalRotationMin, verticalRotationMax);

        transform.localEulerAngles = new Vector3(currentVerticalRotation, currentHozizontalRotation);
        transform.position = playerTransform.position + (Vector3.up * playerEyeLevel);

    }
}
