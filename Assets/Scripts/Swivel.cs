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

    bool rotateRight = true;

    void Start()
    {
        startRotation = transform.rotation;
        targetRotation = Quaternion.Euler(0f, 0f, _swivelRange);
    }

    void Update()
    {
        Swiveling();
    }

    private void Swiveling()
    {
        float rotateAngle = rotateRight ? _swivelRange : -_swivelRange;
        RotateTowards(rotateAngle);
        float currentAngle = transform.rotation.z;
        if (currentAngle >= targetRotation.z - 0.1)
        {
            rotateRight = false;
        }
        if (currentAngle <= startRotation.z + 0.1)
        {
            rotateRight = true;
        }
    }

    private void RotateTowards(float rotationAngle)
    {
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, rotationAngle), _swivelSpeed);
    }
}
