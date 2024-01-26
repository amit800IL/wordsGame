using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("ControllerAttributes")]
    [SerializeField] private float _railSizeMultiplyer;
    [SerializeField] private float _rayCastDistance;
    [Header("Components")]
    [SerializeField] private GameObject _rail;
    [SerializeField] private float movementSpeed;
    [SerializeField] private PlayerInputSO playerInput;
    [SerializeField] private Rigidbody playerRigibBody;

    private Vector3 initialPosition;
    private void Awake()
    {
        if (_rail == null) return;
        initialPosition = transform.position;
        transform.localScale = new Vector3(transform.localScale.x * (_railSizeMultiplyer * _railSizeMultiplyer), 1, 1);
        transform.localPosition = initialPosition;
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
            moveInDirection(movementInput);
        }
    }

    private void moveInDirection(Vector2 input)
    {
        Vector3 currentPosition = transform.position;

        RaycastHit hitLeft, hitRight;

        if (Physics.Raycast(currentPosition, -transform.right, out hitLeft, _rayCastDistance))
        {
            MoveWithRaycast(hitLeft.normal.normalized, input.x);
        }

        else if (Physics.Raycast(currentPosition, transform.right, out hitRight, _rayCastDistance))
        {
            MoveWithRaycast(hitRight.normal.normalized, input.x);
        }
    }

    private void MoveWithRaycast(Vector3 direction, float input)
    {
        transform.position += direction * movementSpeed * input * Time.deltaTime;
    }
}

