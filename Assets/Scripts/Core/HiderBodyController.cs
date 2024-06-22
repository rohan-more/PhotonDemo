using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

namespace Core
{
    public class HiderBodyController : MonoBehaviour
    {
        [SerializeField] private MeshFilter _playerMesh;
        [SerializeField] private MeshRenderer _playerMeshRenderer;
        [SerializeField] private PhotonView _photonView;
        [SerializeField] private Mesh cube;
        [SerializeField] private Mesh capsule;
        [SerializeField] private Mesh sphere;
        private void OnEnable()
        {
            Events.SelectedObjectType += SwapMesh;
            Events.SelectedObject += SwapMesh;
        }

        private void OnDisable()
        {
            Events.SelectedObjectType -= SwapMesh;
            Events.SelectedObject -= SwapMesh;
        }
        
        [PunRPC]
        void RPC_PropChangeModel(int targetPropID, string meshName)
        {
            PhotonView targetPV = PhotonView.Find(targetPropID);

            if (gameObject.GetPhotonView().ViewID != targetPropID)
            {
                return;
            }
            //_playerMesh.mesh = GUIDManager.Instance.GetMeshByID(meshID);
            _playerMesh.mesh = MeshManager.Instance.GetMeshByName(meshName);
            _playerMeshRenderer.material = MeshManager.Instance.GetMaterialByName(meshName);
        }
        

        private void SwapMesh(int viewID, string meshName)
        {
            if (gameObject.GetPhotonView().ViewID != viewID)
            {
                return;
            }
            //_playerMesh.mesh = GUIDManager.Instance.GetMeshByID(meshID);
            _playerMesh.mesh = MeshManager.Instance.GetMeshByName(meshName);
            _playerMeshRenderer.material = MeshManager.Instance.GetMaterialByName(meshName);
            _photonView.RPC("RPC_PropChangeModel", RpcTarget.OthersBuffered, viewID, meshName);
        }
        
        private void SwapMesh(int viewID, MeshName meshName)
        {
            if (gameObject.GetPhotonView().ViewID != viewID)
            {
                return;
            }
            //_playerMesh.mesh = GUIDManager.Instance.GetMeshByID(meshID);
            _playerMesh.mesh = MeshManager.Instance.GetMesh(meshName);
            _playerMeshRenderer.material = MeshManager.Instance.GetMaterial(meshName);
            _photonView.RPC("RPC_PropChangeModel", RpcTarget.OthersBuffered, viewID, meshName.ToString().ToLower());
        }
        
    }
}

