using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPointHolder;
    public List<Transform> spawnPositions;  // List to hold fixed spawn positions
    public ObjectPool objectPool;  // Reference to the object pool
    public List<ObjectPool.ObjectType> objectTags;  // Tags of objects to spawn
    public float spawnInterval = 5f;  // Time interval between spawns
    private List<Transform> availablePositions;
    private void Start()
    {
        availablePositions = new List<Transform>(spawnPositions);
        foreach (Transform child in spawnPointHolder)
        {
            spawnPositions.Add(child);
        }

        foreach (var item in spawnPositions)
        {
           // SpawnObjectAtRandomPosition();
        }
    }

    private void SpawnObjectAtRandomPosition()
    {
        if (spawnPositions.Count == 0)
        {
            Debug.LogWarning("No spawn positions set in the inspector.");
            return;
        }

        if (objectTags.Count == 0)
        {
            Debug.LogWarning("No object tags set in the inspector.");
            return;
        }

        // Select a random position from the list
        int randomPositionIndex = Random.Range(0, spawnPositions.Count);
        Transform spawnPosition = spawnPositions[randomPositionIndex];

        // Select a random object tag from the list
        int randomTagIndex = Random.Range(0, objectTags.Count);
        ObjectPool.ObjectType tag = objectTags[randomTagIndex];

        // Get a pooled object by tag and activate it at the chosen position
        GameObject pooledObject = objectPool.GetPooledObject(tag);
        if (pooledObject != null)
        {
            pooledObject.transform.position = spawnPosition.position;
            pooledObject.transform.rotation = spawnPosition.rotation;
        }
    }
}