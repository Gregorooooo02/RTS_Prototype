using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Soldier : HumanBehavior
{
    private enum patrolType
    {
        CIRCLE,
        RETURN
    }

    [Header("Patroling parameters")]
    [SerializeField] float minDelay;
    [SerializeField] float maxDelay;
    [SerializeField] float distanceToReroute;
    [SerializeField] float patrolingSpeed;
    public Transform[] waypoints;
    [SerializeField] patrolType PatrolType;

    [Header("Atacking parameters")]
    [SerializeField] float moveSpeed;
    [SerializeField] float searchRadius;
    [SerializeField] float checkTime;
    [SerializeField] float calmingDistance;

    private Transform _target;
    private LayerMask playerLayer;

    [SerializeField] bool walking = false;
    private bool switched = false;


    [SerializeField] int waypointIndex = 0;
    private bool backwards = false;

    void Start()
    {
        movement = move();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = patrolingSpeed;
        StartCoroutine(movement);
    }

    private void FixedUpdate()
    {
        if(currentState == HumanStates.ATACKING && _target != null)
        {
            if((transform.position - _target.position).magnitude >= calmingDistance)
            {
                currentState = HumanStates.PATROLING;
                agent.speed = patrolingSpeed;
                _target = null;
            }
        } 
        else if(currentState == HumanStates.PATROLING && walking)
        {
            if (Vector3.Distance(waypoints[waypointIndex].position,transform.position) <= distanceToReroute && !switched)
            {
                Invoke("changeWalking", Random.value * (maxDelay - minDelay) + minDelay);
                switched = true;
            }
        }
    }

    private void changeWalking()
    {
        walking = false;
        switched = false;
        if (PatrolType == patrolType.CIRCLE)
        {
            incrementIndex();
        }
        else if (PatrolType == patrolType.RETURN)
        {
            incrementIndexLooped();
        }
    }

    public override void Alert(Transform source)
    {
        if(currentState != HumanStates.ATACKING)
        {
            currentState = HumanStates.ATACKING;
            _target = source;
            agent.speed = moveSpeed;
        }
    }

    private IEnumerator move()
    {
        //TODO: Implement XD
        //yield return new WaitForSeconds(2);
        while (true)
        {
            if(currentState == HumanStates.PATROLING)
            {
                agent.SetDestination(waypoints[waypointIndex].position);
                walking = true;
                yield return new WaitWhile(()=> walking == true);
            }
            else if(currentState == HumanStates.ATACKING)
            {
                yield return new WaitForSeconds(1);
            }
        }
    }

    private bool searchForNewTarget()
    {
        RaycastHit[] results = Physics.SphereCastAll(transform.position, searchRadius, Vector3.forward, Mathf.Infinity, playerLayer);
        foreach (RaycastHit hit in results)
        {
            //'If' statement is here for future aplications XD
            if (true)
            {
                _target = hit.transform;
                return true;
            }
        }
        return false;
    }

    private void incrementIndexLooped()
    {
        if (waypointIndex == 0 && backwards)
        {
            backwards = false;
        } 
        else if(waypointIndex == waypoints.Length - 1 && !backwards)
        {
            backwards = true;
        }
        if (backwards)
        {
            waypointIndex--;
        }
        else
        {
            waypointIndex++;
        }
    }

    private void incrementIndex()
    {
        if (waypointIndex == waypoints.Length - 1)
        {
            waypointIndex = 0;
        } 
        else
        {
            waypointIndex++;
        }
    }
}
