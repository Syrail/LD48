using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMotion : MonoBehaviour
{

    enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    public float Speed;

    public float forceToAdd = 10.0f;
    public float forceToAddAngular = 10.0f;
    public Vector3 velocity;
    public Vector3 angularVelocity = new Vector3();
    public float drag = 0.5f;
    public float angularDrag = 0.8f;
    public bool rotate = false;

    float[] thrusterPower;
    // Start is called before the first frame update
    void Start()
    {

        thrusterPower = new float[4] { 0f, 0f, 0f, 0f };
        velocity = Vector3.forward * Speed;
    }

    public void EngageThruster(int thrusterIndex)
    {
        thrusterPower[thrusterIndex] += rotate ? forceToAddAngular : forceToAdd;
    }


    private void FixedUpdate()
    {
        /*Quaternion thrustRotation = Quaternion.Euler(thrusterPower[1] - thrusterPower[0], thrusterPower[3] - thrusterPower[2], 0.0f);
        for(int i = 0; i < thrusterPower.Length; i++)
        {
            thrusterPower[i] = Mathf.Lerp(thrusterPower[i], 0.0f, 0.05f);
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation*thrustRotation, Time.fixedDeltaTime);*/

        Vector3 acceleration = CalculateTotalAcceleration();
        if (rotate)
        {
            angularVelocity += acceleration - (drag * angularVelocity);
            if (Mathf.Approximately(velocity.sqrMagnitude, 0.001f))
            {
                angularVelocity = new Vector3(0.0f, 0.0f, 0.0f);
            }
            transform.Rotate(new Vector3(0.0f, angularVelocity.x, 0.0f));
        }
        else
        {
            velocity += acceleration - (drag * velocity);
            if (Mathf.Approximately(velocity.sqrMagnitude, 0.001f))
            {
                velocity = new Vector3(0.0f, 0.0f, 0.0f);
            }
            transform.position += Time.fixedDeltaTime * velocity;
        }
        //velocity = transform.forward * Speed;
        
    }

    Vector3 CalculateTotalAcceleration()
    {
        Vector3 acceleration = new Vector3(0.0f,0.0f,0.0f);
        for(int i=0; i< thrusterPower.Length; ++i)
        {
            switch ((Direction)i)
            {
                case Direction.UP:
                    //shipMotion.EngageThruster(interaction.slot);
                    break;
                case Direction.DOWN:
                    break;
                case Direction.LEFT:
                    acceleration.x -= thrusterPower[i];
                    break;
                case Direction.RIGHT:
                    acceleration.x += thrusterPower[i];
                    break;
            }
            thrusterPower[i] = 0.0f;
        }
        return acceleration;
    }
}
