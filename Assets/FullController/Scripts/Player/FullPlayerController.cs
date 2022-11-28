using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FullController.Scripts.Player
{
    public class FullPlayerController : FullPlayerComponent
    {
        [Header("Movement settings :")] 
        [SerializeField] private float moveSpeed = 4.0f;
        [SerializeField] private float rotationSpeed = 1.0f;
        [SerializeField] private float SpeedChangeRate = 10.0f;
        [SerializeField] private bool canMove = true;

        [Space(10), Header("Jump value :")] 
        [SerializeField] private float jumpForce = 1.0f;
        [SerializeField] private LayerMask groundedMask;
        [SerializeField] private bool grounded;

        [Space(10), Header("Camera values :")] 
        [SerializeField] private GameObject cinemachineCameraRoot = null;
        [SerializeField] private GameObject mainCamera = null;
        [SerializeField] private float CameraAngleOverride;
        [SerializeField,Range(0.0f, 0.3f)] private float rotationSmoothTime = 0.12f;
        [SerializeField] private float topClamp = 90f;
        [SerializeField] private float bottomClamp = -90f;

        [Space(10), Header("References :")] [SerializeField]
        private CharacterController controller = null;

        public Vector2 direction { get; private set; }

        private float speed = 0f;
        
        private Vector2 look = Vector2.zero;
        private float rotationVelocity;
        private float verticalVelocity;
        private bool isJump = false;
        private bool freeLook = false;
        private bool wasFreeLook = false;
        
        Vector3 smoothMoveVelocity;

        private const float threshold = 0.01f;

        private float cinemachineTargetPitch;
        private float cinemachineTargetYaw;
        
        private float targetRotation = 0.0f;
        private float terminalVelocity = 53.0f;

        #region Inputs

        public void InputMoveCallback(InputAction.CallbackContext callback)
        {
            direction = callback.ReadValue<Vector2>();
        }

        public void InputJumpCallback(InputAction.CallbackContext callback)
        {
            if (callback.performed || callback.canceled)
            {
                isJump = callback.action.IsPressed();
            }
        }
        public void InputLookCallback(InputAction.CallbackContext callback)
        {
            look = callback.ReadValue<Vector2>();
        }
        public void InputFreeLookCallback(InputAction.CallbackContext callback)
        {
            if (callback.performed || callback.canceled)
            {
                freeLook = callback.action.IsPressed();
            }
        }

        #endregion

        private void Update()
        {
            if (disableComponent) return;
            Move();
        }

        private void LateUpdate()
        {
            if (disableComponent) return;
            CameraRotation();
        }

        private void Move()
        {

            float targetSpeed = moveSpeed;
            if (direction == Vector2.zero) targetSpeed = 0f;

            float currentHSpeed = new Vector3(controller.velocity.x, 0f, controller.velocity.z).magnitude;

            float speedOffset = 0.1f;
            float inputMagnitude = 1f; //rework for gamepad

            if (currentHSpeed < targetSpeed - speedOffset || currentHSpeed > targetSpeed + speedOffset)
            {
                speed = Mathf.Lerp(currentHSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);
                speed = Mathf.Round(speed * 1000f) / 1000f;
            }
            else
            {
                speed = targetSpeed;
            }

            Vector3 inputDirection = new Vector3(direction.x, 0f, direction.y).normalized;
            
            if (!freeLook)
            {
                targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, rotationSmoothTime);
                
                transform.rotation = Quaternion.Euler(0f, rotation, 0f);
            }

            Vector3 targetDirection = Quaternion.Euler(0f, targetRotation, 0f) * Vector3.forward;
            controller.Move(targetDirection.normalized * (speed * Time.deltaTime) + new Vector3(0f, verticalVelocity, 0f) * Time.deltaTime);
        }

        private void CameraRotation()
        {
            if (look.sqrMagnitude >= threshold)
            {
                cinemachineTargetYaw += look.x;
                cinemachineTargetPitch += look.y;
            }
            
            cinemachineTargetYaw = ClampAngle(cinemachineTargetYaw, float.MinValue, float.MaxValue);
            cinemachineTargetPitch = ClampAngle(cinemachineTargetPitch, bottomClamp, topClamp);
            
            cinemachineCameraRoot.transform.rotation = Quaternion.Euler(cinemachineTargetPitch + CameraAngleOverride, cinemachineTargetYaw, 0f);
        }

        private float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
    }
}