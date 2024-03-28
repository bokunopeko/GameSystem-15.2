using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HitScanFromScreen : MonoBehaviour
{
    [SerializeField] private LayerMask hitLayel;
    [SerializeField] private float MaxDistance;

    private CameraSwapper cameraswapper;


    //protected = child scripts can see it, virtual = can be overridden
    protected virtual void Start()
    {
        cameraswapper = FindObjectOfType<CameraSwapper>();
    }

    protected virtual RaycastHit CastFromScreenCentre()
    {
        //get the current camera and store it in a variable
        Camera currentCamera = cameraswapper.GetCurrentCamera();

        //create a ray from the cnetre of the scree . viewport botom left = 0,0 to top right = 1,1,
        Ray ray = currentCamera.ViewportPointToRay(Vector3.one * 0.5f);

        //if the ray hits anything
        if(Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity, hitLayel))
        {
            return hit;
        }
        //if we dont hit anything...
        return new RaycastHit();
    }

}
