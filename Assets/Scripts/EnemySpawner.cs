using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Enemy spawn system:
 * Has a x% chance of spawning an enemy every x seconds within a certain distance from the player
 * Enemy spawn level and frequency scales with player level
 * Does not spawn enemies if max enemies are present
 */

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float spawnCooldown = 10f;
    [SerializeField] float maxSpawnDistance;
    [SerializeField] float minSpawnDistance;
    [SerializeField] float spawnChance = 0.5f;
    [SerializeField] float spawnScaling = 1.2f;
    [SerializeField] List<GameObject> enemyPrefabs;

    [SerializeField] float maxEnemies = 25f;

    private Transform player;

    private float nextSpawnTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawnTime)
        {
            if (Random.value < spawnChance && GameObject.FindGameObjectsWithTag("Enemy").Length <= maxEnemies)
            {
                spawnEnemy();
            }
            nextSpawnTime = Time.time + spawnCooldown;  // Reset the cooldown
        }
    }

    void spawnEnemy ()
    {
        if (enemyPrefabs.Count == 0) return;

        GameObject enemyToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

        Vector2 spawnDirection = Random.insideUnitCircle.normalized;
        float spawnDistance = Random.Range(minSpawnDistance, maxSpawnDistance);
        Vector2 spawnPosition = (Vector2)player.position + spawnDirection * spawnDistance;

        Instantiate(enemyToSpawn, new Vector3(spawnPosition.x, spawnPosition.y, (Random.value+10)), Quaternion.identity);
    }
}
