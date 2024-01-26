using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hook : MonoBehaviour
{
    [Header("Attributtes")]
    [SerializeField] private float _entangleCoolDown;
    [SerializeField] private float _hookSpeed;
    [SerializeField] private float _maxHookSize;
    private float _minHookSize;
    [Header("Components")]
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private PlayerInputSO playerInput;
    bool _canShoot = true;
    private float timer;
    private bool _isShooting = false;
    private bool isHolding = false;


    private void Awake()
    {
        _minHookSize = transform.localScale.y;
    }
    private void SetHold(bool state)
    {
        isHolding = state;
    }
    private void Start()
    {
        playerInput.Shoot.performed += ctx => SetHold(true);
        playerInput.Shoot.canceled += ctx => SetHold(false);
    }

    private void OnDisable()
    {
        playerInput.Shoot.performed -= ctx => SetHold(true);
        playerInput.Shoot.canceled -= ctx => SetHold(false);
    }

    private void Update()
    {
        if (timer < Time.deltaTime)
        {
            _canShoot = true;
        }
        if (isHolding) MoveHook(1);
         if (!isHolding) MoveHook(-1);
      


    }

    private void MoveHook(int direction)
    {
        Debug.Log("dir:" + direction);
        if (direction == 1 && transform.localScale.y <= _maxHookSize) transform.localScale += new Vector3(0, _hookSpeed * direction * Time.deltaTime, 0);
        else  transform.localScale += new Vector3(0, _hookSpeed * direction * Time.deltaTime, 0);
    }
 
    private void StartEntangle()
    {
       _canShoot = false;   
        timer = Time.deltaTime + _entangleCoolDown;
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.collider.CompareTag("Hook")) StartEntangle();
    }
}

