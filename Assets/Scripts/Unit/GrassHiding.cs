using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassHiding : MonoBehaviour
{
    public bool isHidden = false;

    private int enters = 0;

    [SerializeField] Material normal;
    [SerializeField] Material hidden;
    private MeshRenderer mesh;

    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        mesh.material = normal;
    }

    public void updateEnters(int change)
    {
        enters += change;
        if(enters > 0 && !isHidden)
        {
            mesh.material = hidden;
            isHidden = true;
        } 
        else if (enters == 0 && isHidden)
        {
            mesh.material = normal;
            isHidden = false;
        }
    }
}
