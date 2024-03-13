using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLogic : MonoBehaviour
{
    [SerializeField] float hp;
    [SerializeField] GameObject attentionBar;
    public HumanBehavior behavior;

    public void dealDamage(float amount, Transform source)
    {
        hp = Mathf.Max(0, hp - amount);
        behavior.Alert(source);
        if (hp == 0)
        {
            attentionBar.GetComponent<AttentionBar>().updateAttention(10);
            Destroy(gameObject);
            
        }
    }
}
