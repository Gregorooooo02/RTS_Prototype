using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class FogInteraction : MonoBehaviour
{
    public HardFog Fog;
    public HardFog SecondaryFog;

    public bool onlyCheckOnMove = true;
    public float sightRange;
    public float interval;

    private Vector3 previousPosition;


    // Start is called before the first frame update
    void Start()
    {
        previousPosition = transform.position;
        StartCoroutine(CheckFog(interval));
    }

    private IEnumerator CheckFog(float checkInterval)
    {
        while (true)
        {
            if (!onlyCheckOnMove)
            {
                Fog.MakeHole(new Vector2(transform.position.x, transform.position.z), sightRange);
                
            } else
            {
                if (previousPosition != transform.position)
                {
                    Fog.MakeHole(new Vector2(transform.position.x, transform.position.z), sightRange);
                }
            }
            SecondaryFog.MakeHole(new Vector2(previousPosition.x, previousPosition.z), sightRange, true);
            SecondaryFog.MakeHole(new Vector2(transform.position.x, transform.position.z), sightRange);
            previousPosition = transform.position;
            yield return new WaitForSeconds(checkInterval);
        }
    }
}
