using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMotion : MonoBehaviour
{

    public Quaternion TargetAttitude { get; set; }
    public float Speed { get; set; }
    private float timeToAttitude;

    // Start is called before the first frame update
    void Start()
    {
        timeToAttitude = 0.0f;
        Speed = 4.0f;
        TargetAttitude = GetComponent<Transform>().rotation;
    }


    void RotateHeading(Quaternion rot)
    {
        TargetAttitude = TargetAttitude * rot;
        timeToAttitude += 1.0f;
    }


    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, TargetAttitude, Time.fixedDeltaTime/timeToAttitude);
        transform.position += Speed *Time.fixedDeltaTime*transform.forward;
        timeToAttitude -= Time.fixedDeltaTime;
    }


}
