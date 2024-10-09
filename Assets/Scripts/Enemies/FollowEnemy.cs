using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FollowEnemy : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float deceleration;
    [SerializeField] float tiltAmount;
    [SerializeField] float tiltSpeed;

    Vector2 currentVelocity = Vector2.zero;
    float currentTilt = 0f;

    public Transform player;
    private Rigidbody2D rb;
    private Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direction = player.position - transform.position;
        direction.Normalize();
        movement = direction;

        moveCharacter();
        rotateCharacter();
    }

    void moveCharacter()
    {
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
