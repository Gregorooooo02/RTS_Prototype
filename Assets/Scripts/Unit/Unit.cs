using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] double hp;
    [SerializeField] double armor = 1;

    [SerializeField] float dmgTime;
    private Material normal;
    [SerializeField] Material damaged;
    private MeshRenderer mesh;

    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
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
