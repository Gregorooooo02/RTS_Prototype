using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HumanMovement : MonoBehaviour
{
    public float minDelay;
    public float maxDelay;
    public float minDistance;
    public float maxDistance;

    
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(move());
    }

    private IEnumerator move()
    {
        while (true)
        {
            Vector2 destination = Random.insideUnitCircle * (maxDistance - minDistance);
            agent.SetDestination(new Vector3(destination.x + minDistance + transform.position.x, 0 , destination.y + minDistance + transform.position.z));
            yield return new WaitForSeconds(Random.value * (maxDelay - minDelay) + minDelay);
        }
    }
}
