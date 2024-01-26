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

    private Coroutine extendHookCoroutine;
    private Coroutine retractHookCoroutine;

    private void Awake()
    {
        _minHookSize = transform.localScale.y;
    }

    private void Start()
    {
        playerInput.Shoot.performed += PlayerShooting;
        playerInput.Shoot.canceled += PlayerShooting;
    }

    private void OnDisable()
    {
        playerInput.Shoot.performed -= PlayerShooting;
        playerInput.Shoot.canceled -= PlayerShooting;
        StopExtendHookCoroutine();
        StopRetractHookCoroutine();
    }

    private void Update()
    {
        if (timer < Time.deltaTime)
        {
            ChangeShootState(true);
        }

        if (transform.localScale.y >= _maxHookSize)
        {
            StartRetractHookCorutine();
        }
    }
    private void PlayerShooting(InputAction.CallbackContext inputAction)
    {
        if (inputAction.performed && _canShoot)
        {
            if (retractHookCoroutine != null)
            {
                StopRetractHookCoroutine();
                StartExtendHookCorutine();
            }
            else if (extendHookCoroutine != null)
            {
                StopExtendHookCoroutine();
                StartRetractHookCorutine();
            }
            else
            {
                StartExtendHookCorutine();
            }
        }
    }

    private IEnumerator ExtendHook()
    {
        while (_canShoot && transform.localScale.y <= _maxHookSize)
        {
            float newYScale = transform.localScale.y + _hookSpeed * Time.deltaTime;
            transform.localScale = new Vector3(transform.localScale.x, newYScale, transform.localScale.z);
            yield return null;
        }
    }

    private IEnumerator RetractHook()
    {
        while (_canShoot && transform.localScale.y >= _minHookSize)
        {
            float newYScale = transform.localScale.y - _hookSpeed * Time.deltaTime;
            transform.localScale = new Vector3(transform.localScale.x, newYScale, transform.localScale.z);
            yield return null;
        }
    }

    private void StartExtendHookCorutine()
    {
        if (extendHookCoroutine == null)
        {
            extendHookCoroutine = StartCoroutine(ExtendHook());
        }
    }

    private void StopExtendHookCoroutine()
    {
        if (extendHookCoroutine != null)
        {
            StopCoroutine(extendHookCoroutine);
            extendHookCoroutine = null;
        }
    }

    private void StartRetractHookCorutine()
    {
        if (retractHookCoroutine == null)
        {
            retractHookCoroutine = StartCoroutine(RetractHook());
        }
    }

    private void StopRetractHookCoroutine()
    {
        if (retractHookCoroutine != null)
        {
            StopCoroutine(retractHookCoroutine);
            retractHookCoroutine = null;
        }
    }
    private void StartEntangle()
    {
        ChangeShootState(false);
        timer = Time.deltaTime + _entangleCoolDown;
    }
    private void ChangeShootState(bool state)
    {
        _canShoot = state;
        _boxCollider.enabled = state;
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.collider.CompareTag("Hook")) StartEntangle();
    }
}

