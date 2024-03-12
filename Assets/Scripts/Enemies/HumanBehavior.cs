using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public abstract class HumanBehavior : MonoBehaviour
{
    public enum HumanStates
    {
        WANDERING,
        FLEEING,
        PATROLING,
        ATACKING
    }

    public bool AiEnabled = true;
    public HumanStates currentState;

    protected IEnumerator movement;
    protected NavMeshAgent agent;
    public abstract void Alert(Transform source);
}
