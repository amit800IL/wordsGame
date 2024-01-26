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
        float currentAngle = ConvertAngle(transform.localRotation.eulerAngles.y);
        if (currentAngle >= ConvertAngle(targetRotation.eulerAngles.z) - 0.1)
        {
            rotateRight = false;
        }
        if (currentAngle <= -ConvertAngle(targetRotation.eulerAngles.z) + 0.1)
        {
            rotateRight = true;
        }
    }

    private float ConvertAngle(float angle)
    {
        if(angle >=0 && angle <= 180)
        {
            return angle;
        }
        return angle - 360;
    }

    private void RotateTowards(float rotationAngle)
    {
        float xLocalRotation = transform.localRotation.eulerAngles.x;
        float yLocalRotation = transform.localRotation.eulerAngles.y;
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(xLocalRotation, yLocalRotation, rotationAngle), _swivelSpeed);
    }
}
