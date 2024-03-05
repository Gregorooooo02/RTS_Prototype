using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    private Camera camera;
    private NavMeshAgent agent;

    private Transform target;
    private bool atackMode = false;
    
    public LayerMask groundLayer;
    public LayerMask enemyLayer;

    private void Start()
    {
        camera = Camera.main;
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
        while (atackMode && target != null)
        {
            agent.SetDestination(target.position);
            yield return new WaitForSeconds(updateInterval);
        }
        target = null;
        atackMode = false;
    }
}


