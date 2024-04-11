using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Crosshair : MonoBehaviour
{

    private Image crosshair;
    private CameraSwapper swapper;

    void Start()
    {
        crosshair = GetComponent<Image>();
        swapper = GetComponent<CameraSwapper>();
    }

    // Update is called once per frame
    void Update()
    {
        crosshair.enabled = Input.GetButton("Scope") || swapper.GetCameraMode() == CameraSwapper.CameraMode.FirstPerson;
    }
}
