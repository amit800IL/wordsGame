using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swivel : MonoBehaviour
{
    [Header("SwivelAttributes")]
    [SerializeField] private float _swivelSpeed;
    [SerializeField] private float _swivelRange;



    // Update is called once per frame
    void Update()
    {
        Swiveling();
    }
    private void Swiveling()
    {
        RotateTowards(_swivelRange);
    }
    private void RotateTowards(float rotationAngle)
    {
    
      transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(90, 0, rotationAngle), _swivelSpeed);
        Debug.Log(transform.localRotation.eulerAngles.z) ; // fix rotation checker
        //Debug.Log(transform.localEulerAngles.z);
        // if (transform.localEulerAngles.z >= rotationAngle) return;
        // transform.Rotate(0, 0, _swivelSpeed);
    }
}
