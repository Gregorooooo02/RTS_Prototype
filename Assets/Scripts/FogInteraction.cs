using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class FogInteraction : MonoBehaviour
{
    public bool onlyCheckOnMove = true;
    public float sightRange;
    public float interval;

    public Vector3 previousPositionSoft;
    private Vector3 previousPositionHard;

    void Start()
    {
        while (SoftFog.softFog == null) { };
        SoftFog.softFog.revelers.Add(this);
        HardFog.hardFog.MakeHole(new Vector2(transform.position.x, transform.position.z), sightRange);
        previousPositionHard = transform.position;
        previousPositionSoft = transform.position;
        StartCoroutine(CheckFog(interval));
    }

    private void OnDestroy()
    {
        SoftFog.softFog.revelers.Remove(this);
    }

    private IEnumerator CheckFog(float checkInterval)
    {
        while (true)
        {
            if (!onlyCheckOnMove)
            {
                HardFog.hardFog.MakeHole(new Vector2(transform.position.x, transform.position.z), sightRange);
                
            } else
            {
                if (previousPositionHard != transform.position)
                {
                    HardFog.hardFog.MakeHole(new Vector2(transform.position.x, transform.position.z), sightRange);
                }
            }
            previousPositionHard = transform.position;
            yield return new WaitForSeconds(checkInterval);
        }
    }
}
