using System;
using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;

public class GUIDManager : MonoBehaviour
{
    public static GUIDManager Instance;

    private Dictionary<string, string> sceneObjectGUIDs = new Dictionary<string, string>();
    private Dictionary<string, Mesh> idToMeshMap = new Dictionary<string, Mesh>();
    [SerializeField] private PhotonView _photonView;
    [SerializeField] private NetworkGUID[] sceneObjects;
    [SerializeField] private Mesh cube, capsule, sphere;

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

    public void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            AssignGUIDsToSceneObjects();
        }
    }

    [PunRPC]
    private void SyncGUID(string objectName, string guid)
    {
        sceneObjectGUIDs[objectName] = guid;
        GameObject obj = GameObject.Find(objectName);
        if (obj != null)
        {
     
            NetworkGUID networkGUID = obj.GetComponent<NetworkGUID>();
            if (networkGUID != null)
            {
                networkGUID.SetGUID(guid);
            }
            MeshFilter meshFilter = obj.GetComponent<MeshFilter>();
            Mesh mesh = meshFilter.sharedMesh;
                
            if (mesh.name.Contains("Sphere"))
            {
                mesh = sphere;
            }
            else if (mesh.name.Contains("Cube"))
            {
                mesh = cube;
            }
            else
            {
                mesh = capsule;
            }
            
            idToMeshMap.Add(guid, mesh);
        }
    }

    public Mesh GetMeshByID(string id)
    {
        idToMeshMap.TryGetValue(id, out Mesh mesh);
        return mesh;
    }

    private void AssignGUIDsToSceneObjects()
    {
        

        NetworkGUID[] sceneObjects = FindObjectsOfType<NetworkGUID>();
        foreach (var obj in sceneObjects)
        {
            string objectName = obj.gameObject.name;
            GameObject swap = obj.gameObject;
            if (!sceneObjectGUIDs.ContainsKey(objectName))
            {
                MeshFilter meshFilter = swap.GetComponent<MeshFilter>();
                Mesh mesh = meshFilter.sharedMesh;
                string newGUID = System.Guid.NewGuid().ToString();
                sceneObjectGUIDs[objectName] = newGUID;
                obj.SetGUID(newGUID);
                _photonView.RPC("SyncGUID", RpcTarget.OthersBuffered, objectName, newGUID);
                idToMeshMap.Add(newGUID, mesh);
            }
        }
    }
}