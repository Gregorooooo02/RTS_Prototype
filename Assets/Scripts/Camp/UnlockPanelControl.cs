using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UnlockPanelControl : MonoBehaviour
{
    [Header("Panel")] 
    [SerializeField] public GameObject unlockPanel;
    
    [Header("Buttons")]
    [SerializeField] public Button unlockBaracks;
    [SerializeField] public Button unlockHouse1;
    [SerializeField] public Button unlockHouse2;
    [SerializeField] public Button unlockHouse3;
    
    [Header("Buildings")]
    [SerializeField] public GameObject baracks;
    [SerializeField] public GameObject house1;
    [SerializeField] public GameObject house2;
    [SerializeField] public GameObject house3;
    
    private void Start()
    {
        unlockBaracks.onClick.AddListener(UnlockBaracks);
        unlockHouse1.onClick.AddListener(UnlockHouse1);
        unlockHouse2.onClick.AddListener(UnlockHouse2);
        unlockHouse3.onClick.AddListener(UnlockHouse3);
    }

    private void FixedUpdate()
    {
        if (!unlockBaracks.IsActive() && !unlockHouse1.IsActive() && !unlockHouse2.IsActive() && !unlockHouse3.IsActive())
        {
            unlockPanel.SetActive(false);
        }
    }

    public void UnlockBaracks()
    {
        Debug.Log("Unlocking baracks");
        baracks.SetActive(true);
        unlockBaracks.gameObject.SetActive(false);
    }
    
    public void UnlockHouse1()
    {
        Debug.Log("Unlocking house 1");
        house1.SetActive(true);
        unlockHouse1.gameObject.SetActive(false);
    }
    
    public void UnlockHouse2()
    {
        Debug.Log("Unlocking house 2");
        house2.SetActive(true);
        unlockHouse2.gameObject.SetActive(false);
    }
    
    public void UnlockHouse3()
    {
        Debug.Log("Unlocking house 3");
        house3.SetActive(true);
        unlockHouse3.gameObject.SetActive(false);
    }
}
