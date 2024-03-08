using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class HumanBehavior : MonoBehaviour
{
    public enum HumanStates
    {
        WANDERING,
        FLEEING
    }

    public bool AiEnabled = true;

    [Header("Wandering parameters")]
    [SerializeField] float minDelay;
    [SerializeField] float maxDelay;
    [SerializeField] float minDistance;
    [SerializeField] float maxDistance;
    [SerializeField] float wanderingSpeed;

    [Header("Fleeing parameters")]
    [SerializeField] float fleeingSpeed;
    [SerializeField] float checkTime;
    [SerializeField] float calmingDuration;


    public HumanStates currentState;

    private IEnumerator movement;
    private NavMeshAgent agent;
    public Transform _source;
    private float lastAlerted = 0;

    
    void Start()
    {
        movement = move();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = wanderingSpeed;
        StartCoroutine(movement);
    }

    private void FixedUpdate()
    {
        if (currentState == HumanStates.FLEEING)
        {
            lastAlerted += Time.deltaTime;
            if (lastAlerted >= calmingDuration)
            {
                lastAlerted = 0;
                currentState = HumanStates.WANDERING;
                agent.speed = wanderingSpeed;
                _source = null;
            }
        } 
    }
    
    public void Alert(Transform source)
    {
        lastAlerted = 0;
        currentState = HumanStates.FLEEING;
        agent.speed = fleeingSpeed;
        _source = source;
        StopCoroutine(movement);
        StartCoroutine(movement); 
    }

    private IEnumerator move()
    {
        while (true)
        {
            if (AiEnabled)
            {
                if (currentState == HumanStates.WANDERING)
                {
                    Vector2 destination = Random.insideUnitCircle * (maxDistance - minDistance);
                    agent.SetDestination(new Vector3(destination.x + minDistance + transform.position.x, 0, destination.y + minDistance + transform.position.z));
                    yield return new WaitForSeconds(Random.value * (maxDelay - minDelay) + minDelay);
                }
                else if (currentState == HumanStates.FLEEING)
                {
                    if (_source == null)
                    {
                        currentState = HumanStates.WANDERING;
                        agent.speed = wanderingSpeed;
                        continue;
                    }
                    Vector3 moveVector = (transform.position - _source.position).normalized * 20;
                    agent.SetDestination(new Vector3(transform.position.x + moveVector.x, 0, transform.position.z + moveVector.z));
                    yield return new WaitForSeconds(checkTime);
                }
            } 
            else
            {
                yield return new WaitForSeconds(1);
            }
        }
    }
}
