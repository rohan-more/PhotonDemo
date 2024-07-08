using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCameraController : MonoBehaviour
{
    public Transform playerTransform;
    private void LateUpdate()
    {
        if (playerTransform == null)
        {
            return;
        }
        
        Vector3 newPos = playerTransform.position;
        var cameraTransform = this.transform;
        newPos.y = this.transform.position.y;
        this.transform.position = newPos;
        this.transform.rotation = Quaternion.Euler(90f, playerTransform.eulerAngles.y, 0f);
    }
}
