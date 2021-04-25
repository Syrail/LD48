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
    public float distanceFromShip = 20.0f;
    public float maxDistanceFromShip = 40.0f;

    public Vector2 spawnSize = new Vector2(5.0f, 2.0f);

    float totalWeight = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        AudioListener.volume = 0.5f;
        CalculateTotalWeight();
        StartCoroutine(SpawnObject());
    }

    IEnumerator SpawnObject()
    {
        //for now, spawn 20 units away from the ship
        while (true)
        {
            Vector3 spawnPosition = ship.transform.position + (ship.transform.forward * distanceFromShip);
            spawnPosition.x += UnityEngine.Random.Range(-spawnSize.x, spawnSize.x);
            spawnPosition.y += UnityEngine.Random.Range(-spawnSize.y, spawnSize.y);
            int objectIndex = SelectGameObjectToSpawn();

            SpawnableObject newObject = Instantiate(spawnableObjects[objectIndex].spawnableObject, spawnPosition, Quaternion.LookRotation(-ship.transform.forward, ship.transform.up));
            newObject.SetShip(ship, maxDistanceFromShip);
            yield return new WaitForSeconds(UnityEngine.Random.Range(spawnTimerRange.x, spawnTimerRange.y));
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
}
