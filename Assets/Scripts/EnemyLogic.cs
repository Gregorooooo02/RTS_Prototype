using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    [SerializeField] float hp;
    public LayerMask units;
    public float damageCooldown;
    private float timeSinceHit = 0;


    private void FixedUpdate()
    {
        timeSinceHit += Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7 && timeSinceHit >= damageCooldown) {
            hp = Mathf.Max(0,hp - other.GetComponent<Unit>().damage);
            if(hp == 0)
            {
                Destroy(gameObject.transform.parent.gameObject);
            }
            timeSinceHit = 0;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 7 && timeSinceHit >= damageCooldown) {
            hp = Mathf.Max(0, hp - other.GetComponent<Unit>().damage);
            if (hp == 0)
            {
                Destroy(gameObject.transform.parent.gameObject);
            }
            timeSinceHit = 0;
        }
    }
}
