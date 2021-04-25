using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//may change this to scriptable object? may not work since there will be actual behavior
public class SpawnableObject : MonoBehaviour
{
    public Vector2 speedRange = new Vector2(1.0f, 10.0f);
    public Vector2 angularSpeedRange = new Vector2(1.0f, 10.0f);

    float speed = 0.0f;
    Vector3 angularSpeed;
    Vector3 velocity;

    void Start()
    {
        speed = UnityEngine.Random.Range(speedRange.x, speedRange.y);
        angularSpeed = new Vector3(UnityEngine.Random.Range(angularSpeedRange.x, angularSpeedRange.y),
                                   UnityEngine.Random.Range(angularSpeedRange.x, angularSpeedRange.y),
                                   UnityEngine.Random.Range(angularSpeedRange.x, angularSpeedRange.y));

        velocity = transform.forward * speed;


    }
    // General behavior would be to just move on the forward vector
    void Update()
    {
        transform.position += velocity * Time.deltaTime;
    }
}
