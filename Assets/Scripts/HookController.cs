using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HookController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [Space, SerializeField] private PlayerInputSO playerInput;

    [Space, Header("Properties")]
    [SerializeField] private float maxHookRange;
    [SerializeField] private float hookExtendSpeed, hookRetractSpeed;
    [SerializeField] private LayerMask wordBoxesLayerMask;

    [Space, Header("Components")]
    [SerializeField] private Transform hookTransform;
    [SerializeField] private LineRenderer ropeLineRenderer;

    private IEnumerator ExtendHookLoop()
    {
        float hookDistance = Vector3.Distance(hookTransform.position, transform.position);
        while (hookDistance < maxHookRange)
        {
            Vector3 targetPosition= hookTransform.position + hookTransform.up * hookExtendSpeed * Time.deltaTime;
            //hookTransform.position += hookTransform.up * hookExtendSpeed * Time.deltaTime;

            bool raycast = Physics.Raycast(hookTransform.position, hookTransform.up, out RaycastHit hit, hookRetractSpeed * Time.deltaTime, wordBoxesLayerMask);
            if (!raycast)
                hookTransform.position = targetPosition;
            else if (hit.collider.GetComponent<Word>())
            {
                hit.transform.parent.parent = hookTransform;
                break;
            }



            hookDistance = Vector3.Distance(hookTransform.position, transform.position);
            yield return null;
        }

        RetractHook();
    }

    private IEnumerator RetractHookLoop()
    {
        playerController.playerMode = PlayerController.PlayerMode.HookRetracts;

        Vector3 hookOffset = hookTransform.position - transform.position;
        while (Vector3.Dot(hookOffset, hookTransform.up) > 0)
        {
            hookTransform.position -= hookTransform.up * hookRetractSpeed * Time.deltaTime;

            hookOffset = hookTransform.position - transform.position;
            yield return null;
        }

        playerController.playerMode = PlayerController.PlayerMode.Roaming;
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
            
            StartCoroutine(RetractHookLoop());
            
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
