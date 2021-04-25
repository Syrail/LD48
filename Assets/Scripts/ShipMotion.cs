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
        RIGHT,
        STRAFE_LEFT,
        STRAFE_RIGHT
    }

    public float Speed;

    public float forceToAdd = 10.0f;
    public float forceToAddAngular = 10.0f;
    public Vector3 velocity;
    public Vector3 angularVelocity = new Vector3();
    public float drag = 0.5f;
    public float angularDrag = 0.8f;

    float[] thrusterPower;
    Vector3 linearAcceleration = new Vector3();
    Vector3 angularAcceleration = new Vector3();
    // Start is called before the first frame update
    void Start()
    {

        thrusterPower = new float[4] { 0f, 0f, 0f, 0f };
        velocity = Vector3.forward * Speed;
    }

    public void EngageThruster(int thrusterIndex)
    {
        thrusterPower[thrusterIndex] += ((Direction)thrusterIndex == Direction.LEFT || (Direction)thrusterIndex == Direction.RIGHT) ? forceToAddAngular : forceToAdd;
    }


    private void FixedUpdate()
    {
        /*Quaternion thrustRotation = Quaternion.Euler(thrusterPower[1] - thrusterPower[0], thrusterPower[3] - thrusterPower[2], 0.0f);
        for(int i = 0; i < thrusterPower.Length; i++)
        {
            thrusterPower[i] = Mathf.Lerp(thrusterPower[i], 0.0f, 0.05f);
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation*thrustRotation, Time.fixedDeltaTime);*/

        CalculateTotalAcceleration();
        angularVelocity += angularAcceleration - (angularDrag * angularVelocity);
        if (Mathf.Approximately(angularVelocity.sqrMagnitude, 0.001f))
        {
            angularVelocity = new Vector3(0.0f, 0.0f, 0.0f);
        }
        transform.Rotate(new Vector3(0.0f, angularVelocity.x, 0.0f));

        velocity += (linearAcceleration) - (drag * velocity);
        if (Mathf.Approximately(velocity.sqrMagnitude, 0.001f))
        {
            velocity = new Vector3(0.0f, 0.0f, 0.0f);
        }
        transform.position += (Time.fixedDeltaTime * velocity) + (Time.fixedDeltaTime * transform.forward * Speed);
        //
        //reset accelerations for this frame
       
    }

    void CalculateTotalAcceleration()
    {
        linearAcceleration = new Vector3(0.0f, 0.0f, 0.0f);
        angularAcceleration = new Vector3(0.0f, 0.0f, 0.0f);
        for (int i=0; i< thrusterPower.Length; ++i)
        {
            switch ((Direction)i)
            {
                case Direction.UP:
                    linearAcceleration += transform.up*thrusterPower[i];
                    break;
                case Direction.DOWN:
                    linearAcceleration -= transform.up*thrusterPower[i];
                    break;
                case Direction.LEFT:
                    angularAcceleration.x -= thrusterPower[i];
                    break;
                case Direction.RIGHT:
                    angularAcceleration.x += thrusterPower[i];
                    break;
                case Direction.STRAFE_LEFT:
                    linearAcceleration -= transform.right*thrusterPower[i];
                    break;
                case Direction.STRAFE_RIGHT:
                    linearAcceleration += transform.right * thrusterPower[i];
                    break;
            }
            thrusterPower[i] = 0.0f;
        }
    }
}
