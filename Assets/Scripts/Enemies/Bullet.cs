using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Bullet:
 * Travels a distance before despawning
 * Does not collide with enemies
 * Need to do: collision effect
 */

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10.0f;
    public float timeToDestroy = 10.0f;
    public int damage = 1;
    public bool isPlayerBullet = false;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine("DestroyCountdown");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = (transform.up * bulletSpeed);
        //print(rb.velocity);
    }

    IEnumerator DestroyCountdown()
    {
        yield return new WaitForSeconds(timeToDestroy);
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object the bullet collided with is the player
        if (other.CompareTag("Player"))
        {
            if (isPlayerBullet)
            {
                //do nothing
            }
            else
            {
                PlayerCombat player = other.GetComponent<PlayerCombat>();
                player.TakeHit(damage);

                // Destroy the bullet after hitting the player
                DestroyBullet();
            }
        }
        else if (other.CompareTag("Enemy"))
        {
            if (isPlayerBullet)
            {
                //enemy takes hit
            }
            else
            {
                //do nothing
            }
        }
        else
        {
            DestroyBullet();
        }
    }
}
