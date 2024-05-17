using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunFacePlayer : EnemyGun
{
    private Transform playerTransform;

    protected override void Start()
    {
        base.Start();
        playerTransform = FindObjectOfType<CustomController>().transform;

    }


    protected override void SetDirection()
    {
        psysWeapon.transform.LookAt(playerTransform);
    }
}
