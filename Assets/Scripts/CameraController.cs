using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] float turnSpeed = 4.0f;
    [SerializeField] float moveSpeed = 2.0f;

    [SerializeField] float minTurnAngle = -90.0f;
    [SerializeField] float maxTurnangle = 90.0f;

    private float rotx;
    void Start()
    {

    }
    void Update()
    {
        MouseAiming();
        KeyBoardMovement();

    }

    private void KeyBoardMovement()
    {
        Vector3 dir = new Vector3(0, 0, 0);

        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");
    }

    private void MouseAiming()
    {
        float y = Input.GetAxis("Mouse X") * turnSpeed;
        rotx += Input.GetAxis("Mouse Y") * turnSpeed;

        rotx = Mathf.Clamp(rotx, minTurnAngle, maxTurnangle);

        transform.eulerAngles = new Vector3(-rotx, transform.eulerAngles.y + y, 0);
    }

}
