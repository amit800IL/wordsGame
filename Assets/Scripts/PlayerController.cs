using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerMode { Roaming, HookExtends, HookRetracts}

    [SerializeField] private PlayerMode _playerMode;
    public PlayerMode playerMode
    {
        get => _playerMode;
        set
        {
            _playerMode = value;

            movementController.enabled = (value == PlayerMode.Roaming);
            swivelController.enabled = (value == PlayerMode.Roaming);
        }
    }

    [Header("Components")]
    [SerializeField] private PlayerMovement movementController;
    [SerializeField] private HookController hookController;
    [SerializeField] private Swivel swivelController;
}


