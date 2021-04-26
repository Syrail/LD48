using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    public GameObject goReference = null;

    int originaLayer;
    public void SetHighlight(bool active)
    {
        GameObject go = goReference != null ? goReference : gameObject;

        if (active)
        {
            originaLayer = go.layer;
            go.layer = LayerMask.NameToLayer("Highlighted");
        }
        else
        {
            go.layer = originaLayer;
        }
    }
}
