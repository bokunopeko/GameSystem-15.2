using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //<---- access NavMesh and navmeshagent stuff

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyWalk : EnemyBase
{
    [Tooltip("How far the enemy is allowed to move from its start point")]
    [SerializeField] protected float wanderDistance = 20f;

    [Tooltip("whether the enemy should stop moving while attacking ")]
    [SerializeField] protected bool stopWhileAttacking = true;

    protected NavMeshAgent agent;
    protected Vector3 homePosition;
    protected Vector3 targetPosition;

    protected override void Start()
    {
        base.Start();

        homePosition = transform.position;
        agent = GetComponent<NavMeshAgent>();
    }

    protected override void Update()
    {
        base.Update();
        if (isAttacking && stopWhileAttacking)
        {
            agent.isStopped = true;

        }
        else
        {
            agent.isStopped = false;
        }

        if (agent.remainingDistance <2f && !agent.isStopped && !isAttacking)
        {
            agent.isStopped = true;
            StartCoroutine("WaitAndChooseNewLocation");
        }
    }

    protected IEnumerator WaitAndChooseNewLocation()
    {
        yield return new WaitForSeconds(Random.Range(2f, 4f));
    }

    protected void ChooseNewLocation()
    {
        //get a random point inside our home radius 
        Vector2 randomTraget = Random.insideUnitCircle * wanderDistance;
        Vector3 flatTarget = new Vector3(homePosition.x + randomTraget.x , homePosition.y , homePosition.z + randomTraget.y);

        // find the point on the navmesh that matches
        RaycastHit hit = new();
        Physics.Raycast(flatTarget + Vector3.up * 1000f, Vector3.down, out hit );

        //set that to our new destination 
        targetPosition = hit.point;
        agent.isStopped = false;
        agent.SetDestination(targetPosition);
    }

}
