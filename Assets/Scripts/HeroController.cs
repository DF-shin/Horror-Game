using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class HeroController : MonoBehaviour
{
    [Header("player")]
    public float playerWalkingSpeed = 7.5f;
    public float playerRunningSpeed = 11.5f;
    public float playerCrouchSpeed = 4.5f;
    public float playerJumpSpeed = 8.0f;
    public float playerGravity = 20.0f;

    [Header("Camera")]
    public Camera playerCamera;
    public float cameraLookSpeed = 2.0f;
    public float cameraXlimit = 45.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationx = 0;

    Rigidbody heroRigidBody;
    Animator myAnimator;

    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        myAnimator = GetComponent<Animator>();
        heroRigidBody = GetComponent<Rigidbody>();

        // locks Cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        heroOnGround();

        float movementDirectionY = moveDirection.y;
        float curSpeedX = 0.0f;

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        bool isCrouching = Input.GetKey(KeyCode.LeftControl);


        // Two long if statements to check if running or crouching. Can't think of a cleaner way, but this works.

        curSpeedX = canMove ? (
            isRunning ? playerRunningSpeed : isCrouching ? playerCrouchSpeed : playerWalkingSpeed) * Input.GetAxis("Vertical") : 0;

        float curSpeedY = canMove ? (
            isRunning ? playerRunningSpeed : isCrouching ? playerCrouchSpeed : playerWalkingSpeed) * Input.GetAxis("Horizontal") : 0;

        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = playerJumpSpeed;
            myAnimator.SetBool("IsJumping", true);
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }


        if (!characterController.isGrounded)
        {
            moveDirection.y -= playerGravity * Time.deltaTime;

        }

        if (curSpeedX != 0 || curSpeedY != 0)
        {
            myAnimator.SetBool("IsMoving", true);
        }
        else
        {
            myAnimator.SetBool("IsMoving", false);
        }

        characterController.Move(moveDirection * Time.deltaTime);


        if (canMove)
        {
            rotationx += -Input.GetAxis("Mouse Y") * cameraLookSpeed;
            rotationx = Mathf.Clamp(rotationx, -cameraXlimit, cameraXlimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationx, 0, 0);

            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * cameraLookSpeed, 0);

        }
    }

    void heroOnGround()
    {
        if (characterController.isGrounded)
        {
            myAnimator.SetBool("IsJumping", false);
        }
    }

}


