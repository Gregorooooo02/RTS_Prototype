using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] double hp;
    [SerializeField] double armor = 1;

    private void Start()
    {
        UnitSelectionManager.Instance.allUnitsList.Add(gameObject);        
    }

    private void OnDestroy()
    {
        UnitSelectionManager.Instance.allUnitsList.Remove(gameObject);
        UnitSelectionManager.Instance.unitsSelected.Remove(gameObject);
    }

    public void TakeDamage(double damage)
    {
        hp -= damage / armor;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
