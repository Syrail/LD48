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
    bool hasImpactedShip;

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
        rb.useGravity = false;
        hasImpactedShip = false;
    }

    // General behavior would be to just move on the forward vector
    void FixedUpdate()
    {
        if(shipInstance != null)
        {
            float distance = Vector3.Distance(shipInstance.transform.position, transform.position);
            if(distance > maxDistanceFromShip)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider target)
    {
        DamageZone dmgZone;
        if (!hasImpactedShip && target.gameObject.tag.Equals("DamageZone") == true && target.gameObject.TryGetComponent(out dmgZone))
        {
            hasImpactedShip = true;
            dmgZone.DealDamage(damage);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(hasImpactedShip)
        {
            Vector3 rev = collision.GetContact(0).normal;
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddForceAtPosition(rb.mass * 100f* rev, collision.GetContact(0).point);
        }
    }

    public void SetShip(GameObject ship, float maxDistance)
    {
        shipInstance = ship;
        maxDistanceFromShip = maxDistance;
    }
}
