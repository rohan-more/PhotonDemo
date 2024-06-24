using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class MeshManager : MonoBehaviour
{
    public static MeshManager Instance;
    public MeshConfig meshConfig;
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
        meshConfig.CreateData();
    }
    
    public Mesh GetMesh(MeshName meshName)
    {
        return meshConfig.GetMesh(meshName);
    }
    
    public Material GetMaterial(MeshName meshName)
    {
        return meshConfig.GetMaterial(meshName);
    }

    
    public Mesh GetMeshByName(string name)
    {
        Enum.TryParse(name, true, out MeshName meshName);
        return meshConfig.GetMesh(meshName);
    }
    
    public Material GetMaterialByName(string name)
    {
        Enum.TryParse(name, true, out MeshName meshName);
        return meshConfig.GetMaterial(meshName);
    }

}
