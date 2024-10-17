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

    private void Awake()
    {
        playerRotation = transform.Find("PlayerRotation");
        playerCombat = GetComponent<PlayerCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnFire()
    {
        Vector3 position = new Vector3(playerRotation.position.x , playerRotation.position.y, 1f);
        GameObject bulletInstance = Instantiate(bullet, playerRotation.position, playerRotation.rotation);
        //bulletInstance.GetComponent<Bullet>().isPlayerBullet = true;
    }
}
