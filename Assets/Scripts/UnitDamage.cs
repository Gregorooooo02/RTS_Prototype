using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDamage : MonoBehaviour
{
    public float damage;
    public float atackSpeed;
    private float lastAtack = 0;

    private UnitMovement movement;

    private void Start()
    {
        movement = transform.parent.gameObject.GetComponent<UnitMovement>();
    }

    private void Update()
    {
        lastAtack += Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if(lastAtack >= atackSpeed && movement.atackMode)
        {
            if (other.gameObject.layer == 8)
            {
                other.GetComponent<EnemyLogic>().dealDamage(damage, transform.parent);
                lastAtack = 0;
            }
            else if(other.gameObject.layer == 10)
            {
                other.GetComponent<EnemyBuilding>().TakeDamage(damage);
                lastAtack = 0;
            }
        }  
    }
}
