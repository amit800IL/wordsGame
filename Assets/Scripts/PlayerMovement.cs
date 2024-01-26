using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("ControllerAttributes")]
    [SerializeField] private float _railSizeMultiplyer;
    [SerializeField] private float _rayCastDistance;
    [Header("Components")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private PlayerInputSO playerInput;
    [SerializeField] private GameObject wordHolder;

    private Vector3 initialPosition;
    private void Awake()
    {
        wordHolder.transform.localScale = new Vector3(transform.localScale.x * ( _railSizeMultiplyer), 1, 1);
        transform.parent = wordHolder.transform;
        transform.localPosition =  Vector3.zero + new Vector3(0, 0, 1);

    }

    private void Start()
    {
        playerInput.Move.performed += MovePlayer;
        playerInput.Move.canceled += MovePlayer;
    }

    private void OnDisable()
    {
        playerInput.Move.performed -= MovePlayer;
        playerInput.Move.canceled -= MovePlayer;
    }

    private void MovePlayer(InputAction.CallbackContext inputAction)
    {
        Vector2 movementInput = inputAction.ReadValue<Vector2>();

        if (inputAction.performed)
        {
            Move(movementInput.x);
        }
    }


    private void Move(float direction)
    {
        float nextPosition = transform.localPosition.x + (transform.localScale.x * direction);
        float movementRange = (1 - (1 / _railSizeMultiplyer)) / 2 + 0.01f;
        if (nextPosition > movementRange || nextPosition < -movementRange) return;
        transform.localPosition += new Vector3(transform.localScale.x * direction, 0, 0);
    }


}

