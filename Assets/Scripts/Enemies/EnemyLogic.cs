using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLogic : MonoBehaviour
{
    [SerializeField] float hp;
    [SerializeField] GameObject attentionBar;
    public HumanBehavior behavior;

    [SerializeField] float dmgTime;
    [SerializeField] Material normal;
    [SerializeField] Material damaged;
    [SerializeField] MeshRenderer mesh;

    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    public void dealDamage(float amount, Transform source)
    {
        hp = Mathf.Max(0, hp - amount);
        mesh.material = damaged;
        behavior.Alert(source);
        if (hp == 0)
        {
            if(attentionBar != null)attentionBar.GetComponent<AttentionBar>().updateAttention(10);
            Destroy(gameObject);
        }
        Invoke("backToNormal",dmgTime);
    }

    private void backToNormal()
    {
        mesh.material = normal;
    }
}
