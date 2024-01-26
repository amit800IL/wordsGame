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

    // Update is called once per frame
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
        //float t = Mathf.PingPong(Time.time * rotationAngle * Time.deltaTime, 1f);
        //if (!isForward)
        //{
        //    t = 1f - t; // Reverse the interpolation when going backward
        //}

        //transform.localRotation = Quaternion.Lerp(startRotation, targetRotation, t);

        //// Check if the rotation reached the target, then toggle the direction
        //if (t >= 0.99f)
        //{
        //    isForward = !isForward;
        //}

        //if (_swivelSpeed <= Mathf.Epsilon)
        //{
        //    return;
        //}
        //float cycles = Time.time / _swivelSpeed;
        //float rawSinWave = Mathf.Sin(cycles * tau);
        //float movementFactor = (rawSinWave + 1f) / 2f;
        //transform.localRotation = Quaternion.Lerp(startingRotation, Quaternion.Euler(0, 0, rotationAngle * movementFactor), _swivelSpeed * timeCount);
        //transform.position = startingRotation + offset;

        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, rotationAngle), _swivelSpeed);

        //if (transform.localRotation.eulerAngles.z >= Quaternion.Euler(0, 0, rotationAngle).z)
        //{
        //    _swivelRange = -_swivelRange;
        //    return;
        //}
    }
}
