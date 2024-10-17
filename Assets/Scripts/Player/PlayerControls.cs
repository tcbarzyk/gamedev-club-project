using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputActionAsset inputAsset;
    private InputActionMap inputMap;

    public InputAction move;
    public InputAction fire;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        inputAsset = playerInput.actions;
        inputMap = inputAsset.FindActionMap("Player");

        move = inputMap.FindAction("Move");
        fire = inputMap.FindAction("Fire");

        fire.Enable();
        move.Enable();
    }
}
