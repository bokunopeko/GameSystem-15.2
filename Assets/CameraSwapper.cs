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
        if (currentCameraMode == CameraMode.FirstPerson)
        {
            currentCameraMode = CameraMode.ThirdPerson;
        }
        else
        {
            currentCameraMode = CameraMode.FirstPerson
        }

        SetCamera();
    }
    
    private void SetCamera()
    {

    }
}
