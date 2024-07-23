using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class RotateProp : MonoBehaviour
    {
        public float rotationAngle = 90f; // Angle to rotate

        void Update()
        {
            if (Input.GetKey(KeyCode.Tab))
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
                {
                    Rotate(Vector3.up, rotationAngle);
                }

                if (Input.GetKeyDown(KeyCode.A))
                {
                    Rotate(Vector3.up, -rotationAngle);
                }

                if (Input.GetKeyDown(KeyCode.D))
                {
                    Rotate(Vector3.up, rotationAngle);
                }
            }
        }

        void Rotate(Vector3 axis, float angle)
        {
            transform.Rotate(axis, angle, Space.World);
        }

    }
}
