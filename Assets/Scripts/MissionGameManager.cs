using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MissionGameManager : MonoBehaviour
{
    [SerializeField] public GameObject soldiers;
    [SerializeField] public BuildingManager buildingManager;
    
    public int numberOfSoldiers;
    public int numberOfBuildings;

    public bool areSoldiersDead;
    public bool areBuildingsDestroyed;

    private void Start()
    {
        areSoldiersDead = false;
        areBuildingsDestroyed = false;
    }

    private void Update()
    {
        numberOfBuildings = buildingManager.allBuildingsList.Count;
        numberOfSoldiers = soldiers.transform.childCount;
    }
    
    public void CheckMissionStatus()
    {
        if (numberOfSoldiers == 0)
        {
            Debug.Log("Task Complete");
            areSoldiersDead = true;
        }
        else if (numberOfBuildings == 0)
        {
            Debug.Log("Task Complete");
            areBuildingsDestroyed = true;
        }
        
        if (areSoldiersDead && areBuildingsDestroyed)
        {
            Debug.Log("Mission Complete");
        }
    }
}
