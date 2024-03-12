using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    [SerializeField] float enemySearchRadius;

    private Camera camera;
    private NavMeshAgent agent;

    private Transform target;
    public bool atackMode = false;
    
    public LayerMask groundLayer;
    public LayerMask enemyLayer;

    private IEnumerator atackSequance;

    private void Start()
    {
        camera = Camera.main;
        enemyLayer = LayerMask.GetMask("Enemy","EnemyBuildings");
        atackSequance = followTarget(0.5f);
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(ray, out hit, Mathf.Infinity, enemyLayer))
            {
                if (target != null) { target.Find("Indicator").gameObject.SetActive(false); }
                target = hit.collider.transform;
                Debug.Log(target.name);
                target.Find("Indicator").gameObject.SetActive(true);
                atackMode = true;
                StartCoroutine(followTarget(0.5f));
            }
            else if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                if (target != null) { target.Find("Indicator").gameObject.SetActive(false); }
                agent.SetDestination(hit.point);
                atackMode = false;
            } 
        }
        
    }
    private IEnumerator followTarget(float updateInterval)
    {
        while (atackMode)
        {
            if(target != null)
            {
                agent.SetDestination(target.position);
                yield return new WaitForSeconds(updateInterval);
            }
            else
            {
                if (searchForNewTarget())
                {
                    yield return new WaitForFixedUpdate();
                } 
                else
                {
                    atackMode = false;
                }
            }
        }
    }

    private bool searchForNewTarget()
    {
        RaycastHit[]results =  Physics.SphereCastAll(transform.position,enemySearchRadius,Vector3.forward,Mathf.Infinity,enemyLayer);
        foreach (RaycastHit hit in results)
        {
            //'If' statement is here for future aplications XD
            if (true)
            {
                target = hit.transform;
                target.Find("Indicator").gameObject.SetActive(true);
                return true;
            }
        }
        return false;
    }
}


