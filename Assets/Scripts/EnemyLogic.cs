using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    [SerializeField] float hp;
    public HumanBehavior behavior;

    public void dealDamage(float amount, Transform source)
    {
        hp = Mathf.Max(0, hp - amount);
        behavior.Alert(source);
        if (hp == 0) Destroy(gameObject);
    }
}
