using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : MonoBehaviour
{
    [SerializeField]
    private float shootCooldown = 1f;

    private float nextFireTime = 0f;

    public GameObject bullet;
    private Transform[] shootPoints;

    // Start is called before the first frame update
    void Start()
    {
        //shootPoints = new List<Transform>();

        // Get all the child transforms of the ShooterEnemy
        shootPoints = GameObject.Find("ShootPoints").GetComponentsInChildren<Transform>();

        /*
        foreach (Transform child in childTransforms)
        {
            shootPoints.Add(child); // Add the ShootPoint transform to the list
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextFireTime)
        {
            Shoot();  // Shoot if cooldown has passed
            nextFireTime = Time.time + shootCooldown;  // Reset the cooldown
        }
    }

    private void Shoot()
    {
        foreach (Transform point in shootPoints)
        {
            Instantiate(bullet, point.position, point.rotation);
        }
    }
}
