using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchGrass : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            other.gameObject.GetComponent<GrassHiding>().updateEnters(1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            other.gameObject.GetComponent<GrassHiding>().updateEnters(-1);
        }
    }
}
