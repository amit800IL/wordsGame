using UnityEngine;
using UnityEngine.InputSystem;


[CreateAssetMenu(fileName = "PlayerInput", menuName = "Player/PlayerInput")]
public class PlayerInputSO : ScriptableObject
{
    [field: SerializeField] public InputAction Move { get; private set; }
    [field: SerializeField] public InputAction Shoot { get; private set; }

    private void OnEnable()
    {
        Move.Enable();
        Shoot.Enable();
    }

    private void OnDisable()
    {
        Move.Disable();
        Shoot.Disable();
    }
}
