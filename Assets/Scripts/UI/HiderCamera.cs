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
    private Dictionary<Func<string, bool>, MeshName> meshMap;
    private void Start()
    {
        meshMap = new Dictionary<Func<string, bool>, MeshName>
        {
            { str => str.Contains("vase"), MeshName.VASE },
            { str => str.Contains("chair"), MeshName.CHAIR },
            { str => str.Contains("bathtub"), MeshName.BATHTUB },
            { str => str.Contains("sack_open"), MeshName.SACK_OPEN },
            { str => str.Contains("bird_house"), MeshName.BIRD_HOUSE },
            { str => str.Contains("barrel"), MeshName.BARREL },
        };
    }

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
                Mesh newMesh = hit.collider.gameObject.GetComponent<MeshFilter>().sharedMesh;

                foreach (var condition in meshMap)
                {
                    if (!condition.Key(newMesh.name))
                    {
                        continue;
                    }
                    Events.OnSelectedObject(_targetViewID, condition.Value);
                    break;
                }
            }
        }
    }
}