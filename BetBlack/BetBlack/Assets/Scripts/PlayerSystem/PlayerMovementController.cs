using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace BulletEcho.PlayerSystem
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private float walkingSpeed = 7.5f;
        [SerializeField] private  float runningSpeed = 11.5f;
        [SerializeField] private  float gravity = 20.0f;
        [SerializeField] private  float lookSpeed = 2.0f;
        [SerializeField] private  float lookXLimit = 45.0f;
        [SerializeField]private  bool canMove = true;

        private Vector3 moveDirection = Vector3.zero;
        private float rotationX = 0;


        private void Start()
        {
            characterController = GetComponent<CharacterController>();
            // Lock cursor
            //Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;
        }

        private void Update()
        {
            var forward = transform.TransformDirection(Vector3.forward);
            var right = transform.TransformDirection(Vector3.right);
            var isRunning = Input.GetKey(KeyCode.LeftShift);
            var curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
            var curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
            var movementDirectionY = moveDirection.y;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            
            if (!characterController.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }
            characterController.Move(moveDirection * Time.deltaTime);
            if (canMove)
            {
                rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                // playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            }
        }
    }
}
