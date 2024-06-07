using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    private string targetLayerName = "Default";
    [SerializeField] private GameObject targetGameObject;
    [SerializeField] private PhotonView photonView;
    void Start()
    {
        int targetLayer = LayerMask.NameToLayer(targetLayerName);
        
        if (photonView.IsMine)
        {
            return;
        }
        
        if (targetLayer == -1)
        {
            Debug.LogError($"Layer '{targetLayerName}' does not exist.");
            return;
        }

        SetLayerRecursively(targetGameObject, targetLayer);
    }

    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (obj == null)
        {
            return;
        }

        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            if (child == null)
            {
                continue;
            }
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
}
