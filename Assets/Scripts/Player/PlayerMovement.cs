using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    PlayerInputActions playerControls;

    Rigidbody2D rb;

    Vector2 movement;
    Vector2 currentVelocity = Vector2.zero;

    private InputAction move;
    private InputAction fire;

    public float moveSpeed = 5f;
    public float acceleration = 5f;
    public float deceleration = 5f;

    private Camera mainCamera;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = move.ReadValue<Vector2>();
    }
    void FixedUpdate()
    {
        // Calculate the target velocity based on input
        Vector2 targetVelocity = movement * moveSpeed;

        // Accelerate or decelerate towards the target velocity
        if (movement.magnitude > 0) // When there is input
        {
            currentVelocity = Vector2.Lerp(currentVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
        }
        else // When there is no input, decelerate smoothly
        {
            currentVelocity = Vector2.Lerp(currentVelocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
        }

        // Apply the velocity to the rigidbody
        rb.velocity = currentVelocity;
        RotateTowardsMouse();
    }

    void RotateTowardsMouse()
    {
        // Get the mouse position in screen coordinates and convert it to world space
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePosition.z = 0f; // Set z to 0 because we're in 2D

        // Calculate the direction from the player to the mouse
        Vector2 direction = (mousePosition - transform.position).normalized;

        // Calculate the angle in radians and convert to degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply the rotation to face the mouse
        rb.rotation = angle;
    }
}
