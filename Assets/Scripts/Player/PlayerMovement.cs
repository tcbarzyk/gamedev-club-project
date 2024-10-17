using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 * Controls player movement and tilt
 * 
 * 
 */

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float acceleration = 5f;
    [SerializeField] float deceleration = 5f;
    [SerializeField] float tiltAmount;
    [SerializeField] float tiltSpeed;

    private Camera mainCamera;
    private GameObject playerRotationObject;
    private PlayerControls playerControls;
    private Rigidbody2D rb;

    private InputAction move;

    Vector2 movement;
    Vector2 currentVelocity = Vector2.zero;
    float currentTilt = 0f;

    private void Awake()
    {
        playerControls = GetComponent<PlayerControls>();
        playerRotationObject = GameObject.Find("PlayerRotation");
        mainCamera = Camera.main;
        move = playerControls.move;
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
        rotateCharacter();
    }

    void rotateCharacter()
    {
        // Calculate the target tilt based on velocity
        float targetTilt = 0f;

        if (currentVelocity.magnitude > 0.1f)
        {
            // Calculate tilt based on movement direction
            //float angle = Mathf.Atan2(currentVelocity.y, currentVelocity.x) * Mathf.Rad2Deg;
            targetTilt = (currentVelocity.x / moveSpeed) * -tiltAmount;
        }

        // Smoothly interpolate the current tilt towards the target tilt
        currentTilt = Mathf.LerpAngle(currentTilt, targetTilt, tiltSpeed * Time.fixedDeltaTime);

        // Apply the current tilt to the character's rotation
        transform.rotation = Quaternion.Euler(0, 0, currentTilt);
    }
}
