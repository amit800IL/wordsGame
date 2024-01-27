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

    void Start()
    {
        startRotation = transform.localRotation;
        float xLocalRotation = transform.localRotation.eulerAngles.x;
        float yLocalRotation = transform.localRotation.eulerAngles.y;
        targetRotation = Quaternion.Euler(xLocalRotation, yLocalRotation, _swivelRange * 2);
    }

    void Update()
    {
        RotateTowards(_swivelRange);
    }

    private void RotateTowards(float rotationAngle)
    {
        if (_swivelSpeed <= Mathf.Epsilon)
        {
            return;
        }
        float cycles = (Time.time % (2 * MathF.PI)) / _swivelSpeed;
        float rawSinWave = Mathf.Sin(cycles * Mathf.Deg2Rad);
        float movementFactor = (rawSinWave + 1f) / 2f;

        transform.localRotation = Quaternion.Lerp(startRotation, targetRotation, movementFactor);
    }
}
