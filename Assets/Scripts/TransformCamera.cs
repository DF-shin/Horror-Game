using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformCamera : MonoBehaviour
{
    public Transform cameraPosition;
    void Update()
    {
        transform.position = cameraPosition.position;
    }
}
