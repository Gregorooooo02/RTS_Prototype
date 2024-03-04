using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisibilty : MonoBehaviour
{
    public MeshRenderer mesh;
    public float checkInterval;
    [SerializeField]private bool fogAffected = true;

    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        while (SoftFog.softFog == null) { };
        StartCoroutine(checkVisibility(checkInterval));
    }

    private IEnumerator checkVisibility(float interval)
    {
        while (true)
        {
            if (fogAffected)
            {
                bool result = SoftFog.softFog.isVisible(transform.position);
                if (mesh.enabled != result) mesh.enabled = result;
            }
            yield return new WaitForSeconds(interval);
        }
    }
}
