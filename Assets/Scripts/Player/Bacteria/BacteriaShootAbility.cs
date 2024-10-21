using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;

public class BacteriaShootAbility : MonoBehaviour
{
    private Transform playerRotation;
    private PlayerCombat playerCombat;
    public GameObject bullet;

    [SerializeField] bool shooting = false;
    private float nextShot = 0f;
    [SerializeField] float shotCooldown = 1f;

    private void Awake()
    {
        playerRotation = transform.Find("PlayerRotation");
        playerCombat = GetComponent<PlayerCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shooting && (nextShot < Time.time))
        {
            nextShot = Time.time + shotCooldown;
            shoot();
        }
    }

    public void OnFire(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            shooting = true;
        }
        else if (ctx.canceled)
        {
            shooting = false;
        }
    }

    private void shoot()
    {
        Vector3 position = new Vector3(playerRotation.position.x, playerRotation.position.y, 1f);
        GameObject bulletInstance = Instantiate(bullet, playerRotation.position, playerRotation.rotation);
        //bulletInstance.GetComponent<Bullet>().isPlayerBullet = true;
    }
}
