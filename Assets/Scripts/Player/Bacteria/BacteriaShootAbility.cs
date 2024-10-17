using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BacteriaShootAbility : MonoBehaviour
{
    private Transform playerRotation;
    private PlayerCombat playerCombat;

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
        print("Player fire!!!");
    }
}
