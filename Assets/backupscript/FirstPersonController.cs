using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonControllerbackup : CustomController
{
    [SerializeField] private Transform cameraTransform;
    protected override void Move(Vector3 direction)
    {
        transform.localEulerAngles = new(0,cameraTransform.localEulerAngles.y, 0);
        direction = transform.TransformDirection(direction);
        base.Move(direction);
    }
}
