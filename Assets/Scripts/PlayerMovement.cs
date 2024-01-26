using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("ControllerAttributes")]
    [SerializeField] private float _railSizeMultiplyer;
    [Header("Components")]
    [SerializeField] private GameObject _rail;
    [SerializeField] private float movementSpeed;
    [SerializeField] private PlayerInputSO playerInput;
    [SerializeField] private Rigidbody playerRigibBody;

    private void Awake()
    {
        if (_rail == null) return;
        _rail.transform.localScale = new Vector3(transform.localScale.x * (_railSizeMultiplyer * _railSizeMultiplyer), 1, 1);
        transform.localPosition = Vector3.zero + new Vector3(0, 0, 1);
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
        moveOnRail(movementInput.x);
    }

    private void moveOnRail(float direction)
    {
        float nextPosition = transform.localPosition.x + (movementSpeed * direction * Time.deltaTime);
        float movementRange = Mathf.Abs(_railSizeMultiplyer);

        if (nextPosition > movementRange || nextPosition < -movementRange)
        {
            Debug.Log("Movement out of range.");
            return;
        }

        transform.localPosition += new Vector3(movementSpeed * direction * Time.deltaTime, 0, 0);
        Debug.Log($"New Position: {transform.localPosition}");
    }

}
  