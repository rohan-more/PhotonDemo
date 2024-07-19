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

    public string shaderProperty = "_Thickness"; 
    public float hideValue = 0f;
    public float showValue = 0.005f;
    private List<Renderer> renderers = new();
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
        if (!photonView.IsMine)
        {
            return;
        }

        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Hideable"))
            {
                Renderer renderer = hit.collider.GetComponent<Renderer>();
                renderers.Add(renderer);

                foreach (var item in renderers)
                {
                    ToggleShaderProperty(item, showValue);
                }
            }
            else
            {
                foreach (var item in renderers)
                {
                    ToggleShaderProperty(item, hideValue);
                }
                renderers.Clear();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
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
    
    void ToggleShaderProperty(Renderer renderer, float value)
    {
        if (renderer == null)
        {
            return;
        }
        foreach (Material material in renderer.materials)
        {
            if (material.HasProperty(shaderProperty))
            {
                float newValue = value;
                material.SetFloat(shaderProperty, newValue);
            }
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