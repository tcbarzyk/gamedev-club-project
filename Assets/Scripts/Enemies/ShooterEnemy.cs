using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class ShooterEnemy : MonoBehaviour
{
    [SerializeField]
    private float shootCooldown = 1f;

    [SerializeField] float acceleration = 0f;
    //[SerializeField] float movementCooldown = 0f;
    [SerializeField] float minMovement = 0f;
    [SerializeField] float maxMovement = 0f;

    private float nextFireTime = 0f;
    private bool moving = false;
    Vector2 currentVelocity = Vector2.zero;
    Vector2 randomVector = Vector2.zero;

    private Rigidbody2D rb;
    public GameObject bullet;
    private Transform[] shootPoints;

    // Start is called before the first frame update
    void Start()
    {
        Transform parentShootPoints = transform.Find("ShootPoints");
        shootPoints = new Transform[parentShootPoints.childCount];

        for (int i = 0; i < parentShootPoints.childCount; i++)
        {
            shootPoints[i] = parentShootPoints.GetChild(i);
        }

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Time.time > nextFireTime - (shootCooldown / 2)) && !moving)
        {
            moving = true;
            float randomAngle = UnityEngine.Random.Range(0f, Mathf.PI * 2);
            float randomMagnitude = UnityEngine.Random.Range(minMovement, maxMovement);

            // Create the vector and scale it by the random magnitude
            randomVector = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)) * randomMagnitude;
        }
        if (Time.time > nextFireTime)
        {
            Shoot();  // Shoot if cooldown has passed
            nextFireTime = Time.time + shootCooldown;  // Reset the cooldown
            moving = false;
        }
    }

    private void FixedUpdate()
    {
        moveCharacter();
    }

    void moveCharacter()
    {
        Vector2 targetVelocity = randomVector;

        if (moving)
        {
            currentVelocity = Vector2.Lerp(currentVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
        }
        else
        {
            currentVelocity = Vector2.Lerp(currentVelocity, Vector2.zero, acceleration * Time.fixedDeltaTime);
        }

        rb.velocity = currentVelocity;
    }

    private void Shoot()
    {
        foreach (Transform point in shootPoints)
        {
            Instantiate(bullet, point.position, point.rotation);
        }
    }
}
