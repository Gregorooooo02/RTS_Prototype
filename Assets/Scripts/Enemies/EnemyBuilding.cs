using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBuilding : MonoBehaviour
{
    [SerializeField] double health;
    [SerializeField] double armor;
    [SerializeField] private BuildingManager _buildingManager;
    
    public void TakeDamage(double damage)
    {
        health -= damage / armor;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        health = 100;
        armor = 1;
        //add to list of all buildings
        _buildingManager.allBuildingsList.Add(gameObject);

    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            TakeDamage(10);
        }
    }
}