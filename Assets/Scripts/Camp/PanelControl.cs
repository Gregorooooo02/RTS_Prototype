using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelControl : MonoBehaviour
{
    [SerializeField] public GameObject missionSelectPanel;
    [SerializeField] public Button exitButton;
    [SerializeField] public Button firstMissionButton;

    private void Start()
    {
        exitButton.onClick.AddListener(ExitPanel);
        firstMissionButton.onClick.AddListener(StartFirstMission);
    }
    
    private void ExitPanel()
    {
        missionSelectPanel.SetActive(false);
    }
    
    private void StartFirstMission()
    {
        Debug.Log("Starting first mission");
        SceneManager.LoadScene("TestMission", LoadSceneMode.Single);
    }
}
