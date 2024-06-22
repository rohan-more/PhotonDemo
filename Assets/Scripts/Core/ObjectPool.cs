using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    //BATH_TUB, CHAIR, BIRD_HOUSE, VASE, GRAIN_SACK
    public enum ObjectType {CUBE, SPHERE, CAPSULE}
    [System.Serializable]
    public class Pool
    {
        public ObjectType tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<ObjectType, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        poolDictionary = new Dictionary<ObjectType, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject GetPooledObject(ObjectType tag)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}