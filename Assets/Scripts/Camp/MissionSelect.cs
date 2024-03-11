using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class MissionSelect : MonoBehaviour
{
    private Camera cam;
    [SerializeField] GameObject missionSelectPanel;
    [SerializeField] GameObject unlockPanel;
    
    public LayerMask groundLayer;
    
    void Start()
    {
        cam = Camera.main;
        missionSelectPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.name == "Bonfire")
                {
                    missionSelectPanel.SetActive(true);
                    unlockPanel.SetActive(false);
                } else if (!EventSystem.current.IsPointerOverGameObject())
                {
                    missionSelectPanel.SetActive(false);
                    unlockPanel.SetActive(true);
                }
            }
        }
    }
}
