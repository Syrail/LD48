using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShot : MonoBehaviour
{
    GameObject currentTarget = null;
    GameObject origin = null;
    public float lifetime;

    public void SetOrigin(GameObject origin)
    {
        this.origin = origin;
    }

    public void SetTarget(GameObject target)
    {
        currentTarget = target;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime < 0f) {
            Destroy(currentTarget);
            Destroy(gameObject);
        }
        LineRenderer lr;
        if (TryGetComponent<LineRenderer>(out lr))
        {
            if(origin != null && origin.activeSelf)
            {
                lr.SetPosition(0, origin.transform.position);
            }

            
            if (currentTarget != null && currentTarget.activeSelf)
            {
                lr.SetPosition(1, currentTarget.transform.position);
            }
        }
    }
}
