using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookController : MonoBehaviour
{
    //[Header("Settings")]
    [SerializeField] private PlayerInputSO playerInput;

    [Space, Header("Properties")]
    [SerializeField] private float maxHookRange;
    [SerializeField] private float hookExtendSpeed, hookRetractSpeed;

    [Space, Header("Components")]
    [SerializeField] private Transform hookTransform;
    [SerializeField] private LineRenderer ropeLineRenderer;

    private IEnumerator ExtendHook()
    {
        float hookDistance = Vector3.Distance(hookTransform.position, transform.position);
        while(hookDistance<maxHookRange)
        {
            hookTransform.position += hookTransform.up * hookExtendSpeed * Time.deltaTime;

            hookDistance = Vector3.Distance(hookTransform.position, transform.position);
            yield return null;
        }
    }

    private IEnumerator RetractHook()
    {
        Vector3 hookOffset = hookTransform.position - transform.position;
        while(Vector3.Dot(hookOffset, hookTransform.up)>0)
        {
            hookTransform.position -= hookTransform.up * hookRetractSpeed * Time.deltaTime;

            hookOffset = hookTransform.position - transform.position;
            yield return null;
        }

    }

    private void LateUpdate()
    {
        if(ropeLineRenderer != null)
        {
            ropeLineRenderer.SetPosition(0, transform.position);
            ropeLineRenderer.SetPosition(1,hookTransform.position);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, maxHookRange);
    }

    private void OnEnable()
    {
        playerInput.Shoot.performed += _ => StartCoroutine(ExtendHook());
        playerInput.Shoot.performed += _ => StartCoroutine(RetractHook());
    }

    private void OnDisable()
    {
        playerInput.Shoot.performed -= _ => StartCoroutine(ExtendHook());
        playerInput.Shoot.performed -= _ => StartCoroutine(RetractHook());
    }
}
