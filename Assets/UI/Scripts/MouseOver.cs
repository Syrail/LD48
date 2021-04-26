using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOver : MonoBehaviour
{
    public float distanceFromCamera = 10.0f;
    Ray ray;
    RaycastHit hit;
    Highlight currentObject = null;
    

    void Update()
    {
        ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        if (Physics.Raycast(ray, out hit, distanceFromCamera,  LayerMask.GetMask("ControlPanelButtons")))
        {
            Highlight objectToHighlight = hit.collider.GetComponent<Highlight>();
            if(objectToHighlight && objectToHighlight != currentObject)
            {
                if(currentObject != null)
                {
                    currentObject.SetHighlight(false);
                }
                currentObject = objectToHighlight;
                currentObject.SetHighlight(true);
            }
        }
        else if (currentObject != null)
        {
            currentObject.SetHighlight(false);
            currentObject = null;
        }
    }
}
