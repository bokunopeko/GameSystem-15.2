using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Scannable))]
public abstract class EnemyBase : CombatAgent
{
    //how close can the player get before the enemy starts to attack 
    [SerializeField] protected float aggroRange;
    protected Transform playerTransform;
    //define if the nemy is currently attacking or not 
    protected bool isAttacking;
    protected EnemyGun myGun;

    //virtual = can be overriuden, doesnt have to be 
    protected virtual void Start()
    {
        //find the player 
        playerTransform = FindObjectOfType<PlayerGun>().transform;
        //get our gun
        myGun = GetComponent<EnemyGun>();
    }

    protected virtual void Update()
    {
        isAttacking = false;
        if (Vector3.Distance(playerTransform.position, transform.position) < aggroRange)
        {
            //trigger the attack 
        }
    }
    protected virtual void DoAttack()
    {
        myGun.Shoot();
        isAttacking = true;
    }

    protected override void EndOfLife()
    {
        Debug.Log("enemy dead");
        gameObject.SetActive(false);
    }


}
