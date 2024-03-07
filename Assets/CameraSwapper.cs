using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class CameraSwapper : MonoBehaviour
{
    [SerializeField] private Camera firstPersonCamera;
    [SerializeField] private Camera thirdPersonCamera;
    [SerializeField] private FirstPersonController firstPersonController;
    [SerializeField] private ThirdPersonController thirdPersonController;


    public enum CameraMode
    {
        FirstPerson,
        ThirdPerson
    }

    [SerializeField] private CameraMode currentCameraMode;

    void Start()
    {
        firstPersonController = GetComponent<FirstPersonController>();
        thirdPersonController = GetComponent<ThirdPersonController>();

        //find the camera scripts first, and get the component off the same object
        firstPersonCamera = FindObjectOfType<FirstPersonCamera>().GetComponent<Camera>();
        thirdPersonCamera = FindObjectOfType<ThirdPersonCamera>().GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SetCamera();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCamera();
        }

    }

    private void ToggleCamera()
    {
        //if we are currently in First Person mode
        if (currentCameraMode == CameraMode.FirstPerson)
        {
            //swap th 3rd person mode
            currentCameraMode = CameraMode.ThirdPerson;
        }
        else
        {
            //else, we must be in 3rd person, so swap to 1st person
            currentCameraMode = CameraMode.FirstPerson;
        }

        SetCamera();
    }
    
    private void SetCamera()
    {
        //do somehing diferent depending on th value of the currentCameraMode
        switch (currentCameraMode)
        {
            //if currentcameraMode is CameraMode.FirstPerson...
            case CameraMode.FirstPerson :
                firstPersonCamera.depth = 0;
                firstPersonController.enabled = true;
                thirdPersonCamera.depth = -1;
                thirdPersonController.enabled = false;
                break;

            //if currentCameraMode is CameraMode.ThirdPerson
            case CameraMode.ThirdPerson :
                thirdPersonCamera.depth = 0;
                thirdPersonController.enabled = true;
                firstPersonCamera.depth = -1;
                firstPersonController.enabled = false;
                break;
        }
    }
}
