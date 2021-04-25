using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//may change this to scriptable object? may not work since there will be actual behavior
public class SpawnableObject : MonoBehaviour
{
    public Vector2 speedRange = new Vector2(1.0f, 10.0f);
    public Vector2 angularSpeedRange = new Vector2(1.0f, 10.0f);
    public int damage = 0;

    float speed = 0.0f;
    Vector3 angularSpeed;
    Vector3 velocity;
    GameObject shipInstance = null;
    float maxDistanceFromShip = 0.0f;

    void Start()
    {
        speed = UnityEngine.Random.Range(speedRange.x, speedRange.y);
        angularSpeed = new Vector3(UnityEngine.Random.Range(angularSpeedRange.x, angularSpeedRange.y),
                                   UnityEngine.Random.Range(angularSpeedRange.x, angularSpeedRange.y),
                                   UnityEngine.Random.Range(angularSpeedRange.x, angularSpeedRange.y));

        velocity = transform.forward * speed;
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = velocity;
        rb.angularVelocity = angularSpeed;


    }

    // General behavior would be to just move on the forward vector
    void FixedUpdate()
    {
        /*CustomBehavior();
        transform.Rotate(angularSpeed * Time.fixedDeltaTime);
        transform.position += velocity * Time.fixedDeltaTime;*/
        if(shipInstance != null)
        {
            float distance = Vector3.Distance(shipInstance.transform.position, transform.position);
            if(distance > maxDistanceFromShip)
            {
                Destroy(gameObject);
            }
        }

    }

    public void CustomBehavior()
    {
    }

    /*void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.tag.Equals("DespawnWall") == true)
        {
            Destroy(gameObject);
        }
    }*/

    public void SetShip(GameObject ship, float maxDistance)
    {
        shipInstance = ship;
        maxDistanceFromShip = maxDistance;
    }
}
