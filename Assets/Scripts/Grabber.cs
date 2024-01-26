using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    [SerializeField] private Transform grabber;
    [SerializeField] private Transform word;
    private void CatchWord()
    {
        word.position = transform.position;
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.collider.CompareTag("Word")) CatchWord();
    }
}