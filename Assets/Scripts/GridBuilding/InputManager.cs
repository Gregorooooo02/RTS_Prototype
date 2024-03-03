using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class InputManager : MonoBehaviour
{

    [SerializeField] 
    private Camera sceneCamera;

    private Vector3 lastPosition;

    [SerializeField] private LayerMask placementLayermask;
    
    private bool isBuilding = false;

    // private void Update()
    // {
    //     if (Input.GetKeyDown("w"))
    //     {
    //         OnClicked?.Invoke();
    //     }
    //     if (Input.GetKeyDown(KeyCode.Escape))
    //     {
    //         OnExit?.Invoke();
    //     }
    // }
    
    public Vector3 getSelectedMapPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = sceneCamera.nearClipPlane;
        Ray ray = sceneCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, placementLayermask))
        {
            lastPosition = hit.point;
        }

        return lastPosition;
    }

    public void buildMode()
    {
        if(Input.GetKeyUp("b"))
        {
            isBuilding = !isBuilding;
            
        }
    }

    public bool getBuildMode()
    {
        return isBuilding;
    }
    
    public void setBuildMode(bool a)
    {
        isBuilding = a;
    }
}
