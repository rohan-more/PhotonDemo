using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshManager : MonoBehaviour
{
    public static MeshManager Instance;
    
    private Dictionary<string, Mesh> idToMeshMap = new Dictionary<string, Mesh>();
    public List<string> meshNames;
    public List<Mesh> meshes;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        CreateMeshMap();
    }

    private void CreateMeshMap()
    {
        // Ensure the lists have the same count
        int count = Mathf.Min(meshNames.Count, meshes.Count);
        
        for (int i = 0; i < count; i++)
        {
            idToMeshMap[meshNames[i]] = meshes[i];
        }
    }
    
    public Mesh GetMeshByName(string name)
    {

        idToMeshMap.TryGetValue(name, out Mesh mesh);
        return mesh;
    }

}
