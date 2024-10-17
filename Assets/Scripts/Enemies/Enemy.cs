using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private float despawnTimer = 5f;
    [SerializeField] private float despawnDistance = 80f;

    private Transform player;

    private int health;
    private float timeToDespawn = 0f;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Awake()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) > despawnDistance)
        {
            if (timeToDespawn == 0)
            {
                timeToDespawn = Time.time + despawnTimer;
            }
            else if (Time.time > timeToDespawn)
            {
                despawn();
            }
        }
        else
        {
            timeToDespawn = 0;
        }
    }

    void despawn()
    {
        Destroy(gameObject);
    }
}
