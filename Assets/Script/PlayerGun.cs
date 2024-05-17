using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : HitScanFromScreen
{
    //how much damage our gun will do 
    [SerializeField] private float damage;
    //how much the gun cost to use
    [SerializeField] private float cost;

    private CustomController player;

    protected override void Start()
    {
        base.Start();
        player = GetComponent<CustomController>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Shoot") && player.TryToUseStamina(cost))
        {
            Shoot();
        }
    }


    //try to find combat agent
    private void Shoot()
    {

        RaycastHit hit = CastFromScreenCentre();
        if (hit.collider) // making sure if we're hitting anything
        {
            if(hit.collider.TryGetComponent<CombatAgent>(out CombatAgent agent)) //if we do 47;40 09may
            {
                agent.TakeDamage(damage);
            }
        }
    }
}
