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
    
    [SerializeField] float dmgTime;
    private Material normal;
    [SerializeField] Material damaged;
    private MeshRenderer mesh;

    private void Start()
    {
        hp = maxHp;
        mesh = GetComponent<MeshRenderer>();
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
        normal = mesh.material;
        mesh.material = damaged;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
        Invoke("backToNormal", dmgTime);
    }

    private void backToNormal()
    {
        mesh.material = normal;
    }
}
