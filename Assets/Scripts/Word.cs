using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Word : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private string wordString;
    [SerializeField] private float letterScale;

    [SerializeField] private float fixTorque = 1;
    [SerializeField] public Rigidbody boxRigidbody;

    public BoxCollider boxCollider => GetComponentInChildren<BoxCollider>();


    public Rigidbody pivot => GetComponent<Rigidbody>();

    public int Length => wordString.Length;

    public void Catch(Rigidbody hookRigidbody)
    {
        transform.parent = hookRigidbody.transform;
    }

    private void FixedUpdate()
    {
        if (transform.position.y < 0)
        {
            pivot.useGravity = false;
            pivot.constraints |= RigidbodyConstraints.FreezePositionY;
        }

        if (!pivot.useGravity)
        {
            Vector3 axis = Vector3.Cross(boxRigidbody.transform.up, Vector3.up).normalized;
            float angle = Vector3.Angle(boxRigidbody.transform.up, Vector3.up);

            boxRigidbody.AddTorque(axis * Mathf.Sin(angle * Mathf.Deg2Rad) * fixTorque);
        }
    }

    private void LateUpdate()
    {
        if(!pivot.useGravity) transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + transform.up);
    }
}
