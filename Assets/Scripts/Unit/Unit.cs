using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    [SerializeField] double maxHp;
    public double hp;
    [SerializeField] double armor = 1;

    public GameObject healthBarUI;
    public Slider healthBar;

    private void Start()
    {
        hp = maxHp;
        UnitSelectionManager.Instance.allUnitsList.Add(gameObject);  
        healthBarUI.SetActive(false);
        healthBar.value = CalculateHealth();
    }
    
    private void Update()
    {
        healthBar.value = CalculateHealth();

        if (hp < maxHp)
        {
            healthBarUI.SetActive(true);
        }

        if (hp > maxHp)
        {
            hp = maxHp;
        }
    }

    float CalculateHealth()
    {
        return (float)(hp / maxHp);
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
