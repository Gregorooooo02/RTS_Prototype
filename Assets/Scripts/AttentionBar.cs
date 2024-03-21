using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AttentionBar : MonoBehaviour
{
    
    public int maximum;
    public int current;
    public Image mask;
    
    // Update is called once per frame
    void Update()
    {
        getCurrentFill();
    }

    public void updateAttention(int amount)
    {
        UnityEngine.Debug.Log("attention updated");
        current += amount;
    }

    
    public void getCurrentFill()
    {
        float fillAmount = (float)current / (float)maximum;
        mask.fillAmount = fillAmount;
    }

}
