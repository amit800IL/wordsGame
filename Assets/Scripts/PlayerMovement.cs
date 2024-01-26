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
        MoveInDirection(movementInput);
    }

    private void MoveInDirection(Vector2 input)
    {
        Vector3 currentPosition = transform.position;

        // Cast rays to the left and right
        RaycastHit hitLeft, hitRight;

        // Left raycast
        if (Physics.Raycast(currentPosition, -transform.right, out hitLeft, _rayCastDistance))
        {
            Debug.DrawRay(currentPosition, -transform.right * hitLeft.distance, Color.blue);
            MoveWithRaycast(hitLeft.normal.normalized, input.x);
        }
        // Right raycast
        else if (Physics.Raycast(currentPosition, transform.right, out hitRight, _rayCastDistance))
        {
            Debug.DrawRay(currentPosition, transform.right * hitRight.distance, Color.red);
            MoveWithRaycast(hitRight.normal.normalized, input.x);
        }
        // No raycast hit
        else
        {
            Debug.Log("No raycast hit. Free movement.");
            transform.Translate(Vector3.right * movementSpeed * input.x * Time.deltaTime);
        }
    }

    private void MoveWithRaycast(Vector3 direction, float input)
    {
        transform.position += direction * movementSpeed * input * Time.deltaTime;
    }

}

