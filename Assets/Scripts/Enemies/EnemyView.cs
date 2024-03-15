using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    public HumanBehavior behavior;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            if(!other.GetComponent<GrassHiding>().isHidden) behavior.Alert(other.transform);
        }
    }
}