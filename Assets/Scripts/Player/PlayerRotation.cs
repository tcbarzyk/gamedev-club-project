using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 * Controls player rotation towards mouse
 */

public class PlayerRotation : MonoBehaviour
{
    private Camera mainCamera;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    void FixedUpdate()
    {
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

        //print("rotating to " + angle);

        // Apply the rotation to face the mouse
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
