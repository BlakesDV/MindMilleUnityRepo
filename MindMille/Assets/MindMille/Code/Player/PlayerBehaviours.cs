using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviours : MonoBehaviour
{
    public CharacterController controller;
    private PlayerInputActions inputActions;

    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private Vector3 moveDirection;
    private Vector3 velocity;
    private bool isJumping;
    private float gravity = -9.81f;
    private bool isGrounded;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Movement.Jump.performed += ctx => Jump();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Update()
    {
        RotateTowardsMouse();
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;
        float x = 0f;
        float z = 0f;

        if (inputActions.Movement.Front.IsPressed())
            z += 1f;
        if (inputActions.Movement.Back.IsPressed())
            z -= 1f;
        if (inputActions.Movement.Right.IsPressed())
            x += 1f;
        if (inputActions.Movement.Left.IsPressed())
            x -= 1f;

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Jump()
    {
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
    }
    void RotateTowardsMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            Vector3 target = hit.point;
            Vector3 direction = target - transform.position;
            direction.y = 0f;
            if (direction.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
            }
        }
    }
}
