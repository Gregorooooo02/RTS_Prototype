using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionGameManager : MonoBehaviour
{
    [SerializeField] public GameObject soldiers;
    [SerializeField] public BuildingManager buildingManager;

    [SerializeField] public TextMeshProUGUI firstTask;
    [SerializeField] public TextMeshProUGUI secondTask;
    [SerializeField] public TextMeshProUGUI puzzleCount;

    [SerializeField] public Canvas missionCanvas;
    [SerializeField] public Canvas endCanvas;
    
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

        firstTask.text = "Destroy buildings: " + numberOfBuildings;
        secondTask.text = "Eliminate humans: " + numberOfSoldiers;
        puzzleCount.text = "Puzzle count: " + PuzzleCollector.puzzleCount + "/9";
        
        if (numberOfSoldiers <= 0)
        {
            areSoldiersDead = true;
        }
        
        if (numberOfBuildings <= 0)
        {
            areBuildingsDestroyed = true;
        }
        
        if (areBuildingsDestroyed && areSoldiersDead)
        {
            missionCanvas.gameObject.SetActive(false);
            endCanvas.gameObject.SetActive(true);
        }
    }
    
    public void OnEndButtonClick()
    {
        SceneManager.LoadScene("CampScene", LoadSceneMode.Single);
    }
}
