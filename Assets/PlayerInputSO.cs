using UnityEngine;
using UnityEngine.InputSystem;


[CreateAssetMenu(fileName = "PlayerInput", menuName = "Player/PlayerInput")]
public class PlayerInputSO : ScriptableObject
{
    [field: SerializeField] public InputAction Move { get; private set; }
    [field: SerializeField] public InputAction Swivel { get; private set; }

    private void OnEnable()
    {
        Move.Enable();
        Swivel.Enable();
    }

    private void OnDisable()
    {
        Move.Disable();
        Swivel.Disable();
    }
}
