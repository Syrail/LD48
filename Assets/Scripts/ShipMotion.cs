using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMotion : MonoBehaviour
{

    public float Speed { get; set; }
       
    public float[] thrusterPower;
    public Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {

        thrusterPower = new float[4] { 0f, 0f, 0f, 0f };
        velocity = Vector3.forward * Speed;
    }

    public void EngageThruster(int thrusterIndex)
    {
        thrusterPower[thrusterIndex] += 1.0f;
    }


    private void FixedUpdate()
    {
        Quaternion thrustRotation = Quaternion.Euler(thrusterPower[1] - thrusterPower[0], thrusterPower[3] - thrusterPower[2], 0.0f);
        for(int i = 0; i < thrusterPower.Length; i++)
        {
            thrusterPower[i] = Mathf.Lerp(thrusterPower[i], 0.0f, 0.05f);
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation*thrustRotation, Time.fixedDeltaTime);
        velocity += transform.forward * Time.fixedDeltaTime;
        velocity.Normalize();
        transform.position += Speed *Time.fixedDeltaTime*velocity;
    }


}
