using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [Space, SerializeField] private PlayerInputSO playerInput;

    [Space, Header("Properties")]
    [SerializeField] private float maxHookRange;
    [SerializeField] private float hookExtendSpeed, hookRetractSpeed;

    [Space, Header("Components")]
    [SerializeField] private Transform hookTransform;
    [SerializeField] private LineRenderer ropeLineRenderer;

    private IEnumerator ExtendHookLoop()
    {        
        float hookDistance = Vector3.Distance(hookTransform.position, transform.position);
        while (hookDistance < maxHookRange)
        {
            hookTransform.position += hookTransform.up * hookExtendSpeed * Time.deltaTime;

            hookDistance = Vector3.Distance(hookTransform.position, transform.position);
            yield return null;
        }
    }

    private IEnumerator RetractHookLoop()
    {        
        Vector3 hookOffset = hookTransform.position - transform.position;
        while (Vector3.Dot(hookOffset, hookTransform.up) > 0)
        {
            hookTransform.position -= hookTransform.up * hookRetractSpeed * Time.deltaTime;

            hookOffset = hookTransform.position - transform.position;
            yield return null;
        }
    }

    private void ExtendHook()
    {
        if(playerController.playerMode == PlayerController.PlayerMode.Roaming)
        {
            //StopAllCoroutines();
            playerController.playerMode = PlayerController.PlayerMode.HookExtends;
            StartCoroutine(ExtendHookLoop());
        }
    }

    private void RetractHook()
    {
        if (playerController.playerMode == PlayerController.PlayerMode.HookExtends)
        {
            StopAllCoroutines();
            playerController.playerMode = PlayerController.PlayerMode.HookRetracts;
            StartCoroutine(RetractHookLoop());
            playerController.playerMode = PlayerController.PlayerMode.Roaming;
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
        playerInput.Shoot.performed += _ => ExtendHook();
        //playerInput.Shoot.performed += _ => RetractHook();
    }

    private void OnDisable()
    {
        playerInput.Shoot.performed -= _ => ExtendHook();
        //playerInput.Shoot.performed -= _ => RetractHook();
    }
}
