using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private PlayerInputSO playerInput;
    [SerializeField] private GameObject hookGameObject;
    private void Start()
    {
        playerInput.Shoot.performed += PlayerShooting;
        playerInput.Shoot.canceled += PlayerShooting;
    }

    private void OnDisable()
    {
        playerInput.Shoot.performed -= PlayerShooting;
        playerInput.Shoot.canceled -= PlayerShooting;
    }
    private void PlayerShooting(InputAction.CallbackContext inputAction)
    {
        if (inputAction.performed)
        {
            float hookScale = hookGameObject.transform.localScale.y;

            while (hookScale < 10)
            {
                hookScale++;
            }
        }
    }
}
