using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scanner : HitScanFromScreen
{
    private ScanPopup scanPopup;

   
    //override = replace parent code with this code
    protected override void Start()
    {
        //run my parent's start()
        base.Start();
        scanPopup = FindObjectOfType<ScanPopup>();
    }

    
    void Update()
    {
        if  ( Input.GetButton("Scan"))
        {
            Scan();
        }
    }
    
    //run this when we try to can something
    private void Scan()
    {
        //run he code from parent, and save the result to hit
        RaycastHit hit = CastFromScreenCentre();
        //if we hit somethin, and it has scannable script (which we save in scan)
        if (hit.collider && hit.collider.TryGetComponent<Scannable>(out Scannable scan))
        {
            scan.Scan(scanPopup);
        }
    }

}
