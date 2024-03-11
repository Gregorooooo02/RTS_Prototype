using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Soldier : HumanBehavior
{
    [Header("Patroling parameters")]
    [SerializeField] float minDelay;
    [SerializeField] float maxDelay;
    [SerializeField] float minDistance;
    [SerializeField] float maxDistance;
    [SerializeField] float patrolingSpeed;

    [Header("Atacking parameters")]
    [SerializeField] float moveSpeed;
    [SerializeField] float searchRadius;
    [SerializeField] float checkTime;
    [SerializeField] float calmingDistance;

    private Transform _target;
    public LayerMask playerLayer;

    void Start()
    {
        movement = move();
        agent = GetComponent<NavMeshAgent>();
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
    }

    public override void Alert(Transform source)
    {
        if(currentState != HumanStates.ATACKING)
        {
            currentState = HumanStates.ATACKING;
            _target = source;
            agent.speed = moveSpeed;
            StopCoroutine(movement);
            StartCoroutine(movement);
        }
    }

    private IEnumerator move()
    {
        //TODO: Implement XD
        yield return new WaitForSeconds(2);
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
}
