using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swivel : MonoBehaviour
{
    [Header("SwivelAttributes")]
    [SerializeField] private float _swivelSpeed;
    [SerializeField] private float _swivelRange;

    private Quaternion startRotation;
    private Quaternion targetRotation;

    private float timer = 0;

    void Start()
    {
        startRotation = transform.localRotation;
        float xLocalRotation = transform.localRotation.eulerAngles.x;
        float yLocalRotation = transform.localRotation.eulerAngles.y;
        targetRotation = Quaternion.Euler(xLocalRotation, yLocalRotation, _swivelRange * 2);
    }

    void Update()
    {
        Debug.Log(GetComponentInParent<PlayerController>().playerMode);
        RotateTowards(_swivelRange);
        timer += Time.deltaTime;
    }

    private void RotateTowards(float rotationAngle)
    {
        if (_swivelSpeed <= Mathf.Epsilon)
        {
            return;
        }
        float cycles = timer / _swivelSpeed ;// % (2 * MathF.PI);
        float rawSinWave = Mathf.Sin(cycles * Mathf.Deg2Rad);
        float movementFactor = (rawSinWave + 1f) / 2f;

        transform.localRotation = Quaternion.Lerp(startRotation, targetRotation, movementFactor);
    }
}
