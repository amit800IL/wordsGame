using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HookController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [Space, SerializeField] private PlayerInputSO playerInput;

    [Space, Header("Graphics Settings")]
    [SerializeField] private Transform ropeConnectionPoint;

    [Space, Header("Properties")]
    [SerializeField] private float maxHookRange;
    [SerializeField] private float hookExtendSpeed, hookRetractSpeed;

    [Header("Spherecast Settings")]
    [SerializeField] private LayerMask wordBoxesLayerMask;
    [SerializeField] private float sphereCastRadius;
    [SerializeField] private float castOffset;

    [Space, Header("Components")]
    [SerializeField] private Transform hookTransform;
    [SerializeField] private LineRenderer ropeLineRenderer;

    

    private IEnumerator ExtendHookLoop()
    {
        playerController.playerMode = PlayerController.PlayerMode.HookExtends;

        Word caughtWord = null;

        while (true)
        {
            float hookDistance = Vector3.Distance(hookTransform.position, transform.position);
            if (hookDistance > maxHookRange) break;           

            Vector3 castOrigin = hookTransform.position + castOffset * Vector3.up;
            float hookRange = hookExtendSpeed * Time.deltaTime;
            if (Physics.SphereCast(castOrigin, sphereCastRadius, hookTransform.up, out RaycastHit hit, hookRange, wordBoxesLayerMask))
            {
                Word word = hit.collider.transform.parent.GetComponent<Word>();
                Debug.Log(word);
                if (word != null)
                {
                    caughtWord = word;
                    word.Catch(hookTransform);
                    break;
                }
            }
            else
                hookTransform.position += hookTransform.up * hookRange;


            yield return null;
        }

        RetractHook(caughtWord);
    }

    private IEnumerator RetractHookLoop(Word heldWord)
    {
        playerController.playerMode = PlayerController.PlayerMode.HookRetracts;
        float retractionSpeed = heldWord == null || heldWord.Length == 0 ? hookRetractSpeed : hookRetractSpeed / heldWord.Length;

        Vector3 hookOffset = hookTransform.position - transform.position;
        while (Vector3.Dot(hookOffset, hookTransform.up) > 0)
        {
            hookTransform.position -= hookTransform.up * retractionSpeed * Time.deltaTime;

            hookOffset = hookTransform.position - transform.position;
            yield return null;
        }

        hookTransform.localPosition = Vector3.zero;
        playerController.playerMode = (heldWord == null) ? PlayerController.PlayerMode.Roaming : PlayerController.PlayerMode.HoldingWord;
    }

    private void ExtendHook()
    {
        if(playerController.playerMode == PlayerController.PlayerMode.Roaming)
        {
            //StopAllCoroutines();
            
            StartCoroutine(ExtendHookLoop());
        }
    }

    private void RetractHook(Word heldWord = null)
    {
        Debug.Log("retract");

        StopAllCoroutines();

        StartCoroutine(RetractHookLoop(heldWord));
    }

    private void LateUpdate()
    {
        if(ropeLineRenderer != null)
        {
            ropeLineRenderer.SetPosition(0, ropeConnectionPoint.position);
            ropeLineRenderer.SetPosition(1,hookTransform.position);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, maxHookRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(hookTransform.position + castOffset * Vector3.up, sphereCastRadius);
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
