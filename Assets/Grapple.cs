using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : HitScanFromScreen
{
    
    public Vector3 ActivateGrapple()
    {
        RaycastHit hit = CastFromScreenCentre();
        if(hit.collider) // if "hit" contains a a collider, this will be true 
        {
            return hit.point;
        }

        //is we didn;t hit a collider, retuen v3.zero
        return Vector3.zero;
    }

}
