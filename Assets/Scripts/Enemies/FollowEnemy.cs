using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/*
 * Follow enemy:
 * Follows the player
 * Need to do: stop following when not in range
 */


public class FollowEnemy : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float deceleration;
    [SerializeField] float tiltAmount;
    [SerializeField] float tiltSpeed;

    [Header("Behavior")]
    [SerializeField] float followRange;

    private Transform player;
    private Rigidbody2D rb;
    private Vector2 movement;

    Vector2 currentVelocity = Vector2.zero;
    float currentTilt = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
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

        if (Vector2.Distance(transform.position, player.position) < followRange) // When in range
        {
            currentVelocity = Vector2.Lerp(currentVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
        }
        else // When out of range, decelerate
        {
            currentVelocity = Vector2.Lerp(currentVelocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
        }

        rb.velocity = currentVelocity;
    }

    void rotateCharacter()
    {
        float targetTilt = 0f;

        if (currentVelocity.magnitude > 0.1f)
        {
            targetTilt = (currentVelocity.x / moveSpeed) * -tiltAmount;
        }

        currentTilt = Mathf.LerpAngle(currentTilt, targetTilt, tiltSpeed * Time.fixedDeltaTime);

        transform.rotation = Quaternion.Euler(0, 0, currentTilt);
    }
}
