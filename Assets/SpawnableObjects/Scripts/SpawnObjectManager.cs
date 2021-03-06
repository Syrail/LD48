using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectManager : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnObjectChance
    {
        public SpawnableObject spawnableObject;
        public float weight;
    }

    public SpawnObjectChance[] spawnableObjects;
    public Vector2 spawnTimerRange = new Vector2(0.5f, 5.0f);
    public GameObject ship;
    public GameplayEventListener gameplayEventListener;
    public float distanceFromShip = 20.0f;
    public float maxDistanceFromShip = 40.0f;
    public float reduceSpawnRatePerIteration = 0.5f;

    public Vector2 spawningTimeDuration = new Vector2(15.0f, 30.0f);
    public Vector2 restTimeDuration = new Vector2(5.0f, 15.0f);

    public Vector2 spawnSize = new Vector2(5.0f, 2.0f);

    float totalWeight = 0.0f;
    float maxSpawnRate = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        AudioListener.volume = 0.5f;
        CalculateTotalWeight();
        maxSpawnRate = spawnTimerRange.y;
        StartCoroutine(ManageSpawnTimers());        
    }

    IEnumerator ManageSpawnTimers()
    {
        while(true)
        {
            yield return SpawnObject();
            float sleepTime = UnityEngine.Random.Range(restTimeDuration.x, restTimeDuration.y);
            Debug.Log("Spawner is sleeping for " + sleepTime);
            yield return new WaitForSeconds(sleepTime);
        }
    }

    IEnumerator SpawnObject()
    {
        Debug.Log("Spawning Objects");
        float duration = UnityEngine.Random.Range(spawningTimeDuration.x, spawningTimeDuration.y);
        float currentTime = 0.0f;
        //for now, spawn 20 units away from the ship
        while (true)
        {
            int objectIndex = SelectGameObjectToSpawn();
            float maxDistance = distanceFromShip;
            bool selfSpawnDistance = false;
            if (spawnableObjects[objectIndex].spawnableObject.distanceFromShip > 0.0f && !Mathf.Approximately(0.0f, spawnableObjects[objectIndex].spawnableObject.distanceFromShip))
            {
                selfSpawnDistance = true;
                maxDistance = spawnableObjects[objectIndex].spawnableObject.distanceFromShip;
            }
            Vector3 spawnPosition = ship.transform.position + (ship.transform.forward * maxDistance);
            spawnPosition.x += UnityEngine.Random.Range(-spawnSize.x, spawnSize.x);
            spawnPosition.y += UnityEngine.Random.Range(-spawnSize.y, spawnSize.y);          
            

            SpawnableObject newObject = Instantiate(spawnableObjects[objectIndex].spawnableObject, spawnPosition, Quaternion.LookRotation(-ship.transform.forward, ship.transform.up));
            float scale = UnityEngine.Random.Range(newObject.scaleRange.x, newObject.scaleRange.y);
            newObject.transform.localScale = new Vector3(scale, scale, scale);
            float maxFromShip = selfSpawnDistance ? (maxDistance * 5.0f) : maxDistanceFromShip;
            newObject.SetShip(ship, maxFromShip);
            newObject.SetEventListener(gameplayEventListener);
            float waitTime = UnityEngine.Random.Range(spawnTimerRange.x, maxSpawnRate);
            currentTime += waitTime;
            if(currentTime > duration)
            {
                yield break;
            }
            yield return new WaitForSeconds(waitTime);
        }

        maxSpawnRate -= reduceSpawnRatePerIteration;
        if(maxSpawnRate < spawnTimerRange.x)
        {
            maxSpawnRate = spawnTimerRange.x;
        }
    }

    void CalculateTotalWeight()
    {
        totalWeight = 0.0f;
        for(int i=0; i< spawnableObjects.Length; ++i)
        {
            totalWeight += spawnableObjects[i].weight;
        }
    }

    int SelectGameObjectToSpawn()
    {
        float randomNumber = UnityEngine.Random.Range(0.0f, totalWeight);
        float currentWeight = 0.0f;
        for (int i = 0; i < spawnableObjects.Length; ++i)
        {
            currentWeight += spawnableObjects[i].weight;
            if (randomNumber < currentWeight || Mathf.Approximately(randomNumber, spawnableObjects[i].weight))
                return i;

            
        }
        return 0;
    }

    public void ResetVariables()
    {
        maxSpawnRate = spawnTimerRange.y;
    }
}
