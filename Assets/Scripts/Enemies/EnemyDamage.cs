using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float atackSpeed;
    private float lastAtack = 0;

    private HumanBehavior behavior;

    // Start is called before the first frame update
    void Start()
    {
        behavior = transform.parent.gameObject.GetComponent<Soldier>();
    }

    // Update is called once per frame
    void Update()
    {
        lastAtack += Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if(lastAtack >= atackSpeed && behavior.currentState == HumanBehavior.HumanStates.ATACKING)
        {
            if(other.gameObject.layer == 7)
            {
                other.GetComponent<Unit>().TakeDamage(damage);
                lastAtack = 0;
            }
        }
    }
}
