using System;
using System.Collections.Generic;
using Core;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Serialization;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    [SerializeField] private MeshName[] meshTypes; // Assign these in the inspector
    [SerializeField] private int poolSize = 10; // Number of objects to pre-instantiate per type

    private Dictionary<string, List<GameObject>> pools;
    private Dictionary<string, int> currentIndex;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        meshTypes = MeshManager.Instance.meshConfig.GetMeshList();
    }

    private void OnEnable()
    {
        InitializePools();
    }

    void InitializePools()
    {
        pools = new Dictionary<string, List<GameObject>>();
        currentIndex = new Dictionary<string, int>();

        foreach (var objectType in meshTypes)
        {
            List<GameObject> pool = new List<GameObject>();
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = CreateNewPooledObject(objectType);
                obj.SetActive(false);
                pool.Add(obj);
            }
            pools.Add(objectType.ToString().ToLower(), pool);
            currentIndex.Add(objectType.ToString().ToLower(), 0);
        }
    }

    GameObject CreateNewPooledObject(MeshName meshName)
    {
        // Create a new GameObject
        GameObject newGameObject = new GameObject(meshName.ToString().ToLower());
        newGameObject.transform.SetParent(this.transform);
        // Add and configure the MeshFilter component
        MeshFilter meshFilter = newGameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = MeshManager.Instance.GetMesh(meshName);

        // Add and configure the MeshRenderer component
        MeshRenderer meshRenderer = newGameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = MeshManager.Instance.GetMaterial(meshName);
        return newGameObject;
    }

    public GameObject GetPooledObject(string objectType)
    {
        if (!pools.ContainsKey(objectType))
        {
            Debug.LogError($"Object type {objectType} not found in pool.");
            return null;
        }

        List<GameObject> pool = pools[objectType];
        int index = currentIndex[objectType];

        for (int i = 0; i < pool.Count; i++)
        {
            int poolIndex = (index + i) % pool.Count;
            if (!pool[poolIndex].activeInHierarchy)
            {
                currentIndex[objectType] = poolIndex;
                return pool[poolIndex];
            }
        }

        // If no inactive object is found, optionally expand the pool
        GameObject obj = CreateNewPooledObject(meshTypes[System.Array.FindIndex(meshTypes, ot => ot.ToString().ToLower() == objectType)]);
        obj.SetActive(false);
        pool.Add(obj);
        return obj;
    }

    public void DeactivateAllPooledObjects()
    {
        foreach (var pool in pools.Values)
        {
            foreach (var obj in pool)
            {
                obj.SetActive(false);
            }
        }
    }
}