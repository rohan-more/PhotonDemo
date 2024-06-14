using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using Photon.Pun;
using UnityEngine;

public class HiderCamera : MonoBehaviour
{
    [SerializeField] private Transform _targetPlayer;
    [SerializeField] private vThirdPersonCamera _tpCamera;
    [SerializeField] private Camera _camera;
    public PhotonView photonView;
    private int _targetViewID;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!photonView.IsMine)
            {
                return;
            }
            DetectObject();
        }
    }

    public void SetTargetPlayer()
    {
        if (_targetPlayer == null)
        {
            _targetPlayer = _tpCamera.target;
            _targetViewID = _targetPlayer.gameObject.GetPhotonView().ViewID;
        }
    }

    private void DetectObject()
    {
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Hideable"))
            {
                Mesh newMesh = hit.collider.gameObject.GetComponent<MeshFilter>().mesh;
                
                string sendName = null;
                if (newMesh.name.Contains("Sphere"))
                {
                    sendName = "Sphere";
                }
                else if (newMesh.name.Contains("Cube"))
                {
                    sendName = "Cube";
                }
                else
                {
                    sendName = "Capsule";
                }
                Events.OnSelectedObjectType(_targetViewID, sendName);
            }
        }
    }
}