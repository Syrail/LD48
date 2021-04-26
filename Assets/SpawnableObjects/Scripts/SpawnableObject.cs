using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//may change this to scriptable object? may not work since there will be actual behavior
public class SpawnableObject : MonoBehaviour
{
    public Vector2 speedRange = new Vector2(1.0f, 10.0f);
    public Vector2 angularSpeedRange = new Vector2(1.0f, 10.0f);
    public Vector2 scaleRange = new Vector2(0.6f, 1.0f);
    public int damage = 0;
    public int energyValue = 0;
    public int resourceValue = 0;
    public float distanceFromShip = 0;
    public bool isHostile;
    public float speedBoost = 0;

    float speed = 0.0f;
    Vector3 angularSpeed;
    Vector3 velocity;
    GameObject shipInstance = null;
    GameplayEventListener gameplayEventListener = null;
    float maxDistanceFromShip = 0.0f;
    bool hasImpactedShip;
    bool isTargetLocked;


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
        isTargetLocked = false;
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
        ShipMotion motion = shipInstance.GetComponent<ShipMotion>();
        bool allowDamage = motion != null ? motion.IsImmune() : true;

        if (!hasImpactedShip && target.gameObject.tag.Equals("DamageZone") == true && target.gameObject.TryGetComponent(out dmgZone))
        {
            if (allowDamage)
            {
                hasImpactedShip = dmgZone.DealDamage(damage);
            }
        }

        //we are checking for the turret (fr the ring), since it looks like a good collider, but this is just hacky
        if (!hasImpactedShip && !isHostile && target.gameObject.tag.Equals("Turret") == true && motion != null)
        {
            motion.BoostSpped(speedBoost);
        }

        if (!hasImpactedShip && !isTargetLocked && target.gameObject.tag.Equals("Turret"))
        {
            AutoTurret turret;
            if(isHostile && target.gameObject.TryGetComponent(out turret)) {
                if (turret.NotifyTargetInRange(gameObject))
                {
                    //set target locked state 
                    isTargetLocked = true;

                    //grant resources based on type
                    gameplayEventListener.addEnergy(energyValue);
                    gameplayEventListener.addResources(resourceValue);
                }
            }
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if(hasImpactedShip)
        {
            Destroy(gameObject);
        }
    }

    public void SetShip(GameObject ship, float maxDistance)
    {
        shipInstance = ship;
        maxDistanceFromShip = maxDistance;
    }

    public void SetEventListener(GameplayEventListener eventListener)
    {
        gameplayEventListener = eventListener;
    }
}
