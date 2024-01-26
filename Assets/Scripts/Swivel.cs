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


    const float tau = Mathf.PI * 2;

    bool rotateRight = true;

    void Start()
    {
        startRotation = transform.localRotation;
        float xLocalRotation = transform.localRotation.eulerAngles.x;
        float yLocalRotation = transform.localRotation.eulerAngles.y;
        targetRotation = Quaternion.Euler(xLocalRotation, yLocalRotation, _swivelRange * 2);
    }

    void Update()
    {
        Swiveling();
    }

    private void Swiveling()
    {
        //float rotateAngle = rotateRight ? _swivelRange : -_swivelRange;
        RotateTowards(_swivelRange);
        //float currentAngle = ConvertAngle(transform.localRotation.eulerAngles.y);
        //if (currentAngle >= ConvertAngle(targetRotation.eulerAngles.z) - 0.1)
        //{
        //    rotateRight = false;
        //}
        //if (currentAngle <= -ConvertAngle(targetRotation.eulerAngles.z) + 0.1)
        //{
        //    rotateRight = true;
        //}
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
        if (_swivelSpeed <= Mathf.Epsilon)
        {
            return;
        }
        float cycles = Time.time / _swivelSpeed;
        float rawSinWave = Mathf.Sin(cycles * Mathf.Deg2Rad);
        float movementFactor = (rawSinWave + 1f) / 2f;

        transform.localRotation = Quaternion.Lerp(startRotation, targetRotation, movementFactor);
    }
}
